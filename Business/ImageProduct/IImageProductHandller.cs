using Microsoft.AspNetCore.Http;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.ImageProduct
{
    public interface IImageProductHandller
    {
        Task<Response> InsertImage(ImageProductModel model);

        Task<Response> UpdateImage(ImageProductModel model);
    }
}
