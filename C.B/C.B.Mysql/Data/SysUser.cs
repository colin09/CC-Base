using System;
using System.Collections.Generic;
using System.Text;

namespace C.B.MySql.Data
{
    public class SysUser : BaseEntity
    {

        public string UserName { set; get; }
        public string Password { set; get; }

        public string MobileNo { set; get; }
        public string Email { set; get; }
        public string Gender { set; get; }

        public string State { set; get; }
    }
}
