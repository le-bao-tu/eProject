using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.ItemProduct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared;

namespace eProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemProductController : ControllerBase
    {
        private IItemProductHandller _itemProductHandller;
        private readonly ILogger<ItemProductController> _logger;

        public ItemProductController(IItemProductHandller itemProductHandller, ILogger<ItemProductController> logger)
        {
            _itemProductHandller = itemProductHandller;
            _logger = logger;
        }

        /// <summary>
        /// lấy danh sách sản phẩm 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getAllItemProduct")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<List<ItemProductModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllItemProduct(ItemProductPageModel model)
        {
            return Ok(await _itemProductHandller.GetAllProduct(model));
        }

        /// <summary>
        /// Thêm mới sản phẩm 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insertItemProduct")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<ItemProductModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> InsertItemProduct(ItemProductModel model)
        {
            return Ok(await _itemProductHandller.CreateProduct(model));
        }

        /// <summary>
        /// Cập nhật sản phẩm 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateItemProduct")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<ItemProductModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateItemProduct(ItemProductModel model)
        {
            return Ok(await _itemProductHandller.UpdateProduct(model));
        }

        /// <summary>
        /// xóa thương hiệu 
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteItemProduct")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<String>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteItemProduct(string proId)
        {
            return Ok(await _itemProductHandller.DeleteProductById(proId));
        }

        /// <summary>
        /// tìm kiếm theo Id 
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getItemProductById")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetItemProductById(string proId)
        {
            return Ok(await _itemProductHandller.GetProductById(proId));
        }


        /// <summary>
        /// tìm kiếm theo Name
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getItemProductByName")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<List<ItemProductModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetItemProductByName(string proName)
        {
            return Ok(await _itemProductHandller.GetproductByName(proName));
        }
    }
}