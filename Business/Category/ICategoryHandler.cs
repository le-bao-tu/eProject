using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Category
{
    public interface ICategoryHandler
    {
        Task<Response> GetAllCategory(CategoryModel model);

        Task<Response> CreateCategory(CategoryModel model);

        Task<Response> UpdateCategory(CategoryModel model);

        Task<Response> DeleteCategory(Guid cateId);

        Task<Response> GetCateById(Guid cateId);

        Task<Response> GetCateByName(string cateName);


    }
}
