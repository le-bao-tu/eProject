using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.SubCategory
{
    public interface ISubCategoryHandller
    {
        Task<Response> GetAllSubCategory(SubCategoryPageModel model);

        Task<Response> CreateSubCategory(SubcategoModel model);

        Task<Response> UpdateSubCategory(SubcategoModel model);

        Task<Response> DeleteSubCategory(Guid certifyId);

        Task<Response> GetSubCategoryById(Guid certifyId);

        Task<Response> GetSubCategoryByName(string scertifyName);
    }
}
