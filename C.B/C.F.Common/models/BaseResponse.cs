using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C.F.Common.models
{

    public class BaseResponse
    {
        public BaseResponse() { }
        public BaseResponse(string message, bool success = true)
        {
            this.message = message;
            this.success = success;
        }

        public bool success { get; set; }
        public string message { get; set; }

        public static BaseResponse Success => new BaseResponse("");
        public static BaseResponse Error(string message) => new BaseResponse(message);
    }

    public class BaseResponse<T> : BaseResponse
    {
        public BaseResponse() { }
        public BaseResponse(T data)
        {
            this.success = true;
            this.data = data;
        }
        public T data { get; set; }

    }
}
