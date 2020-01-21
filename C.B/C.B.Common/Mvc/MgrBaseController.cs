using System;
using System.Security.Claims;
using C.B.Common.helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace C.B.Common.Mvc {

    [Area ("Sys")]
    [MgrErrorFilter, LoggerFilter]
    public class MgrBaseController : Controller {
        protected CurrentUser currentUser = null;

        protected string UserName = "";

        public MgrBaseController () {
            // currentUser = GetCurrentUserInfo ();
        }

        // protected CurrentUser GetCurrentUserInfo (HttpContext httpContext) {
        //     return GetCurrentUserInfo (httpContext.User);
        // }

        protected CurrentUser GetCurrentUserInfo (ClaimsPrincipal curUser) {
            var userId = curUser.FindFirst (ClaimTypes.PrimarySid).Value.ToInt ();
            if (userId.HasValue)
                System.Console.WriteLine ($"==========> ClaimTypes.PrimarySid: {userId}");

            return new CurrentUser {
                Id = userId.HasValue ? userId.Value : 0,
                    UserName = curUser.FindFirst (ClaimTypes.Sid).Value,
                    TrueName = curUser.FindFirst (ClaimTypes.Name).Value,

                    RoleId = Convert.ToInt64 (curUser.FindFirst (ClaimTypes.PrimaryGroupSid).Value),
                    RoleType = curUser.FindFirst (ClaimTypes.Role).Value,

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
        public long RoleId { set; get; }

        public string Email { set; get; }
        public string MobileNo { set; get; }
    }

}