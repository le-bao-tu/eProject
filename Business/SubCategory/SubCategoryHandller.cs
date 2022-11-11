using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Business.SubCategory
{
    public class SubCategoryHandller : ISubCategoryHandller
    {
        private readonly MyDB_Context _myDbContext;

        public SubCategoryHandller(MyDB_Context myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<Response> CreateSubCategory(SubcategoModel model)
        {
            var data = AutoMapperUtils.AutoMap<SubcategoModel, Data.DataModel.Subcategory>(model);
            _myDbContext.Subcategory.Add(data);
            int rs = await _myDbContext.SaveChangesAsync();
            if(rs > 0)
            {
                return new ResponseObject<SubcategoModel>(model, "Thêm mới thành công", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, "Thêm mới thất bại");
            }
        }

        public async Task<Response> DeleteSubCategory(Guid subId)
        {
            var data = await _myDbContext.Subcategory.FirstOrDefaultAsync(X => X.SubCategory_Id == subId);
            _myDbContext.Subcategory.Remove(data);
            int rs = await _myDbContext.SaveChangesAsync();
            if(rs > 0)
            {
                return new ResponseObject<Guid>(subId, "Xóa dữ liệu thành công", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, "Xóa dữ liệu thất bại");
            }
        }

        public async Task<Response> GetAllSubCategory(SubCategoryPageModel model)
        {
            var data = await _myDbContext.Subcategory.ToListAsync();

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

            var entity = AutoMapperUtils.AutoMap<Data.DataModel.Subcategory, SubcategoModel>(data);
            if (entity != null)
            {
                return new ResponseObject<List<SubcategoModel>>(entity, "Lấy dữ liệu thành công ", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, "Lấy dữ liệu thất bại");
            }
        }

        public async Task<Response> GetSubCategoryById(Guid subId)
        {
            var data = await _myDbContext.Subcategory.FirstOrDefaultAsync(x => x.SubCategory_Id == subId);
            var entity = AutoMapperUtils.AutoMap<Data.DataModel.Subcategory, SubcategoModel>(data);
            if(entity != null)
            {
                return new ResponseObject<SubcategoModel>(entity, "Lấy dữ liệu thành công", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, "Lấy dữ liệu thất bai");
            }
        }

        public async Task<Response> GetSubCategoryByName(string subCateName)
        {
            var data = await _myDbContext.Subcategory.ToListAsync();
            if(!String.IsNullOrEmpty(subCateName))
            {
                data = data.Where(x => x.SubCategoryName.Contains(subCateName)).ToList();
            }

            var entity = AutoMapperUtils.AutoMap<Data.DataModel.Subcategory, SubcategoModel>(data);
            if(entity != null)
            {
                return new ResponseObject<List<SubcategoModel>>(entity, "Lấy dữ liệu thành công ", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, "Lấy dữ liệu thất bại");
            }
        }

        public async Task<Response> UpdateSubCategory(SubcategoModel model)
        {
            var data = AutoMapperUtils.AutoMap<SubcategoModel, Data.DataModel.Subcategory>(model);
            _myDbContext.Subcategory.Update(data);
            int rs = await _myDbContext.SaveChangesAsync();
            if(rs > 0)
            {
                return new ResponseObject<SubcategoModel>(model, "Cập nhâth thành công", Code.Success);
            }
            else
            {
                return new ResponseError(Code.ServerError, "Cập nhật thất bại");
            }
        }
    }
}
