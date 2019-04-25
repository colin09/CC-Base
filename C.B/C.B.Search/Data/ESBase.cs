using System;

namespace C.B.Search.Data {
    public class ESBase {
        
        public string Id { set; get; }

        
        public DateTime CreateTime { set; get; } = DateTime.Now;
        
        public DateTime UpdateTime { set; get; } = DateTime.Now;
    }
}