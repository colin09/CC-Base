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
    [Authorize (Roles = "develop")]
    public class DevController : Controller {
        
        public IActionResult Index () {
            return View ();
        }


        public IActionResult SysUsers () {
            return View ();
        }

    }
}