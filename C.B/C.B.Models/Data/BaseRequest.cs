using System;
using System.Collections.Generic;
using System.Text;

namespace C.B.Models.Data {
    public class BaseRequest {
        public int Num1 { set; get; }
        public int Num2 { set; get; }
        public int Num3 { set; get; }

        public string Key1 { set; get; }
        public string Key2 { set; get; }
        public string Key3 { set; get; }
        public string Key4 { set; get; }

        public bool Flag1 { set; get; }
    }

    public class BasePageRequest : BaseRequest {
        public Pager Pager { set; get; }

    }

    public class SearchRequest {
        public int Num1 { set; get; }
        public int Num2 { set; get; }
        public int Num3 { set; get; }

        public string Key1 { set; get; }
        public string Key2 { set; get; }
        public string Key3 { set; get; }
        public string Key4 { set; get; }

        public bool Flag1 { set; get; }
        public bool Flag2 { set; get; }
        public bool Flag3 { set; get; }
    }

    public class BasePageRequest<T> {
        public Pager pager { set; get; }
        public T data { set; get; }
    }
}