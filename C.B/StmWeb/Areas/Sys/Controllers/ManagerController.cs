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
    [Authorize (Roles = "system")]
    public class ManagerController : Controller {
        [Authorize (Roles = "system")]
        public IActionResult Index () {
            return View ();
        }

        [Authorize (Roles = "system")]
        public IActionResult EditerBasic () {
            return View ();
        }

        [Authorize (Roles = "system")]
        public IActionResult EditerStandard () {
            return View ();
        }

        [Authorize (Roles = "system")]
        public IActionResult EditerFull () {
            return View ();
        }

        [Authorize (Roles = "system")]
        public IActionResult VideoPlay () {
            return View ();
        }

    }
}