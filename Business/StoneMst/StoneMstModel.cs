using System;
using System.Collections.Generic;
using System.Text;

namespace Business.StoneMst
{
    public class StoneMstModel
    {
        public string StyleCode { get; set; }

        public string StoneName { get; set; }

        public string Image { get; set; }

        /// <summary>
        /// trọng lượng đá mỗi viên 
        /// </summary>
        public float Stone_DM { get; set; }

        /// <summary>
        /// tổng số đá trong mặt hàng 
        /// </summary>
        public float Stone_PCS { get; set; }

        /// <summary>
        /// carat của đá 
        /// </summary>
        public float MyProperty { get; set; }

        /// <summary>
        /// tỉ lệ của mỗi viên đá 
        /// </summary>
        public float Stone_Rate { get; set; }

        /// <summary>
        /// tổng số lượng đá trong vật phẩm 
        /// </summary>
        public float Stone_Amt { get; set; }
        /// <summary>
        /// chi tiêt 
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        ///khóa ngoại đến bảng danh mục cha 
        /// </summary>
        public Guid Category_id { get; set; }

        /// <summary>
        /// khóa ngoại tham chiếu đến bảng danh mục con 
        /// </summary>
        public Guid? SubCategoryId { get; set; }

        /// <summary>
        /// khóa ngoại tham chiếu đến bảng stoneQltyMst (bảng chất lượng đá )
        /// </summary>
        public Guid StoneqltyId { get; set; }

        /// <summary>
        /// khóa ngoại tham chiếu đến bảng certify(chứng nhận)
        /// </summary>
        public Guid CertifyId { get; set; }
    }

    public class StoneMstPageModel
    {
        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }
    }

}
