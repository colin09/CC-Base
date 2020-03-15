using System;
using System.Linq;
using System.Security.Claims;
using C.B.Common.helper;
using C.B.Common.Mvc;
using C.B.Models.Data;
using C.B.Models.Enums;
using C.B.MySql.Repository.EntityRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace StmWeb.Area.Sys.Controllers {
    [Area ("Sys")]
    [Authorize (Roles = "system,admin")]
    public class ManagerController : MgrBaseController {
        private readonly UserInfoRepository _userRepository;
        private readonly AuthNavsRepository _authNavsRepository;
        private readonly AuthUserRepository _authUserRepository;
        private readonly AuthRoleNavsRepository _authRoleNavsRepository;
        private readonly AuthUserNavsRepository _authUserNavsRepository;

        public ManagerController () : base () {

            _userRepository = new UserInfoRepository ();
            _authNavsRepository = new AuthNavsRepository ();
            _authUserRepository = new AuthUserRepository ();
            _authRoleNavsRepository = new AuthRoleNavsRepository ();
            _authUserNavsRepository = new AuthUserNavsRepository ();
        }
        public IActionResult Index () {
            var curUser = HttpContext.User;
            ViewBag.UserName = curUser.FindFirst (ClaimTypes.Name).Value;

            return View ();
        }

        public IActionResult Password () {
            var curUser = HttpContext.User;
            ViewBag.UserName = curUser.FindFirst (ClaimTypes.Name).Value;
            return View ();
        }

        [HttpPost]
        public IActionResult ModifyPwd ([FromBody] BaseRequest request) {
            var pwd = request.Key1;
            var newPwd = request.Key1;

            // var curUser = HttpContext.User;
            // var curUserInfo = GetCurrentUserInfo (curUser);
            var curUserInfo = GetCurrentUserInfo (HttpContext.User);
            var m = _userRepository.FirstOrDefault (curUserInfo.Id);
            if (m.Password != CryptoHelper.MD5Encrypt (pwd))
                return Json (BaseResponse.ErrorResponse ("原始密码错误。"));
            m.Password = CryptoHelper.MD5Encrypt (newPwd);
            _userRepository.Update (m);
            return Json (BaseResponse.SuccessResponse ());
        }

        public IActionResult GetCurrentUser () {
            // var curUser = HttpContext.User;
            // var UserName = curUser.FindFirst (ClaimTypes.Name).Value;

            var curUserInfo = GetCurrentUserInfo (HttpContext.User);
            return Json (BaseResponse.SuccessResponse (curUserInfo));
        }

        public IActionResult EditerBasic () {
            return View ();
        }

        public IActionResult EditerStandard () {
            return View ();
        }

        public IActionResult EditerFull () {
            return View ();
        }

        public IActionResult VideoPlay () {
            return View ();
        }

        public IActionResult MgrUsers () {
            return View ();
        }

        public IActionResult GetUsersByPage (Pager pager) {
            var userAuths = new UserAuthType[] { UserAuthType.admin, UserAuthType.user };
            var result = _userRepository.Where (pager, m => m.IsDeleted == 0 && userAuths.Contains (m.AuthType), s => s.Id, true).ToList ();
            return Json (BaseResponse.SuccessResponse (result));
        }

        [HttpGet]
        public IActionResult GetUserNavs () {
            var user = GetCurrentUserInfo (HttpContext.User);
            var roleNavs = _authRoleNavsRepository.Where (m => m.AuthRoleId == user.RoleId);
            var navIds = roleNavs.Select (m => m.AuthNavId).ToArray ();
            var navs = _authNavsRepository.Where (m => navIds.Contains (m.Id)).ToList ();

            if (user.RoleType == "system")
                navs = _authNavsRepository.Where (m => m.IsDeleted == 0).OrderBy (m => m.Sort).ToList ();

            return Json (BaseResponse.SuccessResponse (navs));
        }

    }

    public class ApplicationUser : IdentityUser {

    }
}