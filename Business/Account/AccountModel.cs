using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Account
{
    public class AccountModel
    {
        public Guid UserId { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Address { get; set; }

        public int State { get; set; }

        public bool Decentralization { get; set; }

        public int Phone { get; set; }

        public string Email { get; set; }

        public DateTime? DateTime { get; set; }

        public string Password { get; set; }

        public int CountRrror { get; set; }

        public bool Islock { get; set; }

        public DateTime? TimeLock { get; set; }

        public string TokenChangePassword { get; set; }
    }

    public class LoginAccountModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public int State { get; set; }
    }

    public class GetAllAccountModel
    {
        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }

        public bool Islock { get; set; }

        public int State { get; set; }
    }

    public class UpdatePawwordModel
    {
        public Guid UserId { get; set; }

        public string Password { get; set; }
    }

}
