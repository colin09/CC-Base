using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StmWeb.Models;

namespace StmWeb.Area.Sys.Controllers {
    [Area ("Sys")]
    [Authorize (Authentication = "develop,admin")]
    public class ManagerController : Controller {
        
        public IActionResult Index () {
            return View ();
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

    }
}