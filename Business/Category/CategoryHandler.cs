using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Business.Category
{
    public class CategoryHandler : ICategoryHandler
    {
        private readonly MyDB_Context _myDbContext;

        public CategoryHandler(MyDB_Context myDbContext)
        {
            _myDbContext = myDbContext;
        }


        public async Task<Response> CreateCategory(CategoryModel model)
        {
            var entity = AutoMapperUtils.AutoMap<CategoryModel, Data.DataModel.Category>(model);
            _myDbContext.Category.Add(entity);
            int rs = await _myDbContext.SaveChangesAsync();
            if(rs > 0)
            {
                return new ResponseObject<CategoryModel>(model, "Thêm mới thành công", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, "Thêm mới thất bại");
            }
        }

        public async Task<Response> DeleteCategory(Guid cateId)
        {
            var data = await _myDbContext.Category.FirstOrDefaultAsync(x => x.CategoryId == cateId);
            _myDbContext.Category.Remove(data);
            int rs = await _myDbContext.SaveChangesAsync();
            if (rs > 0)
            {
                return new ResponseObject<Guid>(cateId, "Xóa dữ liệu thành công", Code.Success);
            }
            else
            {
                return new Response(Code.ServerError, "Xóa dữ liệu thất bại ");
            }
        }

        public async Task<Response> GetAllCategory(CategoryModel model)
        {
            var data = await _myDbContext.Category.ToListAsync();

            if(model.Status == true)
            {
                data = data.Where(x => x.Status == model.Status).ToList();
            }

            if(model.PageSize.HasValue && model.PageNumber.HasValue)
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

            var entity = AutoMapperUtils.AutoMap<Data.DataModel.Category, CategoryModel>(data);
            if(entity != null)
            {
                return new ResponseObject<List<CategoryModel>>(entity, "Lấy dữ liệu thành công ", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, "Lấy dữ liệu thất bại");
            }
        }

        public async Task<Response> GetCateById(Guid cateId)
        {
            var data = await _myDbContext.Category.FirstOrDefaultAsync(x => x.CategoryId == cateId);
            var entity = AutoMapperUtils.AutoMap<Data.DataModel.Category, CategoryModel>(data);
            if(entity != null)
            {
                return new ResponseObject<CategoryModel>(entity, "Lấy dữ liệu thành công", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, "Lấy dữ liệu thất bại");
            }
        }


        public async Task<Response> GetCateByName(string cateName)
        {
            var data = await _myDbContext.Category.ToListAsync();
            if(!String.IsNullOrEmpty(cateName))
            {
                data = data.Where(x => x.CategoryName.Contains(cateName)).ToList();
            }
            var entity = AutoMapperUtils.AutoMap<Data.DataModel.Category, CategoryModel>(data);
            if (entity != null)
            {
                return new ResponseObject<List<CategoryModel>>(entity, "Lấy dữ liệu thành công", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, "Lấy dữ liệu thất bại");
            }

        }

        public async Task<Response> UpdateCategory(CategoryModel model)
        {
            var data = AutoMapperUtils.AutoMap<CategoryModel, Data.DataModel.Category>(model);
            _myDbContext.Category.Update(data);
            int rs = await _myDbContext.SaveChangesAsync();
            if(rs > 0)
            {
                return new ResponseObject<CategoryModel>(model, "Cập nhật thành công", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, "Cập nhật thất bại ");
            }
        }
    }
}
