using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Business.DimQltySubMst
{
    public class DimQltySubMstHandller : IDimQltySubMstHandller
    {
        private MyDB_Context _myDB_Context;

        public DimQltySubMstHandller(MyDB_Context myDB_Context)
        {
            _myDB_Context = myDB_Context;
        }

        public async Task<Response> CreateDimQltySubMst(DimQltySubMstModel model)
        {
            var data = AutoMapperUtils.AutoMap<DimQltySubMstModel, Data.DataModel.DimQltySubMst>(model);
            _myDB_Context.DimQltySubMst.Add(data);
            int rs = await _myDB_Context.SaveChangesAsync();
            if(rs > 0)
            {
                return new ResponseObject<DimQltySubMstModel>(model, $"{Message.CreateSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.CreateError}");
            }
        }

        public async Task<Response> DeleteDimQltySubMst(Guid dimId)
        {
            var data = await _myDB_Context.DimQltySubMst.FirstOrDefaultAsync(x => x.DimSub_TypeId == dimId);
            if(data != null)
            {
                _myDB_Context.DimQltySubMst.Remove(data);
                int rs = await _myDB_Context.SaveChangesAsync();
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
                return new ResponseError(Code.ServerError, $"Id không tồn tại ");
            }
        }

        public async Task<Response> GetAllDimQltySubMst(DimQltySubMstPageModel model)
        {
            var data = await _myDB_Context.DimQltySubMst.ToListAsync();

            if(model.PageNumber.HasValue && model.PageSize.HasValue)
            {
                if(model.PageSize <= 0)
                {
                    model.PageSize = 20;
                }

                int excludeRows = (model.PageNumber.Value - 1) * (model.PageSize.Value);
                if(excludeRows <= 0)
                {
                    excludeRows = 0;
                }

                // query
                data = data.Skip(excludeRows).Take(model.PageSize.Value).ToList();
            }

            var entity = AutoMapperUtils.AutoMap<Data.DataModel.DimQltySubMst, DimQltySubMstModel>(data);
            if(entity != null)
            {
                return new ResponseObject<List<DimQltySubMstModel>>(entity, $"{Message.GetDataSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
            }
        }

        public async Task<Response> GetDimQltySubMstById(Guid dimId)
        {
            var data = await _myDB_Context.DimQltySubMst.FirstOrDefaultAsync(x => x.DimSub_TypeId == dimId);
            if(data != null)
            {
                var entity = AutoMapperUtils.AutoMap<Data.DataModel.DimQltySubMst, DimQltySubMstModel>(data);
                if(entity != null)
                {
                    return new ResponseObject<DimQltySubMstModel>(entity, $"{Message.GetDataSuccess}", Code.Success);
                }
                else
                {
                    return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
                }
            }
            else
            {
                return new ResponseError(Code.ServerError, $"Id không tồn tại trong hệ thống");
            }

        }

        public async Task<Response> GetDimQltySubMstByName(string dimQlty)
        {
            var data = await _myDB_Context.DimQltySubMst.ToListAsync();
            if(!String.IsNullOrEmpty(dimQlty))
            {
                data = data.Where(x => x.DimQlty.Equals(dimQlty)).ToList();
            }
            var entity = AutoMapperUtils.AutoMap<Data.DataModel.DimQltySubMst, DimQltySubMstModel>(data);
            if(entity != null)
            {
                return new ResponseObject<List<DimQltySubMstModel>>(entity, $"{Message.GetDataSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
            }
        }

        public async Task<Response> UpdateDimQltySubMst(DimQltySubMstModel model)
        {
            var data = AutoMapperUtils.AutoMap<DimQltySubMstModel, Data.DataModel.DimQltySubMst>(model);
            _myDB_Context.DimQltySubMst.Update(data);
            int rs = await _myDB_Context.SaveChangesAsync();
            if(rs > 0)
            {
                return new ResponseObject<DimQltySubMstModel>(model, $"{Message.UpdateSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.UpdateError}");
            }
        }
    }
}
