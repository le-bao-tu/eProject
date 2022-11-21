using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.DimQltySubMst;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared;

namespace eProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DimQltySubMstController : ControllerBase
    {
        private  IDimQltySubMstHandller _dimQltySubMstHandller;

        private readonly ILogger<DimQltySubMstController> _logger;

        public DimQltySubMstController(IDimQltySubMstHandller dimQltySubMstHandller, ILogger<DimQltySubMstController> logger)
        {
            _dimQltySubMstHandller = dimQltySubMstHandller;
            _logger = logger;
        }

        /// <summary>
        /// getAll danh sách 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getAllDimQltySubMst")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<List<DimQltySubMstModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllDimQltySubMst(DimQltySubMstPageModel model)
        {
            return Ok(await _dimQltySubMstHandller.GetAllDimQltySubMst(model));
        }


        /// <summary>
        /// Thêm mới chất lượng kim cương 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insertDimQltySubMst")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<DimQltySubMstModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> InsertDimQltySubMst(DimQltySubMstModel model)
        {
            return Ok(await _dimQltySubMstHandller.CreateDimQltySubMst(model));
        }

        /// <summary>
        /// Cập nhật chất lượng kim cương 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateDimQltySubMst")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<DimQltySubMstModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateDimQltySubMst(DimQltySubMstModel model)
        {
            return Ok(await _dimQltySubMstHandller.UpdateDimQltySubMst(model));
        }

        /// <summary>
        /// xóa chất lượng kim cương 
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteDimQltySubMst")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteDimQltySubMst(Guid dimId)
        {
            return Ok(await _dimQltySubMstHandller.DeleteDimQltySubMst(dimId));
        }

        /// <summary>
        /// tìm kiếm theo Id 
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getDimQltySubMstById")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDimQltySubMstById(Guid dimId)
        {
            return Ok(await _dimQltySubMstHandller.GetDimQltySubMstById(dimId));
        }


        /// <summary>
        /// tìm kiếm theo Name
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getDimQltySubMstByName")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<List<DimQltySubMstModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDimQltySubMstByName(string dimName)
        {
            return Ok(await _dimQltySubMstHandller.GetDimQltySubMstByName(dimName));
        }
    }
}