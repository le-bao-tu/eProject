using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Business.ItemProduct
{
    public class ItemProductHandller : IItemProductHandller
    {
        private readonly MyDB_Context _myDbContext;
        
        public ItemProductHandller( MyDB_Context myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<Response> CreateProduct(ItemProductModel model)
        {
            model.StyleCode = Utils.RandomStyleCode();
            var data = AutoMapperUtils.AutoMap<ItemProductModel, Data.DataModel.ItemProduct>(model);
            _myDbContext.ItemProduct.Add(data);
            int rs = await _myDbContext.SaveChangesAsync();
            if(rs > 0)
            {
                return new ResponseObject<ItemProductModel>(model, $"{Message.CreateSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.CreateError}");
            }


        }
         
        public async Task<Response> DeleteProductById(string proId)
        {
            var data = await _myDbContext.ItemProduct.FirstOrDefaultAsync(x => x.StyleCode == proId);
            if(data != null)
            {
                _myDbContext.ItemProduct.Remove(data);
                int rs = await _myDbContext.SaveChangesAsync();
                if(rs > 0)
                {
                    return new ResponseObject<String>(proId, $"{Message.DeleteSuccess}", Code.Success);
                }
                else
                {
                    return new ResponseError(Code.ServerError, $"{Message.DeleteError}");
                }
            }
            else
            {
                return new ResponseError(Code.ServerError, $"Không tìm thấy Id");
            }
        }

        /// <summary>
        /// GetAll 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Response> GetAllProduct(ItemProductPageModel model)
        {
            var data = await _myDbContext.ItemProduct.ToListAsync();

            if(model.PageSize.HasValue && model.PageSize.HasValue)
            {
                if(model.PageSize <= 0)
                {
                    model.PageSize = 20;
                }

                int excludeRows = (model.PageSize.Value - 1) * (model.PageNumber.Value);
                if(excludeRows <= 0)
                {
                    excludeRows = 0;
                }

                // query 
                data = data.Skip(excludeRows).Take(model.PageSize.Value).ToList();
            }

            var entity = AutoMapperUtils.AutoMap<Data.DataModel.ItemProduct, ItemProductModel>(data);
            if(entity != null)
            {
                return new ResponseObject<List<ItemProductModel>>(entity, $"{Message.GetDataSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
            }
        }

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="proId"></param>
        /// <returns></returns>
        public async Task<Response> GetProductById(string proId)
        {
            var data = await _myDbContext.ItemProduct.FirstOrDefaultAsync(X => X.StyleCode == proId);
            if(data != null)
            {
                var entity = AutoMapperUtils.AutoMap<Data.DataModel.ItemProduct, ItemProductModel>(data);
                if(entity != null)
                {
                    return new ResponseObject<ItemProductModel>(entity, $"{Message.GetDataSuccess}", Code.Success);
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

        /// <summary>
        /// GetNyName
        /// </summary>
        /// <param name="proName"></param>
        /// <returns></returns>
        public async Task<Response> GetproductByName(string proName)
        {
            var data = await _myDbContext.ItemProduct.ToListAsync();
            if(!String.IsNullOrEmpty(proName))
            {
                data = data.Where(x => x.ProductName.Contains(proName)).ToList();
            }

            var entity = AutoMapperUtils.AutoMap<Data.DataModel.ItemProduct, ItemProductModel>(data);
            if(entity != null)
            {
                return new ResponseObject<List<ItemProductModel>>(entity, $"{Message.GetDataSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.GetDataError}");
            }
        }



        public async Task<Response> UpdateProduct(ItemProductModel model)
        {
            
            var data = AutoMapperUtils.AutoMap<ItemProductModel, Data.DataModel.ItemProduct>(model);
            _myDbContext.ItemProduct.Update(data);
            int rs = await _myDbContext.SaveChangesAsync();
            if(rs > 0)
            {
                return new ResponseObject<ItemProductModel>(model, $"{Message.UpdateSuccess}", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, $"{Message.UpdateError}");
            }
        }
    }
}
