using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DimQltyMst
{
    public class DimQltyMstModel
    {
        /// <summary>
        /// khóa chính 
        /// </summary>
        public Guid DimqltyId { get; set; }

        /// <summary>
        /// chất lượng kim cương 
        /// </summary>
        public int DimQlty { get; set; }
    }

    public class DimQltyMstPageModel
    {
        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }
    }
}
