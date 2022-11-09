using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Category
{
    public class CategoryModel
    {
        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }

        public bool Status { get; set; }

        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }
    }
}
