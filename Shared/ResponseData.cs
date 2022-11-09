using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public class Response
    {
        public Code Code { get; set; } = Code.Success;

        public string Message { get; set; } = "Thành công";

        public Response(Code code, string message)
        {
            Code = code;
            Message = message;
        }

        public Response(string message)
        {
            Message = message;
        }

        public Response()
        {
        }
    }

    /// <summary>
    /// Trả về lỗi 
    /// </summary>

    public class ResponseError : Response
    {
        public IList<Dictionary<string, string>> ErrorDetail { get; set; }

        public ResponseError(Code code, string message, IList<Dictionary<string, string>> errorDetail = null) : base(code, message)
        {
            ErrorDetail = errorDetail;
        }
    }

    /// <summary>
    /// Trả về đối tượng 
    /// </summary>
    public class ResponseObject<T> : Response
    {
        /// <summary>
        ///     Dữ liệu trả về
        /// </summary>
        public T Data { get; set; }

        public ResponseObject(T data)
        {
            Data = data;
        }

        public ResponseObject(T data, string message)
        {
            Data = data;
            Message = message;
        }

        public ResponseObject(T data, string message, Code code)
        {
            Code = code;
            Data = data;
            Message = message;
        }
    }

    public class ResponseErrorLogin : Response
    {
        public int CountErroe { get; set; }

        public ResponseErrorLogin(Code code, string message, int countError)
        {
            Code = code;
            Message = message;
            CountErroe = countError;
        }
    }

    /// <summary>
    /// max loi
    /// </summary>
    public enum Code
    {
        Success = 200, // OK
        Created = 201, // xác nhận trạng thái đã hoạt động 
        BadRequest = 400, // lỗi dữ liệu do người dùng chuyền lên 
        Unauthorized = 401, // từ chối truy cập khi header ko chứa mã xác thực (Token) 
        Forbidden = 403, // từ chối người dùng truy cập vào API này 
        NotFound = 404, // không tìm thấy đường dẫn trên API 
        MethodNotAllowed = 405, // truy cập file không dược cho phép 
        Conflict = 409, // quá nhiều yêu cầu cho một file 
        ServerError = 500 // Server Error 
    }
}
