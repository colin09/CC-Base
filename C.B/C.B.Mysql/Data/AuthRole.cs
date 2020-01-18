namespace C.B.MySql.Data {

    public class AuthRole : BaseEntity {

        ///<summary>
        /// 角色类别 0 系统初始角色，1 后台管理角色，2 前台注册角色
        ///</summary>
        public AuthRoleType AuthRoleType { set; get; }
        public string RoleName { set; get; }
        public string RoleCode { set; get; }

        public long Sort { set; get; }
    }

    public enum AuthRoleType {
        system = 0,
        admin = 1,
        guest = 2,
    }

}