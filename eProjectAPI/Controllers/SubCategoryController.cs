using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.SubCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared;

namespace eProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private ISubCategoryHandller _subCategoryHandller;

        // ghi Log
        private readonly ILogger<SubCategoryController> _logger;

        public SubCategoryController(ISubCategoryHandller subCategoryHandller, ILogger<SubCategoryController> logger)
        {
            _logger = logger;
            _subCategoryHandller = subCategoryHandller;
        }

        /// <summary>
        /// lấy danh sách danh mục con 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getAllSubCategory")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<List<SubcategoModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSubCategory(SubCategoryPageModel model)
        {
            return Ok(await _subCategoryHandller.GetAllSubCategory(model));
        }

        /// <summary>
        /// Thêm mới danh mục con 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insertSubCategory")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<SubcategoModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> IndertSubCategory(SubcategoModel model)
        {
            return Ok(await _subCategoryHandller.CreateSubCategory(model));
        }

        /// <summary>
        /// Cập nhật danh  mục con 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateSubCategory")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<SubcategoModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateSubCategory(SubcategoModel model)
        {
            return Ok(await _subCategoryHandller.UpdateSubCategory(model));
        }

        /// <summary>
        /// xóa danhh mục con 
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteSubCategory")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteSubCategory(Guid subId)
        {
            return Ok(await _subCategoryHandller.DeleteSubCategory(subId));
        }

        /// <summary>
        /// tìm kiếm theo Id 
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getSubCategoryById")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSubCategoryById(Guid subId)
        {
            return Ok(await _subCategoryHandller.GetSubCategoryById(subId));
        }


        /// <summary>
        /// tìm kiếm theo Name
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getSubCategoryByName")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<List<SubcategoModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSubCategoryByName(string subName)
        {
            return Ok(await _subCategoryHandller.GetSubCategoryByName(subName));
        }
    }
}