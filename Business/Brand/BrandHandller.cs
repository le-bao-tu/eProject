using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Business.Brand
{
    public class BrandHandller : IBrandHandller
    {
        private readonly MyDB_Context _myDbContext;

        public BrandHandller(MyDB_Context myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<Response> CreateBrand(BrandModel model)
        {
            var entity = AutoMapperUtils.AutoMap<BrandModel, Data.DataModel.Brand>(model);
            _myDbContext.Brand .Add(entity);
            int rs = await _myDbContext.SaveChangesAsync();
            if (rs > 0)
            {
                return new ResponseObject<BrandModel>(model, $"{Message.CreateSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.CreateError}");
            }
        }

        public async Task<Response> DeleteBrand(Guid brandId)
        {
            var data = await _myDbContext.Brand.FirstOrDefaultAsync(x => x.IdBrand == brandId);
            _myDbContext.Brand.Remove(data);
            int rs = await _myDbContext.SaveChangesAsync();
            if (rs > 0)
            {
                return new ResponseObject<Guid>(brandId, $"{Message.DeleteSuccess}", Code.Success);
            }
            else
            {
                return new Response(Code.ServerError, $"{Message.DeleteError}");
            }
        }

        public async Task<Response> GetAllBrand(PageBrandModel model)
        {
            var data = await _myDbContext.Brand.ToListAsync();

            if(model.Status == true)
            {
                data = data.Where(x => x.Status == true).ToList();
            }

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

            var entity = AutoMapperUtils.AutoMap<Data.DataModel.Brand, BrandModel>(data);
            if (entity != null)
            {
                return new ResponseObject<List<BrandModel>>(entity, $"{Message.GetDataSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
            }

        }

        public async Task<Response> GetBrandById(Guid brandId)
        {
            var data = await _myDbContext.Brand.FirstOrDefaultAsync(x => x.IdBrand == brandId);
            var entity = AutoMapperUtils.AutoMap<Data.DataModel.Brand, BrandModel>(data);
            if (entity != null)
            {
                return new ResponseObject<BrandModel>(entity, $"{Message.GetDataSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
            }
        }

        public async Task<Response> GetBrandByName(string brandName)
        {
            var data = await _myDbContext.Brand.ToListAsync();
            if (!String.IsNullOrEmpty(brandName))
            {
                data = data.Where(x => x.BrandName.Contains(brandName)).ToList();
            }
            var entity = AutoMapperUtils.AutoMap<Data.DataModel.Brand, BrandModel>(data);
            if (entity != null)
            {
                return new ResponseObject<List<BrandModel>>(entity, $"{Message.GetDataSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
            }
        }

        public async Task<Response> UpdateBrand(BrandModel model)
        {
            var data = AutoMapperUtils.AutoMap<BrandModel, Data.DataModel.Brand>(model);
            _myDbContext.Brand.Update(data);
            int rs = await _myDbContext.SaveChangesAsync();
            if (rs > 0)
            {
                return new ResponseObject<BrandModel>(model, $"{Message.UpdateSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.UpdateError}");
            }
        }
    }
}
