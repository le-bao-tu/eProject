using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.DataModel
{
    public class Order
    {
        [Key]
        [Column("id_order")]
        public Guid IdOrder { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("phone")]
        public int Phone { get; set; }

        [Column("total")]
        public float Total { get; set; }

        [Column("note")]
        public string Note { get; set; }

        [Column("booking_date")]
        public DateTime? BookingDate { get; set; }

        [Column("cancellation_date")]
        public DateTime? CancellationDate { get; set; }

        [Column("status")]
        public int Status { get; set; }

        [Column("account_id")]
        public Guid Account_Id { get; set; }
        [ForeignKey("Account_Id")]
        public virtual Account  Account{ get; set; }
    }
}
