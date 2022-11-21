using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.DimQltyMst;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared;

namespace eProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DimQltyMstController : ControllerBase
    {
        private IDimQltyMstHandller _dimQltyMstHandller;
        // ghi Log
        private readonly ILogger<DimQltyMstController> _logger;

        public DimQltyMstController(IDimQltyMstHandller dimQltyMstHandller, ILogger<DimQltyMstController> logger)
        {
            _logger = logger;
            _dimQltyMstHandller = dimQltyMstHandller;
        }

        /// <summary>
        /// lấy danh sách danh chất lượng kim cương 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getAllDimQltyMst")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<List<DimQltyMstModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllDimQltyMst(DimQltyMstPageModel model)
        {
            return Ok(await _dimQltyMstHandller.GetAllDimQltyMst(model));
        }

        /// <summary>
        /// Thêm mới chất lượng kim cương 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insertDimQltyMst")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<DimQltyMstModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> InsertDimQltyMst(DimQltyMstModel model)
        {
            return Ok(await _dimQltyMstHandller.CreateDimQltyMst(model));
        }

        /// <summary>
        /// Cập nhật chất lượng kim cương 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateDimQltyMst")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<DimQltyMstModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateDimQltyMst(DimQltyMstModel model)
        {
            return Ok(await _dimQltyMstHandller.UpdateDimQltyMst(model));
        }

        /// <summary>
        /// xóa chất lượng kim cương 
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteDimQltyMst")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteDimQltyMst(Guid dimId)
        {
            return Ok(await _dimQltyMstHandller.DeleteDimQltyMst(dimId));
        }

        /// <summary>
        /// tìm kiếm theo Id 
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getDimQltyMstById")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDimQltyMstById(Guid dimId)
        {
            return Ok(await _dimQltyMstHandller.GetDimQltyMstById(dimId));
        }


        /// <summary>
        /// tìm kiếm theo Name
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getDimQltyMstByName")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<List<DimQltyMstModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDimQltyMstByName(string dimName)
        {
            return Ok(await _dimQltyMstHandller.GetDimQltyMstByName(dimName));
        }
    }
}