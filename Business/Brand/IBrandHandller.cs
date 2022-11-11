using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Brand
{
    public interface IBrandHandller
    {
        Task<Response> GetAllBrand(PageBrandModel model);

        Task<Response> CreateBrand(BrandModel model);

        Task<Response> UpdateBrand(BrandModel model);

        Task<Response> DeleteBrand(Guid brandId);

        Task<Response> GetBrandById(Guid brandId);

        Task<Response> GetBrandByName(string brandName);

    }


}
