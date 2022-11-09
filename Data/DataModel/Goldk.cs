using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.DataModel
{
    [Table("goldk")]
    public class Goldk
    {
        [Key]
        [Column("goldtype_id")]
        public Guid GoldTypeId { get; set; }

        /// <summary>
        /// loại vàng 
        /// </summary>
        [Column("gold_crt")]
        public string Gold_Crt { get; set; }

        /// <summary>
        /// tỷ giá vàng 
        /// </summary>
        [Column("gold_rate")]
        public float GoldRate { get; set; }
    }
}
