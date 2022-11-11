using System;
using System.Collections.Generic;
using System.Text;

namespace Business.SubCategory
{
    public class SubcategoModel
    {
        public Guid SubCategory_Id { get; set; }

        public string SubCategoryName { get; set; }

        public bool Status { get; set; }

        public Guid Category_id { get; set; }
    }

    public class SubCategoryPageModel
    {
        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }

        public bool Status { get; set; }
    }
}
