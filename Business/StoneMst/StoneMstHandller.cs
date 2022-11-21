using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Business.StoneMst
{
    public class StoneMstHandller : IStoneMstHandller
    {
        private readonly MyDB_Context _myDbContext;

        public StoneMstHandller(MyDB_Context myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<Response> CreateStoneMst(StoneMstModel model)
        {
            Random r = new Random();
            string num = r.Next().ToString();

            model.StyleCode = num;
            var data = AutoMapperUtils.AutoMap<StoneMstModel, Data.DataModel.StoneMst>(model);
            _myDbContext.StoneMst.Add(data);
            int rs = await _myDbContext.SaveChangesAsync();
            if(rs > 0)
            {
                return new ResponseObject<StoneMstModel>(model, $"{Message.CreateSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.CreateError}");
            }
        }

        public async Task<Response> DeleteStoneMstById(string stoId)
        {
            var data = await _myDbContext.StoneMst.FirstOrDefaultAsync(x => x.StyleCode == stoId);
            _myDbContext.StoneMst.Remove(data);
            int rs = await _myDbContext.SaveChangesAsync();
            if(rs > 0)
            {
                return new ResponseObject<String>(stoId, $"{Message.DeleteSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.DeleteError}");
            }
        }

        public async Task<Response> GetAllStoneMst(StoneMstPageModel model)
        {
            var data = await _myDbContext.StoneMst.ToListAsync();

            if (model.PageSize.HasValue && model.PageSize.HasValue)
            {
                if (model.PageSize <= 0)
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

            var entity = AutoMapperUtils.AutoMap<Data.DataModel.StoneMst, StoneMstModel>(data);
            if (entity != null)
            {
                return new ResponseObject<List<StoneMstModel>>(entity, $"{Message.GetDataSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
            }
        }

        public async Task<Response> GetStoneMstById(string stoId)
        {
            var data = await _myDbContext.StoneMst.FirstOrDefaultAsync(X => X.StyleCode == stoId);
            if (data != null)
            {
                var entity = AutoMapperUtils.AutoMap<Data.DataModel.StoneMst, StoneMstModel>(data);
                if (entity != null)
                {
                    return new ResponseObject<StoneMstModel>(entity, $"{Message.GetDataSuccess}", Code.Success);
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

        public async Task<Response> GetStoneMstByName(string stoneName)
        {
            var data = await _myDbContext.StoneMst.ToListAsync();
            if (!String.IsNullOrEmpty(stoneName))
            {
                data = data.Where(x => x.StoneName.Contains(stoneName)).ToList();
            }

            var entity = AutoMapperUtils.AutoMap<Data.DataModel.StoneMst, StoneMstModel>(data);
            if (entity != null)
            {
                return new ResponseObject<List<StoneMstModel>>(entity, $"{Message.GetDataSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
            }
        }

        public async Task<Response> UpdateStoneMst(StoneMstModel model)
        {
            var data = AutoMapperUtils.AutoMap<StoneMstModel, Data.DataModel.StoneMst>(model);
            _myDbContext.StoneMst.Update(data);
            int rs = await _myDbContext.SaveChangesAsync();
            if (rs > 0)
            {
                return new ResponseObject<StoneMstModel>(model, $"{Message.UpdateSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.UpdateError}");
            }
        }
    }
}
