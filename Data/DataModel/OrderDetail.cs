using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.DataModel
{
    public class OrderDetail
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("product_name")]
        [StringLength(50)]
        public string ProductName  { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("total")]
        public float Total { get; set; }

        [Column("image")]
        public string Image { get; set; }

        [Column("booking_date")]
        public DateTime? BookingDate { get; set; }

        [Column("cancellation_date")]
        public DateTime? CancellationDate { get; set; }

        [Column("status")]
        public bool Status { get; set; }

        /// <summary>
        /// khóa ngoại tham chiếu đến bảng Order
        /// </summary>
        [Column("order_id")]
        public Guid Order_Id { get; set; }
        [ForeignKey("Order_Id")]
        public virtual Order Order { get; set; }

    }
}
