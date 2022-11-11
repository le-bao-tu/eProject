using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Brand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared;

namespace eProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private IBrandHandller _brandHandller;
        // ghi Log
        private readonly ILogger<BrandController> _logger;

        public BrandController(IBrandHandller brandHandller, ILogger<BrandController> logger)
        {
            _logger = logger;
            _brandHandller = brandHandller;
        }

        /// <summary>
        /// lấy danh sách thương hiệu 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getAllBrand")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<List<BrandModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBrand(PageBrandModel model)
        {
            return Ok(await _brandHandller.GetAllBrand(model));
        }

        /// <summary>
        /// Thêm mới thương hiệu 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insertBrand")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<BrandModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> IndertBrand(BrandModel model)
        {
            return Ok(await _brandHandller.CreateBrand(model));
        }

        /// <summary>
        /// Cập nhật thương hiệu  
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateBrand")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<BrandModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateBrand(BrandModel model)
        {
            return Ok(await _brandHandller.UpdateBrand(model));
        }

        /// <summary>
        /// xóa thương hiệu 
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteBrand")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteBrand(Guid brandId)
        {
            return Ok(await _brandHandller.DeleteBrand(brandId));
        }

        /// <summary>
        /// tìm kiếm theo Id 
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getBrandById")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBranById(Guid brandId)
        {
            return Ok(await _brandHandller.GetBrandById(brandId));
        }


        /// <summary>
        /// tìm kiếm theo Name
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getBrandByName")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<List<BrandModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBrandByName(string brandName)
        {
            return Ok(await _brandHandller.GetBrandByName(brandName));
        }
    }
}