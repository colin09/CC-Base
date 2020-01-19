namespace C.B.MySql.Data {

    public class AuthUserNavs : BaseEntity {

        public long AuthUserId { set; get; }
        public long AuthNavId { set; get; }

        public long Sort { set; get; }
    }

}