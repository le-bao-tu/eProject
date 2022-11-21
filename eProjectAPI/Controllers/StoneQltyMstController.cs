using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.StoneQltyMst;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared;

namespace eProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoneQltyMstController : ControllerBase
    {
        private IStoneQltyMstHandller _stoneQltyMstHandller;

        // ghi Log
        private readonly ILogger<StoneQltyMstController> _logger;

        public StoneQltyMstController(IStoneQltyMstHandller stoneQltyMstHandller, ILogger<StoneQltyMstController> logger)
        {
            _logger = logger;
            _stoneQltyMstHandller = stoneQltyMstHandller;
        }

        /// <summary>
        /// lấy danh sách danh mục bảng đá 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getAllStoneQlty")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<List<StoneQltyMstModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllStoneQlty(StoneQltyMsPageModel model)
        {
            return Ok(await _stoneQltyMstHandller.GetAllStoneQlty(model));
        }

        /// <summary>
        /// Thêm mới danh mục đá 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insertStoneQlty")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<StoneQltyMstModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> InsertStoneQlty(StoneQltyMstModel model)
        {
            return Ok(await _stoneQltyMstHandller.CreateStoneQlty(model));
        }

        /// <summary>
        /// Cập nhật danh  mục đá 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateStoneQlty")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<StoneQltyMstModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateStoneQlty(StoneQltyMstModel model)
        {
            return Ok(await _stoneQltyMstHandller.UpdateStoneQlty(model));
        }

        /// <summary>
        /// xóa danhh mục đá 
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteStoneQlty")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteStoneQlty(Guid stoId)
        {
            return Ok(await _stoneQltyMstHandller.DeleteStoneQlty(stoId));
        }

        /// <summary>
        /// tìm kiếm theo Id 
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getStoQltyById")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStoQltyById(Guid stoId)
        {
            return Ok(await _stoneQltyMstHandller.GetStoneQltyById(stoId));
        }


        /// <summary>
        /// tìm kiếm theo chất lượng 
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getStoneQltyByName")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<List<StoneQltyMstModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStoneQltyByName(int stoQlty)
        {
            return Ok(await _stoneQltyMstHandller.GetStoneQltyByName(stoQlty));
        }
    }
}