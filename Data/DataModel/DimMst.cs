using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.DataModel
{
    [Table("dimmst")]
    public class DimMst
    {
        [Key]
        [Column("style_code")]
        public string Style_Code { get; set; }

        [StringLength(150)]
        [Column("dimmst_name")]
        public string DimMstName { get; set; }

        [Column("image")]
        public string Image { get; set; }

        /// <summary>
        /// cara của kim cương 
        /// </summary>
        [Column("dim_crt")]
        public float DimCrt { get; set; }

        /// <summary>
        /// tổng số kim cương trong mặt hàng 
        /// </summary>
        [Column("dim_pcs")]
        public float DimPcs { get; set; }

        /// <summary>
        /// trọng lượng của mỗi viên 
        /// </summary>
        [Column("dim_gm")]
        public float DimGm { get; set; }

        /// <summary>
        /// size của mỗi viên 
        /// </summary>
        [Column("dim_size")]
        public int DimSize { get; set; }

        /// <summary>
        /// chi tiết 
        /// </summary>
        [Column("detail")]
        public string Detail { get; set; }

        /// <summary>
        /// tổng tiền của tát cả bao gồm cả các loại phí 
        /// </summary>
        [Column("dim_amt")]
        public float DimAmt { get; set; }

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
        /// khóa ngoại tham chiếu đến bảng certify(chứng nhận)
        /// </summary>
        [Column("certify_id")]
        public Guid CertifyId { get; set; }
        [ForeignKey("CertifyId")]
        public virtual Certify Certify { get; set; }

        /// <summary>
        /// khóa ngoại tham chiếu đến bảng DimQltyMst (chất lượng kim cương )
        /// </summary>
        [Column("dimqlty_id")]
        public Guid DimQlty_Id { get; set; }
        [ForeignKey("DimQlty_Id")]
        public virtual DimQltyMst DimQltyMst { get; set; }

        /// <summary>
        /// khóa ngoại tham chiếu đến bảng DimQltySubMst (chất lượng loại phụ kim cương )
        /// </summary>
        [Column("dimsubtype_id")]
        public Guid Dimsubtype_Id { get; set; }
        [ForeignKey("Dimsubtype_Id")]
        public virtual DimQltySubMst DimQltySubMst { get; set; }
    }
}
