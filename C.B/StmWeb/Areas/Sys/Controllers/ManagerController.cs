using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using C.B.Common.helper;
using C.B.Common.Mvc;
using C.B.Models.Data;
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
        public async Task<IActionResult> Index()
        {
            var curUser = HttpContext.User;
            ViewBag.UserName = curUser.FindFirst(ClaimTypes.Name).Value;

            return View();
        }


        public IActionResult GetCurrentUser()
        {
            var curUser = HttpContext.User;
            var UserName = curUser.FindFirst(ClaimTypes.Name).Value;

            return Json(BaseResponse.SuccessResponse(GetCurrentUser(curUser)));
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

    }


    public class ApplicationUser : IdentityUser
    {

    }
}