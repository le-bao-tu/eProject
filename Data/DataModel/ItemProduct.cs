using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.DataModel
{
    [Table("item_product")]
    public class ItemProduct
    {
        [Key]
        [Column("style_code")]
        public string StyleCode { get; set; }

        [Column("pairs")]
        public int Pairs { get; set; }

        [StringLength(150)]
        [Column("product_name")]
        public string ProductName { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("image_product")]
        public string ImageProduct { get; set; }

        /// <summary>
        /// chất lượng sản phẩm 
        /// </summary>
        [Column("pro_quality")]
        public int ProQuality { get; set; }

        /// <summary>
        /// trọng lượng vàng 
        /// </summary>
        [Column("gold_wt")]
        public float Gold_WT { get; set; }

        /// <summary>
        /// trọng lượng đá 
        /// </summary>
        [Column("stone_wt")]
        public float Stone_WT { get; set; }

        /// <summary>
        /// vàng dòng , độ nguyên chất 
        /// </summary>
        [Column("net_gold")]
        public float Net_Gold { get; set; }
        
        /// <summary>
        /// tổng trọng lượng 
        /// </summary>
        [Column("total_gross_wt")]
        public float Total_Gross_WT { get; set; }

        /// <summary>
        /// tỉ giá vàng 
        /// </summary>
        [Column("gold_rate")]
        public float Gold_Rate { get; set; }

        /// <summary>
        /// số lượng vàng trong mục 
        /// </summary>
        [Column("gold_amt")]
        public float Gold_Amt { get; set; }

        /// <summary>
        /// phí làm vàng 
        /// </summary>
        [Column("gold_making")]
        public float? Gold_Making { get; set; }

        /// <summary>
        /// phí làm đá 
        /// </summary>
        [Column("stone_making")]
        public float? Stone_Making { get; set; }


        /// <summary>
        /// các khoản phí khách
        /// </summary>
        [Column("other_making")]
        public float Other_Making { get; set; }


        /// <summary>
        /// tổng phí thực hiện 
        /// </summary>
        [Column("total_making")]
        public float Total_Making { get; set; }


        /// <summary>
        /// mô tả 
        /// </summary>
        [Column("detail")]
        public string Detail { get; set; }

        [Column("mrp")]
        public string MRP { get; set; }

        /// <summary>
        ///khóa ngoại đến bảng danh mục cha 
        /// </summary>
        [Column("category_id")]
        public Guid Category_id { get; set; }
        [ForeignKey("Category_id")]
        public virtual Category Category { get; set; }

        /// <summary>
        ///khóa ngoại tham chiếu đến bảng Brands 
        /// </summary>
        [Column("brand_id")]
        public Guid BrandId { get; set; }
        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }

        /// <summary>
        /// khóa ngoại tham chiếu đến bảng certify(chứng nhận)
        /// </summary>
        [Column("certify_id")]
        public Guid CertifyId { get; set; }
        [ForeignKey("CertifyId")]
        public virtual Certify Certify { get; set; }

        /// <summary>
        /// khóa ngoại tham chiếu đến bảng danh mục con 
        /// </summary>
        [Column("subcategory_id")]
        public Guid?  SubCategoryId { get; set; }
        [ForeignKey("SubCategoryId")]
        public virtual Subcategory Subcategory { get; set; }

        /// <summary>
        /// khóa ngoại tham chiếu đến bảng loại vàng (goldk)
        /// </summary>
        [Column("goldtype_id")]
        public Guid GoldTypeId { get; set; }
        [ForeignKey("GoldTypeId")]
        public virtual Goldk Goldk { get; set; }
    }
}
