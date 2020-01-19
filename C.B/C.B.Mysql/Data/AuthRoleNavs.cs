namespace C.B.MySql.Data {

    public class AuthRoleNavs : BaseEntity {
        public long AuthRoleId { set; get; }
        public long AuthNavId { set; get; }

        public long Sort { set; get; }
    }

}