using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.ImageProduct;
using Data;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Business.ItemProduct
{
    public class ItemProductHandller : IItemProductHandller
    {
        private readonly MyDB_Context _myDbContext;
        private readonly IImageProductHandller _imageProductHandller;

        public ItemProductHandller( MyDB_Context myDbContext, IImageProductHandller imageProductHandller)
        {
            _myDbContext = myDbContext;
            _imageProductHandller = imageProductHandller;
        }

        public async Task<Response> CreateProduct(ItemProductModel model)
        {
            Random r = new Random();
            string num = r.Next().ToString();
            model.StyleCode = num;
            // upload file 
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");
            // nếu chưa tòn tại thư mục files thì sẽ tạo thư mục 
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // kiểm tra đuôi file 
            //var p = Path.GetExtension(model.ImageProduct.FileName);
            //if(p == ".jpg" || )
            //{

            //}

            // lấy file của người dùng upload 
            FileInfo fileInfo = new FileInfo(model.Image.FileName);
            if(fileInfo != null)
            {
                string fileName = fileInfo.Extension;

                string fileNameWithPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    model.Image.CopyTo(stream);
                }
            }
           

            var data = AutoMapperUtils.AutoMap<ItemProductModel, Data.DataModel.ItemProduct>(model);
            _myDbContext.ItemProduct.Add(data);
            int rs = await _myDbContext.SaveChangesAsync();
            if(rs > 0)
            {
                // upload nhiều ảnh 
                if (model.imageProduct.Count > 0)
                {
                    foreach (var item in model.imageProduct)
                    {
                        string pathImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroor/FilesImage");
                        // nếu chưa tồn tại thư mục chứa nhiều ảnh thì tạo thư mục 
                        if (!Directory.Exists(pathImage))
                        {
                            Directory.CreateDirectory(pathImage);
                        }

                        string fileNameImage = Path.Combine(path, item.FileName);

                        using (var stream = new FileStream(fileNameImage, FileMode.Create))
                        {
                            item.CopyTo(stream);

                            var dt = await _imageProductHandller.InsertImage(new ImageProductModel()
                            {
                                Image = model.imageProduct,
                                ProId = model.StyleCode,

                            });

                        }
                    }
                }

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
