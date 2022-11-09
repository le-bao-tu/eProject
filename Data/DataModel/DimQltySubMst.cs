using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.DataModel
{
    [Table("dimqlty_submst")]
    public class DimQltySubMst
    {

        /// <summary>
        /// khóa chính 
        /// </summary>
        [Key]
        [Column("dimsub_type_id")]
        public Guid DimSub_TypeId { get; set; }

        /// <summary>
        /// chất lượng kim cương 
        /// </summary>
        [Column("dimqlty")]
        public int DimQlty { get; set; }
    }
}
