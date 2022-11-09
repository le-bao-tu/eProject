using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data
{
    [Table("brand")]
    public class Brand
    {
        [Key]
        [Column("id_brand")]
        public Guid IdBrand { get; set; }

        [StringLength(50)]
        [Column("brand_name")]
        public string BrandName { get; set; }

        [Column("status")]
        public bool Status { get; set; } = false;
    }
}
