using System;
using System.Security.Claims;
using C.B.Common.helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace C.B.Common.Mvc
{

    [ErrorFilterAttribute]
    public class BaseController : Controller
    {

        private CurrentUser _currentUser = null;
        protected CurrentUser CurrentUser => GetCurrentUserInfo();

        private CurrentUser GetCurrentUserInfo()
        {
            if (_currentUser != null)
                return _currentUser;

            var curUser = HttpContext.User;
            var userId = curUser.FindFirst(ClaimTypes.PrimarySid).Value.ToInt();
            _currentUser = new CurrentUser
            {
                Id = userId.HasValue ? userId.Value : 0,
                UserName = curUser.FindFirst(ClaimTypes.Sid).Value,
                TrueName = curUser.FindFirst(ClaimTypes.Name).Value,
                Department = curUser.FindFirst(ClaimTypes.Dsa).Value,

                MobileNo = curUser.FindFirst(ClaimTypes.MobilePhone).Value,
            };
            return _currentUser;
        }

    }


}