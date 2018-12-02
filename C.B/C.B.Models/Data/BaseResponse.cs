using System;
using System.Collections.Generic;
using System.Text;

namespace C.B.Models.Data
{
    public class BaseResponse
    {

        public BaseResponse(bool success = true, string message = "")
        {
            this.Success = success;
            this.Message = message;
        }


        public bool Success { set; get; }
        public string Message { set; get; }
        public string Code { set; get; }


        public static BaseResponse SuccessResponse() => new BaseResponse();
        public static BaseResponse ErrorResponse(string message) => new BaseResponse(false, message);

        public static BaseResponse<T> SuccessResponse<T>(T data, bool success = true) where T : class
        {
            return new BaseResponse<T>
            {
                Data = data,
                Success = success,
            };
        }

    }

    public class BaseResponse<T> : BaseResponse where T : class
    {
        public T Data { set; get; }
    }
}
