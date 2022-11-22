using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Certify
{
    public class CertifyModel
    {
        public Guid IdCertify { get; set; }

        public string CertifyType { get; set; }

        public IFormFile ImageCertify { get; set; }
    }

    public class CertifyPageModel
    {
        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }
    }
}
