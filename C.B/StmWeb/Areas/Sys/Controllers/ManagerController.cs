using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StmWeb.Models;


namespace StmWeb.Area.Sys.Controllers
{
     [Area("Sys")]
    public class ManagerController : Controller
    {
        public IActionResult Index1()
        {
            return View();
        }
    }
}
