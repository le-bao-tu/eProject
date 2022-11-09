using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.DataModel
{
    [Table("cate_subcategory")]
    public class Subcategory
    {
        [Key]
        [Column("id_subcategory")]
        public Guid SubCategory_Id { get; set; }

        [StringLength(50)]
        [Column("subcategory_name")]
        public string SubCategoryName { get; set; }

        [Column("status")]
        public bool Status { get; set; }

        /// <summary>
        /// khóa ngoại tham chiếu đên bảng danh mục cha (Category)
        /// </summary>
        [Column("category_id")]
        public Guid Category_id { get; set; }
        [ForeignKey("Category_id")]
        public virtual Category Category { get; set; }
    }
}
