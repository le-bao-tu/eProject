using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ItemProduct
{
    public class ItemProductModel
    {
        public string StyleCode { get; set; }

        public int Pairs { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public IFormFile Image { get; set; }

        public IList<IFormFile> imageProduct { get; set; }
        /// <summary>
        /// chất lượng sản phẩm 
        /// </summary>
        public int ProQuality { get; set; }

        /// <summary>
        /// trọng lượng vàng 
        /// </summary>
        public float Gold_WT { get; set; }

        /// <summary>
        /// trọng lượng đá 
        /// </summary>
        public float Stone_WT { get; set; }

        /// <summary>
        /// vàng dòng , độ nguyên chất 
        /// </summary>
        public float Net_Gold { get; set; }

        /// <summary>
        /// tổng trọng lượng 
        /// </summary>
        public float Total_Gross_WT { get; set; }

        /// <summary>
        /// tỉ giá vàng 
        /// </summary>
        public float Gold_Rate { get; set; }

        /// <summary>
        /// số lượng vàng trong mục 
        /// </summary>
        public float Gold_Amt { get; set; }

        /// <summary>
        /// phí làm vàng 
        /// </summary>
        public float? Gold_Making { get; set; }

        /// <summary>
        /// phí làm đá 
        /// </summary>
        public float? Stone_Making { get; set; }

        /// <summary>
        /// các khoản phí khách
        /// </summary>
        public float Other_Making { get; set; }

        /// <summary>
        /// tổng phí thực hiện 
        /// </summary>
        public float Total_Making { get; set; }

        /// <summary>
        /// mô tả 
        /// </summary>
        public string Detail { get; set; }

        public string MRP { get; set; }

        /// <summary>
        ///khóa ngoại đến bảng danh mục cha 
        /// </summary>
        public Guid Category_id { get; set; }

        /// <summary>
        ///khóa ngoại tham chiếu đến bảng Brands 
        /// </summary>
        public Guid BrandId { get; set; }

        /// <summary>
        /// khóa ngoại tham chiếu đến bảng certify(chứng nhận)
        /// </summary>
        public Guid CertifyId { get; set; }

        /// <summary>
        /// khóa ngoại tham chiếu đến bảng danh mục con 
        /// </summary>
        public Guid? SubCategoryId { get; set; }

        /// <summary>
        /// khóa ngoại tham chiếu đến bảng loại vàng (goldk)
        /// </summary>
        public Guid GoldTypeId { get; set; }

      
    }

    public class ItemProductPageModel
    {
        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }
    }
}
