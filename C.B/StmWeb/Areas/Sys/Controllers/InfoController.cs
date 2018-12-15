using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StmWeb.Models;

namespace StmWeb.Area.Sys.Controllers
{
    [Area("Sys")]
    [Authorize(Roles = "develop,admin")]
    public class InfoController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult New()
        {
            return View();
        }
        
        public IActionResult Edit()
        {
            return View();
        }


        public IActionResult Event()
        {
            return View();
        }
        public IActionResult Expert()
        {
            return View();
        }
    }
}