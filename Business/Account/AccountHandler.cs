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
using System.Net.Mail;

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
         
        public async Task<Response> GetAccessCode(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                return new ResponseError(Code.BadRequest, "Email không được để trống");
            }

            var data = await _myDbContext.Account.FirstOrDefaultAsync(x => x.Email == email || x.Phone.ToString() == email);

            if (data != null)
            {
                Random r = new Random();
                string num = r.Next(0, 9999).ToString();

                //string tempassword = Utils.RandomPassword(6, 1, 1, 0);
                data.TokenChangePassword = num;
                _myDbContext.Account.Update(data);
                int rs = await _myDbContext.SaveChangesAsync();
                if (rs > 0)
                {
                    string to = data.Email; //To address    
                    string from = "lebaotu05122002@gmail.com"; //From address    
                    var ms = data.UserName;
                    var change_pass_word = data.TokenChangePassword;
                    MailMessage message = new MailMessage(from, to);
                    message.IsBodyHtml = true;
                    message.Subject = "LEBAOTU - COMPANY";
                    message.Body = bodyEmail(change_pass_word, ms);
                    message.BodyEncoding = Encoding.UTF8;
                    message.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
                    System.Net.NetworkCredential basicCredential1 = new
                    System.Net.NetworkCredential(from, "ptezfclvjexuwbrk"); 
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = basicCredential1;
                    try
                    {
                        client.Send(message);
                    }

                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    return new Response(Code.Success, "Gửi mã thành công ");
                }
                else
                {
                    return new ResponseError(Code.ServerError, "Cập nhật mã thất bại ");
                }
            }
            else
            {
                return new ResponseError(Code.ServerError, "Thông tin tài khoản không tồn tại trong hệ thống");
            }
        }


        public string bodyEmail(string token, string userFullName)
        {
            string strBody = string.Empty;
            strBody += "<html><head> </head>";
            strBody += "<body  style='width: 500px; border: solid 2px #888; padding: 20px; margin: auto;'>";
            strBody += Environment.NewLine;
            strBody += "<p style='text-align:center'><img alt='' src='https://www.codewithmukesh.com/wp-content/uploads/2020/05/codewithmukesh_logo_wordpress.png' style='height:89px; width:400px'/></p>";
            strBody += "<p>Xin Chào : " + userFullName +"</p>";
            strBody += "<p>Bạn vừa yêu cầu cấp mới mã truy cập với LEBAOTU - COMPANY.</p>";
            strBody += "<p><strong>Mã truy cập của bạn là:</strong></p>";
            strBody += "<div style = 'width: 500px; background-color: #3399FF;height: 60px; display: flex; margin-top:30px; border-radius: 10px;'>";
            strBody += "<span style = 'color: white; font-size: 30px;margin-left: 195px; margin-top:9px; letter-spacing: 15px;'>" + token + "</span></div>";
            strBody += "<hr style='border-top: 2px solid #bbb; margin-top: 10px'>";
            strBody += "<p><strong>KHÔNG CHIA SẺ</strong></p>";
            strBody += "<p>Email này chứa một mã bảo mật của LEBAOTU-COMPANY, vui lòng không chia sẻ email hoặc mã bảo mật này với người khác</p>";
            strBody += "<strong>CÂU HỎI VỀ HỢP ĐỒNG</strong>";
            strBody += "<p> Nếu bạn cần sửa đổi hoặc có câu hỏi về nội dung thông báo,vui lòng liên hệ LEBAOTU-COMPANY qua các kênh sau: Hotline: <b>0388334379</b> Hộp thư điện tử: <b>thongtin@sun.com.vn</b></p>";
            strBody += "</body></html >";
            return strBody;
        }

        public async Task<Response> CreateAccount(AccountModel accountModel)
        {
            var checkEmail = await _myDbContext.Account.FirstOrDefaultAsync(x => x.Email.Equals(accountModel.Email));
            if (checkEmail != null)
            {
                return new ResponseError(Code.BadRequest, "Email đã tồn tại trong hệ thống");
            }
            accountModel.Password = Utils.EncryptSha256(accountModel.Password);
            accountModel.DateTime = DateTime.Now;
            var account = AutoMapperUtils.AutoMap<AccountModel, Data.DataModel.Account>(accountModel);
            _myDbContext.Account.Add(account);
            int rs = await _myDbContext.SaveChangesAsync();
            if (rs > 0)
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
                return new ResponseObject<Guid>(accountId, $"{Message.DeleteSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.DeleteError}");
            }
        }

        public async Task<Response> GetAccountById(Guid accountId)
        {
            var account = await _myDbContext.Account.FirstOrDefaultAsync(x => x.UserId == accountId);
            var accountEntity = AutoMapperUtils.AutoMap<Data.DataModel.Account, AccountModel>(account);
            if (accountEntity != null)
            {
                return new ResponseObject<AccountModel>(accountEntity, $"{Message.GetDataSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
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
                return new ResponseObject<List<AccountModel>>(accountEntity, $"{Message.GetDataSuccess}", Code.ServerError);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
            }
        }

        public async Task<Response> GetAllAccount(GetAllAccountModel getAllAccount)
        {
            var list = await _myDbContext.Account.ToListAsync();

            if (getAllAccount.Islock == true)
            {
                list = list.Where(x => x.Islock == true).ToList();
            }

            if (getAllAccount.PageSize.HasValue && getAllAccount.PageNumber.HasValue)
            {
                if (getAllAccount.PageSize <= 0)
                {
                    getAllAccount.PageSize = 20;
                }

                int excludeRows = (getAllAccount.PageNumber.Value - 1) * (getAllAccount.PageSize.Value);
                if (excludeRows <= 0)
                {
                    excludeRows = 0;
                }

                //Query
                list = list.Skip(excludeRows).Take(getAllAccount.PageSize.Value).ToList();
            }
            var account = AutoMapperUtils.AutoMap<Data.DataModel.Account, AccountModel>(list);
            return new ResponseObject<List<AccountModel>>(account, $"{Message.GetDataSuccess}", Code.Success);
        }

        public async Task<Response> GetByNameToken(string email)
        {
            var data = await _myDbContext.Account.FirstOrDefaultAsync(x => x.Email.Contains(email));
            var entity = AutoMapperUtils.AutoMap<Data.DataModel.Account, AccountModel>(data);
            if (entity != null)
            {
                return new ResponseObject<AccountModel>(entity, $"{Message.GetDataSuccess}", Code.ServerError);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
            }

        }

        public async Task<Response> Login(LoginAccountModel accountModel)
        {
            if (String.IsNullOrEmpty(accountModel.Email))
            {
                return new ResponseError(Code.BadRequest, "Thông tin tài khoản không được để trống");
            }
            if (String.IsNullOrEmpty(accountModel.Password))
            {
                return new ResponseError(Code.BadRequest, "PassWord không được để trống");
            }

            var data = await _myDbContext.Account.FirstOrDefaultAsync(x => x.Email == accountModel.Email || x.Phone.ToString() == accountModel.Email);
            if (data == null)
            {
                return new ResponseError(Code.BadRequest, "Thông tin tài khoản không tồn tại trong hệ thống");
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
                data.TimeLock = DateTime.Now.AddHours(1);
                _myDbContext.Account.Update(data);
                await _myDbContext.SaveChangesAsync();
                return new ResponseError(Code.ServerError, "Tài khoản của bạn đã bị khóa 1h");
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
                    var dt = lbtToken;

                    return new ResponseObject<String>(dt, "Đăng nhập thành công ", Code.Success);
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
                        return new ResponseObject<AccountModel>(accountModel, $"{Message.UpdateSuccess}", Code.Success);
                    }
                    else
                    {
                        return new ResponseError(Code.ServerError, $"{Message.UpdateError}");
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
                        return new ResponseObject<AccountModel>(accountModel, $"{Message.UpdateSuccess}", Code.Success);
                    }
                    else
                    {
                        return new ResponseError(Code.ServerError, $"{Message.UpdateError}");
                    }
                }
            }
            else
            {
                return new ResponseError(Code.ServerError, "không tìm thấy thông tin người dùng");
            }
        }

        public async Task<Response> CheckAccessCode(string email, string access_code)
        {
            if (String.IsNullOrEmpty(email))
            {
                return new ResponseError(Code.BadRequest, "Thông tin tài khoản không được để trống");
            }
            if (String.IsNullOrEmpty(access_code))
            {
                return new Response(Code.BadRequest, "Mã code không được để trống ");
            }

            var data = await _myDbContext.Account.FirstOrDefaultAsync(x => x.Email.Contains(email) || x.Phone.Equals(email));
            if (data != null)
            {
                if (data.TokenChangePassword == access_code)
                {

                    return new ResponseObject<Guid>(data.UserId, "Xác thực thành công ", Code.Success);
                }
                else
                {
                    return new ResponseError(Code.ServerError, "Mã truy cập không hợp lệ");
                }
            }
            else
            {
                return new ResponseError(Code.ServerError, "Thông tin tài khoản không tồn tại trong hệ thống");
            }
        }

        public async Task<Response> UpdatePassword(UpdatePawwordModel model)
        {
            if (String.IsNullOrEmpty(model.Password))
            {
                return new ResponseError(Code.BadRequest, "Password không được để trống");
            }
            var data = await _myDbContext.Account.FirstOrDefaultAsync(x => x.UserId == model.UserId);

            if (data != null)
            {
                var pass = Utils.EncryptSha256(model.Password);
                data.Password = pass;
                _myDbContext.Account.Update(data);
                int rs = await _myDbContext.SaveChangesAsync();
                if (rs > 0)
                {
                    return new ResponseObject<UpdatePawwordModel>(model, "Cập nhật mật khẩu mới thành công", Code.Success);
                }
                else
                {
                    return new ResponseError(Code.ServerError, "Cập nhật thất bại ");
                }
            }
            else
            {
                return new ResponseError(Code.ServerError, "Không tìm thấy Id người dùng");
            }
        }
    }
}
