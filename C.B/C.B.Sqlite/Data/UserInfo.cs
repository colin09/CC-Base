namespace C.B.Sqlite.Data {
    using C.B.Models.Data;

    public class UserInfo :BaseEntity {
        
        public string UserName { set; get; }
        public string TrueName { set; get; }
        public string Department { set; get; }

        public string Gender { set; get; }
        public string Email { set; get; }
        public string MobileNo { set; get; }
    }
}