using Nest;

namespace C.B.Search.Data
{
    public class EventInfoES
    {
        public string Id { set; get; }

        [Text(Name = "title", Analyzer = "ik_smart")]
        public string Title { set; get; }

        [Text(Name = "content", Analyzer = "ik_smart")]
        public string Content { set; get; }

        [Text(Name = "author", Analyzer = "ik_max_word")]
        public string Author { set; get; }

        public int SortNo { set; get; }
    }

}