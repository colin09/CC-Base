using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using C.B.Common.helper;
using C.B.Common.Mvc;
using C.B.Models.Data;
using C.B.Models.Enums;
using C.B.MySql.Repository.EntityRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StmWeb.Models;

namespace StmWeb.Area.Sys.Controllers
{
    [Area("Sys")]
    [Authorize(Roles = "develop,admin")]
    public class ManagerController : MgrBaseController
    {
        private readonly UserInfoRepository _userRepository;
        public ManagerController()
        {
            _userRepository = new UserInfoRepository();
        }
        public IActionResult Index()
        {
            var curUser = HttpContext.User;
            ViewBag.UserName = curUser.FindFirst(ClaimTypes.Name).Value;

            return View();
        }

        public IActionResult Password()
        {
            var curUser = HttpContext.User;
            ViewBag.UserName = curUser.FindFirst(ClaimTypes.Name).Value;
            return View();
        }

        [HttpPost]
        public IActionResult ModifyPwd([FromBody]BaseRequest request)
        {
            var pwd = request.Key1;
            var newPwd = request.Key1;
            
            var curUser = HttpContext.User;
            var curUserInfo = GetCurrentUserInfo(curUser);
            var m = _userRepository.FirstOrDefault(curUserInfo.Id);
            if (m.Password != CryptoHelper.MD5Encrypt(pwd))
                return Json(BaseResponse.ErrorResponse("原始密码错误。"));
            m.Password = CryptoHelper.MD5Encrypt(newPwd);
            _userRepository.Update(m);
            return Json(BaseResponse.SuccessResponse());
        }


        public IActionResult GetCurrentUser()
        {
            var curUser = HttpContext.User;
            var UserName = curUser.FindFirst(ClaimTypes.Name).Value;

            return Json(BaseResponse.SuccessResponse(GetCurrentUserInfo(curUser)));
        }


        public IActionResult EditerBasic()
        {
            return View();
        }


        public IActionResult EditerStandard()
        {
            return View();
        }


        public IActionResult EditerFull()
        {
            return View();
        }

        public IActionResult VideoPlay()
        {
            return View();
        }

        public IActionResult MgrUsers()
        {
            return View();
        }

        public IActionResult GetUsersByPage(Pager pager)
        {
            var userAuths = new UserAuthType[] { UserAuthType.admin, UserAuthType.user };
            var result = _userRepository.Where(pager, m => m.IsDeleted == 0 && userAuths.Contains(m.AuthType), s => s.Id, true).ToList();
            return Json(BaseResponse.SuccessResponse(result));
        }


    }


    public class ApplicationUser : IdentityUser
    {

    }
}