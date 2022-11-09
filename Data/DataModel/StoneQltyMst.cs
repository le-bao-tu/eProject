using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.DataModel
{
    [Table("stone")]
    public class StoneQltyMst
    {

        /// <summary>
        /// khóa chính 
        /// </summary>
        [Key]
        [Column("stoneqlty_id")]
        public Guid StoneQltyId { get; set; }

        /// <summary>
        /// chất lượng đá 
        /// </summary>
        [Column("stoneqlty")]
        public int StoneQlty { get; set; }

    }
}
