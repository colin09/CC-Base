using System;

namespace C.B.MySql.Data {

    public class AuthUser : BaseEntity {
        public string UserName { set; get; }
        public string Password { set; get; }
        public string TrueName { set; get; }
        public string MobileNo { set; get; }
        public string EMail { set; get; }
        public DateTime LastLoginTime { set; get; }
        public int Status { set; get; }
        public long AuthRoleId { set; get; }

    }

}