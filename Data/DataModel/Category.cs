using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.DataModel
{
    public class Category
    {
        [Key]
        [Column("category_id")]
        public Guid CategoryId { get; set; }

        [StringLength(50)]
        [Column("category_name")]
        public string  CategoryName { get; set; }

        [Column("status")]
        public bool Status { get; set; }
    }
}
