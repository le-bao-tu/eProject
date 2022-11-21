using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ImageProduct
{
    public class ImageProductModel
    {
        public Guid Id { get; set; }

        public IList<IFormFile> Image { get; set; }

        // khóa ngoại tham chiếu đến bảng itemProduct 
        public string StyleCodeItemProduct { get; set; }

        /// <summary>
        /// khóa ngoại tham chiếu đến bảng đá 
        /// </summary>
        public string StyleCodeStoneMst { get; set; }

        /// <summary>
        /// khóa ngoại tham chiếu đến bảng kim cương 
        /// </summary>
        public string StyleCodeDimMst { get; set; }

        public string ProId { get; set; }
    }
}
