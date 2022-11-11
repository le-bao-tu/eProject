using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Certify
{
    public interface ICertifyHandller
    {
        Task<Response> GetAllCertify(CertifyPageModel model);

        Task<Response> CreateCertify(CertifyModel model);

        Task<Response> UpdateCertify(CertifyModel model);

        Task<Response> DeleteCertify(Guid subId);

        Task<Response> GetCertifyById(Guid subId);

        Task<Response> GetCertifyByName(string subCateName);
    }
}
