using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.DataModel
{
    [Table("stone_mst")]
    public class StoneMst
    {
        [Key]
        [Column("style_code")]
        public string StyleCode { get; set; }

        [StringLength(150)]
        [Column("stone_name")]
        public string StoneName { get; set; }

        [Column("image")]
        public string Image { get; set; }
        
        /// <summary>
        /// trọng lượng đá mỗi viên 
        /// </summary>
        [Column("stone_gm")]
        public float Stone_DM { get; set; }

        /// <summary>
        /// tổng số đá trong mặt hàng 
        /// </summary>
        [Column("stone_pcs")]
        public float Stone_PCS { get; set; }

        /// <summary>
        /// carat của đá 
        /// </summary>
        [Column("stone_crt")]
        public float MyProperty { get; set; }

        /// <summary>
        /// tỉ lệ của mỗi viên đá 
        /// </summary>
        [Column("stone_rate")]
        public float Stone_Rate { get; set; }

        /// <summary>
        /// tổng số lượng đá trong vật phẩm 
        /// </summary>
        [Column("stone_amt")]
        public float Stone_Amt { get; set; }
        /// <summary>
        /// chi tiêt 
        /// </summary>
        [Column("detail")]
        public string Detail { get; set; }

        /// <summary>
        ///khóa ngoại đến bảng danh mục cha 
        /// </summary>
        [Column("category_id")]
        public Guid Category_id { get; set; }
        [ForeignKey("Category_id")]
        public virtual Category Category { get; set; }

        /// <summary>
        /// khóa ngoại tham chiếu đến bảng danh mục con 
        /// </summary>
        [Column("subcategory_id")]
        public Guid? SubCategoryId { get; set; }
        [ForeignKey("SubCategoryId")]
        public virtual Subcategory Subcategory { get; set; }

        /// <summary>
        /// khóa ngoại tham chiếu đến bảng stoneQltyMst (bảng chất lượng đá )
        /// </summary>
        [Column("stoneqlty_id")]
        public Guid StoneqltyId { get; set; }
        [ForeignKey("StoneqltyId")]
        public virtual StoneQltyMst StoneQltyMst { get; set; }

        /// <summary>
        /// khóa ngoại tham chiếu đến bảng certify(chứng nhận)
        /// </summary>
        [Column("certify_id")]
        public Guid CertifyId { get; set; }
        [ForeignKey("CertifyId")]
        public virtual Certify Certify { get; set; }
    }
}
