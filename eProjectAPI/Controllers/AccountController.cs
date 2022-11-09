using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        /// <summary>
        ///  đăng nhấp trả về Token 
        /// </summary>
        /// <param name="accountModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> login(LoginAccountModel accountModel)
        {
            return Ok(await _accountHandler.Login(accountModel));
        }

        /// <summary>
        /// Lấy da danh sách tài khoản 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getAllAccount")]
        [Authorize]
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
        public async Task<IActionResult> getAccountByName(string name)
        {
            return Ok(await _accountHandler.GetAccountByName(name));
        }
    }
}