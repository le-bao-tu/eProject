using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Business.ItemDimMst
{
    public class DimMstHandller : IDimMstHandller
    {
        private readonly MyDB_Context _myDbContext;

        public DimMstHandller(MyDB_Context myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<Response> CreateDimMst(DimMstModel model)
        {
            Random r = new Random();
            string num = r.Next().ToString();
            model.Style_Code = num;

            var data = AutoMapperUtils.AutoMap<DimMstModel, Data.DataModel.DimMst>(model);
            _myDbContext.DimMst.Add(data);
            int rs = await _myDbContext.SaveChangesAsync();
            if(rs > 0)
            {
                return new ResponseObject<DimMstModel>(model, $"{Message.CreateSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.CreateError}");
            }
        }

        public async Task<Response> DeleteDimMst(string dimId)
        {
            var data = await _myDbContext.DimMst.FirstOrDefaultAsync(x => x.Style_Code == dimId);
            if(data != null)
            {
                _myDbContext.DimMst.Remove(data);
                int rs = await _myDbContext.SaveChangesAsync();
                if(rs > 0)
                {
                    return new ResponseObject<String>(dimId, $"{Message.DeleteSuccess}", Code.Success);
                }
                else
                {
                    return new ResponseError(Code.ServerError, $"{Message.DeleteError}");
                }
            }
            else
            {
                return new ResponseError(Code.BadRequest, $"Id không tồn tại trong hệ thống");
            }
        }

        public async Task<Response> GetAllDimMst(DimMstPageModel model)
        {
            var data = await _myDbContext.DimMst.ToListAsync();

            if(model.PageNumber.HasValue && model.PageSize.HasValue)
            {
                if(model.PageSize <= 0)
                {
                    model.PageSize = 20;
                }

                int excludeRows = (model.PageSize.Value - 1) * (model.PageNumber.Value);
                if (excludeRows <= 0)
                {
                    excludeRows = 0;
                }

                // query 
                data = data.Skip(excludeRows).Take(model.PageSize.Value).ToList();
            }

            var entity = AutoMapperUtils.AutoMap<Data.DataModel.DimMst, DimMstModel>(data);
            if(entity != null)
            {
                return new ResponseObject<List<DimMstModel>>(entity, $"{Message.GetDataSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
            }
        } 

        public async Task<Response> GetDimMstById(string dimId)
        {
            var data = await _myDbContext.DimMst.FirstOrDefaultAsync(x => x.Style_Code == dimId);
            if(data != null)
            {
                var entity = AutoMapperUtils.AutoMap<Data.DataModel.DimMst, DimMstModel>(data);
                if (entity != null)
                {
                    return new ResponseObject<DimMstModel>(entity, $"{Message.GetDataSuccess}", Code.Success);

                }
                else
                {
                    return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
                }
             }
            else
            {
                return new Response(Code.BadRequest, $"Id không tồn tại trong hệ thống");
            }
        }

        public async Task<Response> GetDimMstByName(string dimName)
        {
            var data = await _myDbContext.DimMst.ToListAsync();

            if(!String.IsNullOrEmpty(dimName))
            {
                data = data.Where(x => x.DimMstName.Contains(dimName)).ToList();
            }

            var entity = AutoMapperUtils.AutoMap<Data.DataModel.DimMst, DimMstModel>(data);
            if(entity != null)
            {
                return new ResponseObject<List<DimMstModel>>(entity, $"{Message.GetDataSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
            }
        }

        public async Task<Response> UpdateDimMst(DimMstModel model)
        {
            var data = AutoMapperUtils.AutoMap<DimMstModel, Data.DataModel.DimMst>(model);
            _myDbContext.DimMst.Update(data);
            int rs = await _myDbContext.SaveChangesAsync();
            if(rs > 0)
            {
                return new ResponseObject<DimMstModel>(model, $"{Message.UpdateSuccess}", Code.Success);
            }
            else
            {
                return new Response(Code.ServerError, $"{Message.UpdateError}");
            }
        }
    }
}
