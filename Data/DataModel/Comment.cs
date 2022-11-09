using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.DataModel
{
    [Table("comment")]
    public class Comment
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("content_comment")]
        public string ContentComment { get; set; }

        /// <summary>
        /// mai sẽ fix cứng enum 
        /// </summary>
        [Column("feedback")]
        public int Feedback { get; set; }

        /// <summary>
        /// khóa ngoại tham chiếu đến bảng Account 
        /// </summary>
        [Column("account_id")]
        public Guid Account_Id { get; set; }
        [ForeignKey("Account_Id")]
        public virtual Account Account { get; set; }


        // khóa ngoại tham chiếu đến bảng itemProduct 
        [Column("style_code_itemproduct")]
        public string StyleCodeItemProduct { get; set; }
        [ForeignKey("StyleCodeItemProduct")]
        public virtual ItemProduct ItemProduct { get; set; }

        /// <summary>
        /// khóa ngoại tham chiếu đến bảng đá 
        /// </summary>
        [Column("style_code_stone_mst")]
        public string StyleCodeStoneMst { get; set; }
        [ForeignKey("StyleCodeStoneMst")]
        public virtual StoneMst StoneMst { get; set; }

        /// <summary>
        /// khóa ngoại tham chiếu đến bảng kim cương 
        /// </summary>
        [Column("style_code_dim_mst")]
        public string StyleCodeDimMst { get; set; }
        [ForeignKey("StyleCodeDimMst")]
        public virtual DimMst DimMst { get; set; }
    }
}
