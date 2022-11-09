using Microsoft.Extensions.Configuration;
using Business.Account;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class AccountHandler : IAccountHandler
    {
        private readonly MyDB_Context _myDbContext;
        private readonly IConfiguration _config;

        public AccountHandler(MyDB_Context myDbContext, IConfiguration config)
        {
            _myDbContext = myDbContext;
            _config = config;
        }


        public async Task<Response> CreateAccount(AccountModel accountModel)
        {
            accountModel.Password = Utils.EncryptSha256(accountModel.Password);
            accountModel.DateTime = DateTime.Now;
            var account = AutoMapperUtils.AutoMap<AccountModel, Data.DataModel.Account>(accountModel);
            _myDbContext.Account.Add(account);
            int rs = await _myDbContext.SaveChangesAsync();
            if(rs > 0)
            {
                return new ResponseObject<AccountModel>(accountModel, "Thêm mới tài khoản thành công", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, "Thêm mới tài khoản thất bại");
            }
        }

        public async Task<Response> DeleteAccount(Guid accountId)
        {
            var account = await _myDbContext.Account.FirstOrDefaultAsync(x => x.UserId == accountId);
            _myDbContext.Account.Remove(account);
           int rs = await _myDbContext.SaveChangesAsync();
            if (rs > 0)
            {
                return new ResponseObject<Guid>(accountId, "Xóa tài thành công", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, "Xóa tài khoản thất bại");
            }
        }

        public async Task<Response> GetAccountById(Guid accountId)
        {
            var account = await _myDbContext.Account.FirstOrDefaultAsync(x => x.UserId == accountId);
            var accountEntity = AutoMapperUtils.AutoMap<Data.DataModel.Account, AccountModel>(account);
            if(accountEntity != null)
            {
                return new ResponseObject<AccountModel>(accountEntity, "Lấy dữ liệu thành công", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, "Lấy dữ liệu thất bại");
            }
        }

        public async Task<Response> GetAccountByName(string accountName)
        {
            var account = await _myDbContext.Account.ToListAsync();
            if (!String.IsNullOrEmpty(accountName))
            {
                account = account.Where(x => x.FullName.Contains(accountName)).ToList();
            }
            var accountEntity = AutoMapperUtils.AutoMap<Data.DataModel.Account, AccountModel>(account);
            if (accountEntity != null)
            {
                return new ResponseObject<List<AccountModel>>(accountEntity, "Lấy dữ liệu thành công", Code.ServerError);
            }
            else
            {
                return new ResponseError(Code.ServerError, "Lấy dữ liệu thất bại");
            }
        }

        public async Task<Response> GetAllAccount(GetAllAccountModel getAllAccount)
        {
            var list = await _myDbContext.Account.ToListAsync();

            if(getAllAccount.Islock == true)
            {
                list = list.Where(x => x.Islock == true).ToList();
            }
            if(getAllAccount.Islock == false)
            {
                list = list.Where(x => x.Islock == false).ToList();
            }

            if(getAllAccount.PageSize.HasValue && getAllAccount.PageNumber.HasValue)
            {
                if(getAllAccount.PageSize <= 0)
                {
                    getAllAccount.PageSize = 20;
                }

                int excludeRows = (getAllAccount.PageNumber.Value - 1) * (getAllAccount.PageSize.Value);
                if(excludeRows <= 0)
                {
                    excludeRows = 0;
                }

                //Query
                list = list.Skip(excludeRows).Take(getAllAccount.PageSize.Value).ToList();
            }
            var account = AutoMapperUtils.AutoMap<Data.DataModel.Account, AccountModel>(list);
            return new ResponseObject<List<AccountModel>>(account, "Tải dữ liệu thành công", Code.Success);
        }

        public async Task<Response> getByNameToken(string email)
        {
            var data = await _myDbContext.Account.FirstOrDefaultAsync(x => x.Email.Contains(email));
            var entity = AutoMapperUtils.AutoMap<Data.DataModel.Account, AccountModel>(data);
            if (entity != null)
            {
                return new ResponseObject<AccountModel>(entity, "Lấy dữ liệu thành công", Code.ServerError);
            }
            else
            {
                return new ResponseError(Code.ServerError, "Lấy dữ liệu thất bại");
            }

        }

        public async Task<Response> Login(LoginAccountModel accountModel)
        {
            if (String.IsNullOrEmpty(accountModel.Email))
            {
                return new ResponseError(Code.BadRequest, "Email không được để trống");
            }
            if (String.IsNullOrEmpty(accountModel.Password))
            {
                return new ResponseError(Code.BadRequest, "PassWord không được để trống");
            }

            var data = await _myDbContext.Account.FirstOrDefaultAsync(x => x.Email == accountModel.Email);
            if (data == null)
            {
                return new ResponseError(Code.BadRequest, "Email tài khoản không tồn tại trong hệ thống");
            }

            // thời gian khóa này chỉ chạy khi người dùng nhập sai pass quá 5 lần 
            if (data.TimeLock > DateTime.Now)
            {
                return new ResponseError(Code.ServerError, "Tài khoản của bạn vẫn đang trong thời gian khóa");
            }
            // check số lần nhập sai 
            if (data.CountRrror == 5)
            {
                data.Islock = true;
                data.CountRrror = 0;
                data.TimeLock = DateTime.Now.AddMinutes(5);
                _myDbContext.Account.Update(data);
                await _myDbContext.SaveChangesAsync();
                return new ResponseError(Code.ServerError, "Tài khoản của bạn đã bị khóa 5 phút");
            }

            if (data.Islock == false)
            {
                // mã hóa pass người dùng truyền lên 
                var pass = Utils.EncryptSha256(accountModel.Password);

                // check pass 
                if (data.Password == pass)
                {
                   
                        data.CountRrror = 0;
                        data.Islock = false;
                        _myDbContext.Account.Update(data);
                        await _myDbContext.SaveChangesAsync();

                        // thực hiện sinh Token 
                        // lấy key trong file cấu hình ra 
                        var key = _config["Jwt:Key"];

                        // mã hóa cái key lấy được 
                        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

                        // ký vào cái key đã mã hóa ( nghĩa là cái key nó sẽ là MK của cái Token sau khi nó được sinh ra ) 
                        var signingCredential = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

                        // customer JWT header 
                        var header = new JwtHeader(signingCredential);

                        // tạo claims để cấu hình chứa thông tin của người dùng khi pass qua phần check TK 
                        var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Role,"Admin"),
                    //new Claim(ClaimTypes.Name, accountModel.FullName),
                    new Claim(ClaimTypes.MobilePhone,"0388334379"),
                    new Claim(ClaimTypes.Email,accountModel.Email),
                    new Claim(ClaimTypes.Country,"VietNam"),
                    };

                        // sét thời gian hết hạn cho Token 
                        DateTime Expity = DateTime.UtcNow.AddMinutes(10);
                        int ts = (int)(Expity - new DateTime(1970, 1, 1)).TotalSeconds;

                        // tạo Token khớp với các thông số trong file cấu hình statrt để Validate (nghĩa là truyền dữ liệu xuống để khớp với các thông số mà mình đã cấu hình Token trong Startup)
                        var payload = new JwtPayload
                    {
                        {"sub", claims },
                        {"exp",ts },
                        {"iss", _config["Jwt:Issuer"]},
                        {"aud",  _config["Jwt:Audience"]}
                    };

                        var token = new JwtSecurityToken(header, payload);

                        // sinh ra chuỗi Token với các thông số ở trên 
                        var lbtToken = new JwtSecurityTokenHandler().WriteToken(token);

                        // trả về kết quả cho người dùng username và chuỗi Token 
                        var dt = new JsonResult(lbtToken);

                        return new ResponseObject<JsonResult>(dt, "Đăng nhập thành công ", Code.Success);
                }
                else
                {
                    data.CountRrror++;
                    _myDbContext.Account.Update(data);
                    await _myDbContext.SaveChangesAsync();
                    var count = data.CountRrror;
                    return new ResponseErrorLogin(Code.ServerError, "Sai thông tin truy cập vui lòng thực hiện lại", count);
                }
            }
            else
            {
                return new ResponseError(Code.ServerError, "Tài khoản của bạn đã bị khóa ! vui lòng liên hệ Admin để mở lại tài khoản");
            }
        }



        public async Task<Response> UpdateAccount(AccountModel accountModel)
        {
            var checkId = await _myDbContext.Account.FirstOrDefaultAsync(x => x.UserId == accountModel.UserId);
            if (checkId != null)
            {
                if (accountModel.Password == null)
                {
                    accountModel.Password = checkId.Password;
                    AutoMapperUtils.AutoMap<AccountModel, Data.DataModel.Account>(accountModel);
                    _myDbContext.Account.Update(checkId);
                    int rs = await _myDbContext.SaveChangesAsync();
                    if (rs > 0)
                    {
                        return new ResponseObject<AccountModel>(accountModel, "Cập nhật tài khoản thành công ", Code.Success);
                    }
                    else
                    {
                        return new ResponseError(Code.ServerError, "Cập nhật tài khoản thất bại");
                    }
                }
                else
                {
                    accountModel.Password = Utils.EncryptSha256(accountModel.Password);
                    AutoMapperUtils.AutoMap<AccountModel, Data.DataModel.Account>(accountModel);
                    _myDbContext.Account.Update(checkId);
                    int rs = await _myDbContext.SaveChangesAsync();
                    if (rs > 0)
                    {
                        return new ResponseObject<AccountModel>(accountModel, "Cập nhật tài khoản thành công ", Code.Success);
                    }
                    else
                    {
                        return new ResponseError(Code.ServerError, "Cập nhật tài khoản thất bại");
                    }
                }
            }
            else
            {
                return new ResponseError(Code.ServerError, "không tìm thấy thông tin người dùng");
            }
        }
    }
}
