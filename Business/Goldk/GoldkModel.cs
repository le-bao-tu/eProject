using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Goldk
{
    public class GoldkModel
    {
        public Guid GoldTypeId { get; set; }

        public string Gold_Crt { get; set; }

        public float GoldRate { get; set; }
    }

    public class GoldkPageModel
    {
        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }
    }
}
