using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.ItemDimMst;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared;

namespace eProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DimMstController : ControllerBase
    {
        private IDimMstHandller _dimMstHandller;

        // ghi Log
        private readonly ILogger<DimMstController> _logger;

        public DimMstController(IDimMstHandller dimMstHandller , ILogger<DimMstController> logger)
        {
            _dimMstHandller = dimMstHandller;
            _logger = logger;
        }

        /// <summary>
        /// lấy danh sách danh bảng kim cương 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getAllDimMst")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<List<DimMstModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllDimMst(DimMstPageModel model)
        {
            return Ok(await _dimMstHandller.GetAllDimMst(model));
        }

        /// <summary>
        /// Thêm mới bảng kim cương  
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insertDimMst")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<DimMstModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> InsertDimMst(DimMstModel model)
        {
            return Ok(await _dimMstHandller.CreateDimMst(model));
        }

        /// <summary>
        /// Cập nhật bảng kim cương 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateDimMst")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<DimMstModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateDimMst(DimMstModel model)
        {
            return Ok(await _dimMstHandller.UpdateDimMst(model));
        }

        /// <summary>
        /// xóa chứng kim cương 
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteDimMst")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteDimMst(string dimId)
        {
            return Ok(await _dimMstHandller.DeleteDimMst(dimId));
        }

        /// <summary>
        /// tìm kiếm kim cương 
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getDimMstById")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDimMstById(string dimId)
        {
            return Ok(await _dimMstHandller.GetDimMstById(dimId));
        }


        /// <summary>
        /// tìm kiếm theo Name
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getDimMstByName")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<List<DimMstModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDimMstByName(string dimName)
        {
            return Ok(await _dimMstHandller.GetDimMstByName(dimName));
        }
    }
}