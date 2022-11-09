using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.DataModel
{
    [Table("certify")]
    public class Certify
    {
        [Key]
        [Column("id_certify")]
        public Guid IdCertify { get; set; }

        [StringLength(50)]
        [Column("certify_type")]
        public string CertifyType { get; set; }

        [Column("image_certify")]
        public string ImageCertify { get; set; }
    }
}
