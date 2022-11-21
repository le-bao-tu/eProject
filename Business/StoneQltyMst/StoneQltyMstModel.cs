using System;
using System.Collections.Generic;
using System.Text;

namespace Business.StoneQltyMst
{
    public class StoneQltyMstModel
    {
        /// <summary>
        /// khóa chính 
        /// </summary>
        public Guid StoneQltyId { get; set; }

        /// <summary>
        /// chất lượng đá 
        /// </summary>
        public int StoneQlty { get; set; }

    }

    public class StoneQltyMsPageModel
    {
        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }
    }

}
