using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.ItemProduct
{
    public interface IItemProductHandller
    {
        Task<Response> GetAllProduct(ItemProductPageModel model);

        Task<Response> CreateProduct(ItemProductModel model);

        Task<Response> UpdateProduct(ItemProductModel model);

        Task<Response> DeleteProductById(string proId);

        Task<Response> GetProductById(string proId);

        Task<Response> GetproductByName(string proName);

    }
}
