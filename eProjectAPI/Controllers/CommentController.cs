using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared;

namespace eProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ICommentHandller _commentHandller;
        // ghi Log
        private readonly ILogger<CommentController> _logger;

        public CommentController(ICommentHandller commentHandller, ILogger<CommentController> logger)
        {
            _logger = logger;
            _commentHandller = commentHandller;
        }

        /// <summary>
        /// lấy danh sách danh comment 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getAllComment")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<List<CommentModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllComment(CommentPageModel model)
        {
            return Ok(await _commentHandller.GetAllComment(model));
        }

        /// <summary>
        /// Thêm mới comment 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insertComment")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<CommentModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> IndertComment(CommentModel model)
        {
            return Ok(await _commentHandller.CreateComment(model));
        }

        /// <summary>
        /// Cập nhật commnet 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateComment")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<CommentModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateComment(CommentModel model)
        {
            return Ok(await _commentHandller.UpdateComment(model));
        }

        /// <summary>
        /// xóa chứng comment 
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteComment")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteComment(Guid comId)
        {
            return Ok(await _commentHandller.DeleteComment(comId));
        }

        /// <summary>
        /// tìm kiếm comment 
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getCommentById")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCommentById(Guid comId)
        {
            return Ok(await _commentHandller.GetCommentById(comId));
        }


        /// <summary>
        /// tìm kiếm theo Name
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getCommentByName")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<List<CommentModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCommentByName(string comName)
        {
            return Ok(await _commentHandller.GetCommentByName(comName));
        }
    }
}