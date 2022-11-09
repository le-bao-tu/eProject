using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.DataModel
{
    [Table("account")]
    public class Account
    {
        [Key]
        [Column("userId")]
        public Guid UserId { get; set; }

        [Column("fullName")]
        [StringLength(50)]
        public string FullName { get; set; }

        [Column("userName")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("state")]
        public int State { get; set; }

        [Column("decentralization ")]
        public bool Decentralization { get; set; }

        [Column("phone")]
        public int Phone { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("birtday")]
        public DateTime? DateTime { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("counterror")]
        public int CountRrror { get; set; }

        [Column("islock")]
        public bool Islock { get; set; }

        [Column("timelock")]
        public DateTime? TimeLock { get; set; }

        [Column("token_change_password")]
        public string TokenChangePassword { get; set; }
    }
}
