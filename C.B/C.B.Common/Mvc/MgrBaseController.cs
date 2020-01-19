using System;
using System.Security.Claims;
using C.B.Common.helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace C.B.Common.Mvc {

    [Area ("Sys")]
    [ErrorFilter, LoggerFilter]
    public class MgrBaseController : Controller {
        private CurrentUser _currentUser = null;
        //protected CurrentUser CurrentUser => GetCurrentUser();

        protected string UserName = "";

        protected CurrentUser GetCurrentUserInfo (ClaimsPrincipal curUser) {
            if (_currentUser != null)
                return _currentUser;

            //var curUser = HttpContext.User;
            var userId = curUser.FindFirst (ClaimTypes.PrimarySid).Value.ToInt ();
            System.Console.WriteLine ($"==========> ClaimTypes.PrimarySid: {userId}");
            return new CurrentUser {
                Id = userId.HasValue ? userId.Value : 0,
                    UserName = curUser.FindFirst (ClaimTypes.Sid).Value,
                    TrueName = curUser.FindFirst (ClaimTypes.Name).Value,

                    RoleType = curUser.FindFirst (ClaimTypes.PrimaryGroupSid).Value,
                    RoleId = curUser.FindFirst (ClaimTypes.Role).Value,

                    MobileNo = curUser.FindFirst (ClaimTypes.MobilePhone).Value,
                    Email = curUser.FindFirst (ClaimTypes.Email).Value,
            };
        }

    }

    public class CurrentUser {
        public int Id { set; get; }
        public string UserName { set; get; }
        public string TrueName { set; get; }
        public string RoleType { set; get; }
        public string RoleId { set; get; }

        public string Email { set; get; }
        public string MobileNo { set; get; }
    }

}