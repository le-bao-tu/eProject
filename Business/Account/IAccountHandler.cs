using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Account
{
    public interface IAccountHandler
    {
        Task<Response> GetAllAccount(GetAllAccountModel getAllAccount);

        Task<Response> CreateAccount(AccountModel accountModel);

        Task<Response> UpdateAccount(AccountModel accountModel);

        Task<Response> DeleteAccount(Guid accountId);

        Task<Response> GetAccountById(Guid accountId);

        Task<Response> GetAccountByName(string accountName);

        Task<Response> Login(LoginAccountModel accountModel);

        Task<Response> GetByNameToken(string email);

        Task<Response> GetAccessCode(string email);

        Task<Response> CheckAccessCode(string email , string access_code);

        Task<Response> UpdatePassword(UpdatePawwordModel model);
    }
}
