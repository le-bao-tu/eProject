using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Business.StoneQltyMst
{
    public class StoneQltyMstHandller : IStoneQltyMstHandller
    {
        private readonly MyDB_Context _myDbContext;

        public StoneQltyMstHandller(MyDB_Context myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<Response> CreateStoneQlty(StoneQltyMstModel model)
        {
            var data = AutoMapperUtils.AutoMap<StoneQltyMstModel, Data.DataModel.StoneQltyMst>(model);
            _myDbContext.StoneQltyMst.Add(data);
            int rs = await _myDbContext.SaveChangesAsync();
            if (rs > 0)
            {
                return new ResponseObject<StoneQltyMstModel>(model, $"{Message.CreateSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.CreateError}");
            }
        }

        public async Task<Response> DeleteStoneQlty(Guid stoneId)
        {
            var data = await _myDbContext.StoneQltyMst.FirstOrDefaultAsync(x => x.StoneQltyId == stoneId);
            _myDbContext.StoneQltyMst.Remove(data);
            int rs = await _myDbContext.SaveChangesAsync();
            if (rs > 0)
            {
                return new ResponseObject<Guid>(stoneId, $"{Message.DeleteSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.DeleteError}");
            }
        }

        public async Task<Response> GetAllStoneQlty(StoneQltyMsPageModel model)
        {
            var data = await _myDbContext.StoneQltyMst.ToListAsync();

            if (model.PageSize.HasValue && model.PageNumber.HasValue)
            {
                if (model.PageSize <= 0)
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

            var entity = AutoMapperUtils.AutoMap<Data.DataModel.StoneQltyMst, StoneQltyMstModel>(data);
            if (entity != null)
            {
                return new ResponseObject<List<StoneQltyMstModel>>(entity, $"{Message.GetDataSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
            }
        }

        public async Task<Response> GetStoneQltyById(Guid stoneId)
        {
            var data = await _myDbContext.StoneQltyMst.FirstOrDefaultAsync(x => x.StoneQltyId.Equals(stoneId));
            var entity = AutoMapperUtils.AutoMap<Data.DataModel.StoneQltyMst, StoneQltyMstModel>(data);
            if (entity != null)
            {
                return new ResponseObject<StoneQltyMstModel>(entity, $"{Message.GetDataSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
            }
        }

        public async Task<Response> GetStoneQltyByName(int stoneQlty)
        {
            var data = await _myDbContext.StoneQltyMst.ToListAsync();
            if (stoneQlty > 0)
            {
                data = data.Where(x => x.StoneQlty == stoneQlty).ToList();
            }
            
            var entity = AutoMapperUtils.AutoMap<Data.DataModel.StoneQltyMst, StoneQltyMstModel>(data);
            if (entity != null)
            {
                return new ResponseObject<List<StoneQltyMstModel>>(entity, $"{Message.GetDataSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
            }
        }

        public async Task<Response> UpdateStoneQlty(StoneQltyMstModel model)
        {
            var data = AutoMapperUtils.AutoMap<StoneQltyMstModel, Data.DataModel.StoneQltyMst>(model);
            _myDbContext.StoneQltyMst.Update(data);
            int rs = await _myDbContext.SaveChangesAsync();
            if (rs > 0)
            {
                return new ResponseObject<StoneQltyMstModel>(model, $"{Message.UpdateSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.UpdateError}");
            }
        }
    }
}
