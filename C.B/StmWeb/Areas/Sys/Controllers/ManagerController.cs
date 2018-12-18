using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StmWeb.Models;

namespace StmWeb.Area.Sys.Controllers
{
    [Area("Sys")]
    [Authorize(Roles = "develop,admin")]
    public class ManagerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ManagerController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {

            var curUser = HttpContext.User;
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            return View();
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
}