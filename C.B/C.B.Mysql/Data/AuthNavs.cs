namespace C.B.MySql.Data {

    public class AuthNavs : BaseEntity {
        public long ParentId { set; get; }
        public string NavCode { set; get; }
        public int NavType { set; get; }
        public string Title { set; get; }
        public string Desc { set; get; }
        public string Icon { set; get; }
        public string Url { set; get; }
        public string Module { set; get; }
        public string Target { set; get; }

        public long Sort { set; get; }
    }

}