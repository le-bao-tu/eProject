using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Business.DimQltyMst
{
    public class DimQltyMstHandller : IDimQltyMstHandller
    {
        private readonly MyDB_Context _myDbContext;

        public DimQltyMstHandller(MyDB_Context myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<Response> CreateDimQltyMst(DimQltyMstModel model)
        {
            var data = AutoMapperUtils.AutoMap<DimQltyMstModel, Data.DataModel.DimQltyMst>(model);
            _myDbContext.DimQltyMst.Add(data);
            int rs = await _myDbContext.SaveChangesAsync();
            if(rs > 0)
            {
                return new ResponseObject<DimQltyMstModel>(model, $"{Message.CreateSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.CreateError}");
            }
        }

        public async Task<Response> DeleteDimQltyMst(Guid dimId)
        {
            var data = await _myDbContext.DimQltyMst.FirstOrDefaultAsync(x => x.DimqltyId == dimId);
            if(data != null)
            {
                _myDbContext.DimQltyMst.Remove(data);
                int rs = await _myDbContext.SaveChangesAsync();
                if(rs > 0)
                {
                    return new ResponseObject<Guid>(dimId, $"{Message.DeleteSuccess}", Code.Success);
                }
                else
                {
                    return new ResponseError(Code.ServerError, $"{Message.DeleteError}");
                }
            }
            else
            {
                return new ResponseError(Code.ServerError, $"Id không tồn tại trong hệ thống");
            }
        }

        public async Task<Response> GetAllDimQltyMst(DimQltyMstPageModel model)
        {
            var data = await _myDbContext.DimQltyMst.ToListAsync();

            if(model.PageNumber.HasValue && model.PageSize.HasValue)
            {
                if(model.PageSize <= 0)
                {
                    model.PageSize = 20;
                }


                int excludeRows = (model.PageNumber.Value - 1) * (model.PageSize.Value);
                if (excludeRows <= 0)
                {
                    excludeRows = 0;
                }

                //query 
                data = data.Skip(excludeRows).Take(model.PageSize.Value).ToList();
            }

            var entity = AutoMapperUtils.AutoMap<Data.DataModel.DimQltyMst, DimQltyMstModel>(data);
            if(entity != null)
            {
                return new ResponseObject<List<DimQltyMstModel>>(entity, $"{Message.GetDataSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
            }

        }

        public async Task<Response> GetDimQltyMstById(Guid dimId)
        {
            var data = await _myDbContext.DimQltyMst.FirstOrDefaultAsync(x => x.DimqltyId == dimId);
            if(data != null)
            {
                var entity = AutoMapperUtils.AutoMap<Data.DataModel.DimQltyMst, DimQltyMstModel>(data);
                if(entity != null)
                {
                    return new ResponseObject<DimQltyMstModel>(entity, $"{Message.GetDataSuccess}", Code.Success);
                }
                else
                {
                    return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
                }
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
            }
        }

        public async Task<Response> GetDimQltyMstByName(string dimQlty)
        {
            var data = await _myDbContext.DimQltyMst.ToListAsync();
            if(!String.IsNullOrEmpty(dimQlty))
            {
                data = data.Where(x => x.DimQlty.Equals(dimQlty)).ToList();
            }
            var entity = AutoMapperUtils.AutoMap<Data.DataModel.DimQltyMst, DimQltyMstModel>(data);
            if(entity != null)
            {
                return new ResponseObject<List<DimQltyMstModel>>(entity, $"{Message.GetDataSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
            }
        }

        public async Task<Response> UpdateDimQltyMst(DimQltyMstModel model)
        {
            var data = AutoMapperUtils.AutoMap<DimQltyMstModel, Data.DataModel.DimQltyMst>(model);
            _myDbContext.DimQltyMst.Update(data);
            int rs = await _myDbContext.SaveChangesAsync();
            if(rs > 0)
            {
                return new ResponseObject<DimQltyMstModel>(model, $"{Message.UpdateSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.UpdateError}");
            }
        }
    }
}
