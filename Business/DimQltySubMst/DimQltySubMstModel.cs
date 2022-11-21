using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DimQltySubMst
{
    public class DimQltySubMstModel
    {
        /// <summary>
        /// khóa chính 
        /// </summary>
        public Guid DimSub_TypeId { get; set; }

        /// <summary>
        /// chất lượng kim cương 
        /// </summary>
        public int DimQlty { get; set; }
    }

    public class DimQltySubMstPageModel
    {
        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }
    }
}
