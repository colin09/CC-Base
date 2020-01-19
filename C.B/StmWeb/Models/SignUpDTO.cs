using System;
using System.Text.RegularExpressions;
using C.B.Common.helper;

namespace StmWeb.Models {
    public class SignUpDTO {
        public string userName { get; set; }
        public string trueName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string mobileNo { get; set; }

        public string Validate () {
            if (userName.IsEmpty ()) return "用户名不能为空。";
            if (password.IsEmpty ()) return "密码不能为空。";
            if (email.IsEmpty ()) return "邮箱不能为空。";

            if (userName.Length < 6) return "用户名。";
            if (userName.Length < 6) return "密码不能少于6位。";

            var emailReg = @"^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z0-9]{2,6}$";
            var regex = new Regex (emailReg);
            if (!regex.IsMatch (email)) return "邮箱格式错误。";

            var mobilReg = @"/^1\d{11}$/";
            regex = new Regex (mobilReg);
            if (mobileNo.IsNotEmpty () && !regex.IsMatch (mobileNo)) return "联系电话错误。";

            return null;
        }

    }
}