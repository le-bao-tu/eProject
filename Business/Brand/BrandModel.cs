using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Brand
{
    public class BrandModel
    {
        public Guid IdBrand { get; set; }

        public string BrandName { get; set; }

        public bool Status { get; set; } = false;
    }

    public class PageBrandModel
    {
        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }

        public bool Status { get; set; } = false;
    }
}
