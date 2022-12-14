using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Comment
{
    public class CommentModel
    {
        public Guid Id { get; set; }

        public string ContentComment { get; set; }

        /// <summary>
        /// mai sẽ fix cứng enum 
        /// </summary>
        public int Feedback { get; set; }

        /// <summary>
        /// khóa ngoại tham chiếu đến bảng Account 
        /// </summary>
        public Guid Account_Id { get; set; }

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

    public class CommentPageModel
    {
        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }
    }

}
