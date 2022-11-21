using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ItemDimMst
{
    public class DimMstModel
    {
        public string Style_Code { get; set; }

        public string DimMstName { get; set; }

        public string Image { get; set; }

        /// <summary>
        /// cara của kim cương 
        /// </summary>
        public float DimCrt { get; set; }

        /// <summary>
        /// tổng số kim cương trong mặt hàng 
        /// </summary>
        public float DimPcs { get; set; }

        /// <summary>
        /// trọng lượng của mỗi viên 
        /// </summary>
        public float DimGm { get; set; }

        /// <summary>
        /// size của mỗi viên 
        /// </summary>
        public int DimSize { get; set; }

        /// <summary>
        /// chi tiết 
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// tổng tiền của tát cả bao gồm cả các loại phí 
        /// </summary>
        public float DimAmt { get; set; }

        /// <summary>
        ///khóa ngoại đến bảng danh mục cha 
        /// </summary>
        public Guid Category_id { get; set; }

        /// <summary>
        /// khóa ngoại tham chiếu đến bảng danh mục con 
        /// </summary>
        public Guid? SubCategoryId { get; set; }

        /// <summary>
        /// khóa ngoại tham chiếu đến bảng certify(chứng nhận)
        /// </summary>
        public Guid CertifyId { get; set; }

        /// <summary>
        /// khóa ngoại tham chiếu đến bảng DimQltyMst (chất lượng kim cương )
        /// </summary>
        public Guid DimQlty_Id { get; set; }

        /// <summary>
        /// khóa ngoại tham chiếu đến bảng DimQltySubMst (chất lượng loại phụ kim cương )
        /// </summary>
        public Guid Dimsubtype_Id { get; set; }
    }

    public class DimMstPageModel
    {
        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }
    }


}
