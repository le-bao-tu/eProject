using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared;

namespace eProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryHandler _categoryHandler;
        // ghi Log
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryHandler categoryHandler, ILogger<CategoryController> logger)
        {
            _logger = logger;
            _categoryHandler = categoryHandler;
        }

        /// <summary>
        /// lấy danh sách category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getAllCategory")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<List<CategoryModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategory(CategoryModel model)
        {
            return Ok(await _categoryHandler.GetAllCategory(model));
        }

        /// <summary>
        /// Thêm mới danh mục 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insertCategory")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<CategoryModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> IndertCategory(CategoryModel model)
        {
            return Ok(await _categoryHandler.CreateCategory(model));
        }

        /// <summary>
        /// Cập nhật danh mục 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateCategory")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<CategoryModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCategory(CategoryModel model)
        {
            return Ok(await _categoryHandler.UpdateCategory(model));
        }

        /// <summary>
        /// xóa danh mục 
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteCategory")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCategory(Guid cateId)
        {
            return Ok(await _categoryHandler.DeleteCategory(cateId));
        }

        /// <summary>
        /// tìm kiếm theo Id 
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getCategoryById")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoryById(Guid cateId)
        {
            return Ok(await _categoryHandler.GetCateById(cateId));
        }


        /// <summary>
        /// tìm kiếm theo Name
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getCategoryByName")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<List<CategoryModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoryByName(string cateName)
        {
            return Ok(await _categoryHandler.GetCateByName(cateName));
        }
    }
}