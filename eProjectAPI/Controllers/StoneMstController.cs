using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.StoneMst;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared;

namespace eProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoneMstController : ControllerBase
    {
        private IStoneMstHandller _stoneMstHandller;

        private readonly ILogger<StoneMstController> _logger;

        public StoneMstController(IStoneMstHandller stoneMstHandller, ILogger<StoneMstController> logger)
        {
            _stoneMstHandller = stoneMstHandller;
            _logger = logger;
        }

        /// <summary>
        /// lấy danh sách sản phẩm bảng đá 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getAllStoneMst")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<List<StoneMstModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllStoneMst(StoneMstPageModel model)
        {
            return Ok(await _stoneMstHandller.GetAllStoneMst(model));
        }

        /// <summary>
        /// Thêm mới sản phẩm bảng đá 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insertStoneMst")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<StoneMstModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> InsertStoneMst(StoneMstModel model)
        {
            return Ok(await _stoneMstHandller.CreateStoneMst(model));
        }

        /// <summary>
        /// Cập nhật sản phẩm bảng đá 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateStoneMst")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<StoneMstModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateStoneMst(StoneMstModel model)
        {
            return Ok(await _stoneMstHandller.UpdateStoneMst(model));
        }

        /// <summary>
        /// xóa sản phẩm bảng đá 
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteStoneMst")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<String>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteStoneMst(string stoId)
        {
            return Ok(await _stoneMstHandller.DeleteStoneMstById(stoId));
        }

        /// <summary>
        /// tìm kiếm theo Id 
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getStoneMstById")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<String>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStoneMstById(string stoId)
        {
            return Ok(await _stoneMstHandller.GetStoneMstById(stoId));
        }


        /// <summary>
        /// tìm kiếm theo Name
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getStoneMstByName")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<List<StoneMstModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStoneMstByName(string stoName)
        {
            return Ok(await _stoneMstHandller.GetStoneMstByName(stoName));
        }
    }
}