using Nest;

namespace C.B.Search.Data {
    public class EsContent : ESBase {

        
        [Text (Name = "title", Analyzer = "ik_smart")]
        public string Title { set; get; }

        [Text (Name = "content", Analyzer = "ik_smart")]
        public string Content { set; get; }

        [Text (Name = "author", Analyzer = "ik_smart")]
        public string Author { set; get; }
        public string Url { set; get; }

    }
}