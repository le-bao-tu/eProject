using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Business.Goldk
{
    public class GoldkHandller : IGoldkHandller
    {
        private readonly MyDB_Context _myDbContext;

        public GoldkHandller(MyDB_Context myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<Response> CreateGoldk(GoldkModel model)
        {
            var data = AutoMapperUtils.AutoMap<GoldkModel, Data.DataModel.Goldk>(model);
            _myDbContext.Goldk.Add(data);
            int rs = await _myDbContext.SaveChangesAsync();
            if(rs > 0)
            {
                return new ResponseObject<GoldkModel>(model, $"{Message.CreateSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.CreateError}");
            }
        }

        public async Task<Response> DeleteGoldk(Guid goldkId)
        {
            var data = await _myDbContext.Goldk.FirstOrDefaultAsync(x => x.GoldTypeId == goldkId);
            _myDbContext.Goldk.Remove(data);
            int rs = await _myDbContext.SaveChangesAsync();
            if(rs > 0)
            {
                return new ResponseObject<Guid>(goldkId, $"{Message.DeleteSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.DeleteError}");
            }
        }

        public async Task<Response> GetAllGoldk(GoldkPageModel model)
        {
            var data = await _myDbContext.Goldk.ToListAsync();

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

            var entity = AutoMapperUtils.AutoMap<Data.DataModel.Goldk, GoldkModel>(data);
            if (entity != null)
            {
                return new ResponseObject<List<GoldkModel>>(entity, $"{Message.GetDataSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
            }

        }

        public async Task<Response> GetGoldkById(Guid goldkId)
        {
            var data = await _myDbContext.Goldk.FirstOrDefaultAsync(x => x.GoldTypeId.Equals(goldkId));
            var entity = AutoMapperUtils.AutoMap<Data.DataModel.Goldk, GoldkModel>(data);
            if(entity != null)
            {
                return new ResponseObject<GoldkModel>(entity, $"{Message.GetDataSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
            }
        }

        public async Task<Response> GetGoldkByName(string goldkName)
        {
            var data = await _myDbContext.Goldk.ToListAsync();
            if(!String.IsNullOrEmpty(goldkName))
            {
                data = data.Where(x => x.Gold_Crt.Contains(goldkName)).ToList();
            }

            var entity = AutoMapperUtils.AutoMap<Data.DataModel.Goldk, GoldkModel>(data);
            if(entity != null)
            {
                return new ResponseObject<List<GoldkModel>>(entity, $"{Message.GetDataSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
            }
        }

        public async Task<Response> UpdateGoldk(GoldkModel model)
        {
            var data = AutoMapperUtils.AutoMap<GoldkModel, Data.DataModel.Goldk>(model);
            _myDbContext.Goldk.Update(data);
            int rs = await _myDbContext.SaveChangesAsync();
            if(rs > 0)
            {
                return new ResponseObject<GoldkModel>(model, $"{Message.UpdateSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.UpdateError}");
            }
        }
    }
}
