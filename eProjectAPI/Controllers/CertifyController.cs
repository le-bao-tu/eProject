using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Certify;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared;

namespace eProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertifyController : ControllerBase
    {
        private ICertifyHandller _certifyHandller;
        // ghi Log
        private readonly ILogger<CertifyController> _logger;

        public CertifyController(ICertifyHandller certifyHandller, ILogger<CertifyController> logger)
        {
            _logger = logger;
            _certifyHandller = certifyHandller;
        }

        /// <summary>
        /// lấy danh sách danh chứng chỉ 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getAllCertify")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<List<CertifyModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCertify(CertifyPageModel model)
        {
            return Ok(await _certifyHandller.GetAllCertify(model));
        }

        /// <summary>
        /// Thêm mới chứng chỉ 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insertCertify")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<CertifyModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> IndertCertify(CertifyModel model)
        {
            return Ok(await _certifyHandller.CreateCertify(model));
        }

        /// <summary>
        /// Cập nhật chứng chỉ 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateCertify")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<CertifyModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCertify(CertifyModel model)
        {
            return Ok(await _certifyHandller.UpdateCertify(model));
        }

        /// <summary>
        /// xóa chứng chỉ 
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteCertify")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCertify(Guid certifyId)
        {
            return Ok(await _certifyHandller.DeleteCertify(certifyId));
        }

        /// <summary>
        /// tìm kiếm theo Id 
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getCertifyById")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCertifyById(Guid certifyId)
        {
            return Ok(await _certifyHandller.GetCertifyById(certifyId));
        }


        /// <summary>
        /// tìm kiếm theo Name
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getCertifyByName")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseObject<List<CertifyModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCertifyByName(string certifyName)
        {
            return Ok(await _certifyHandller.GetCertifyByName(certifyName));
        }
    }
}