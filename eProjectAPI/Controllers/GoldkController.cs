using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Goldk;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared;

namespace eProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoldkController : ControllerBase
    {
        private IGoldkHandller _goldkHandller;
        // ghi Log
        private readonly ILogger<GoldkController> _logger;

        public GoldkController(IGoldkHandller goldkHandller, ILogger<GoldkController> logger)
        {
            _logger = logger;
            _goldkHandller = goldkHandller;
        }

        /// <summary>
        /// lấy danh sách danh mục vàng 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getAllGoldk")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<List<GoldkModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllGoldk(GoldkPageModel model)
        {
            return Ok(await _goldkHandller.GetAllGoldk(model));
        }

        /// <summary>
        /// Thêm mới danh mục vàng 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insertGoldk")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<GoldkModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> IndertGoldk(GoldkModel model)
        {
            return Ok(await _goldkHandller.CreateGoldk(model));
        }

        /// <summary>
        /// Cập nhật danh mục vàng
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateGoldk")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<GoldkModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateGoldk(GoldkModel model)
        {
            return Ok(await _goldkHandller.UpdateGoldk(model));
        }

        /// <summary>
        /// xóa thương hiệu 
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteGoldk")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteGoldk(Guid goldkId)
        {
            return Ok(await _goldkHandller.DeleteGoldk(goldkId));
        }

        /// <summary>
        /// tìm kiếm theo Id 
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getGoldkById")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBranById(Guid goldkId)
        {
            return Ok(await _goldkHandller.GetGoldkById(goldkId));
        }


        /// <summary>
        /// tìm kiếm theo Name
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getGoldkByName")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<List<GoldkModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBrandByName(string goldkName)
        {
            return Ok(await _goldkHandller.GetGoldkByName(goldkName));
        }

    }
}