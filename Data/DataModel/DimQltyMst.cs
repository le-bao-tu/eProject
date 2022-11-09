using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.DataModel
{
    [Table("dimqlty_mst")]
    public class DimQltyMst
    {
        /// <summary>
        /// khóa chính 
        /// </summary>
        [Key]
        [Column("dimqlty_id")]
        public Guid DimqltyId { get; set; }

        /// <summary>
        /// chất lượng kim cương 
        /// </summary>
        [Column("dim_qlty")]
        public int DimQlty { get; set; }
    }
}
