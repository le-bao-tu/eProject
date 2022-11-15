using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared;

namespace eProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountHandler _accountHandler;
        // ghi Log
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountHandler accountHandler, ILogger<AccountController> logger)
        {
            _logger = logger;
            _accountHandler = accountHandler;
        }

        #region luồng quên mật khẩu 
        /// <summary>
        ///  Lấy mã truy cập , để đổi mật khẩu 
        /// </summary>
        /// <param name="accountModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("get/access_code")]
        [ProducesResponseType(typeof(ResponseObject<String>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccessCode(string email)
        {
            return Ok(await _accountHandler.GetAccessCode(email));
        }

        /// <summary>
        /// check email và access_code 
        /// </summary>
        /// <param name="accountModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("check/access_code")]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CheckAccessCode(string email , string access_code)
        {
            return Ok(await _accountHandler.CheckAccessCode(email,access_code));
        }


        /// <summary>
        /// cập nhật mật khẩu mới 
        /// </summary>
        /// <param name="accountModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update/password")]
        [ProducesResponseType(typeof(ResponseObject<UpdatePawwordModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePassword(UpdatePawwordModel model)
        {
            return Ok(await _accountHandler.UpdatePassword(model));
        }
        #endregion

        /// <summary>
        ///  đăng nhấp trả về Token 
        /// </summary>
        /// <param name="accountModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(ResponseObject<LoginAccountModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> login(LoginAccountModel accountModel)
        {
            return Ok(await _accountHandler.Login(accountModel));
        }

     
        /// <summary>
        /// Lấy da danh sách tài khoản 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getAllAccount")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<List<GetAllAccountModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> getAllAccount(GetAllAccountModel model)
        {
            return Ok(await _accountHandler.GetAllAccount(model));
        }

        /// <summary>
        /// Thêm mới tài khoản 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insertAccount")]
        [ProducesResponseType(typeof(ResponseObject<AccountModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> insertAccount([FromBody] AccountModel account)
        {
            return Ok(await _accountHandler.CreateAccount(account));
        }

        /// <summary>
        /// cập nhật tài khoản 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateAccount")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<AccountModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> updateAccount([FromBody] AccountModel account)
        {
            return Ok(await _accountHandler.UpdateAccount(account));
        }

        /// <summary>
        /// xóa tài khoản 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteAccount")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> deleteAccount(Guid id)
        {
            return Ok(await _accountHandler.DeleteAccount(id));
        }

        /// <summary>
        /// lấy tài khoản theo id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getAccountById")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> getAccountById(Guid id)
        {
            return Ok(await _accountHandler.GetAccountById(id));
        }

        /// <summary>
        /// tìm kiếm tên tài khoản 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getAccountByName")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<String>), StatusCodes.Status200OK)]
        public async Task<IActionResult> getAccountByName(string name)
        {
            return Ok(await _accountHandler.GetAccountByName(name));
        }
    }
}