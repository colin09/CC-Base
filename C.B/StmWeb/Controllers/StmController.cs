using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using C.B.Common.Mvc;
using Microsoft.AspNetCore.Mvc;
using StmWeb.Models;

namespace StmWeb.Controllers
{
    public class StmController : BaseController // Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        

        public IActionResult ExpertTeam()
        {
            return View();
        }
        
        public IActionResult Prize()
        {
            return View();
        }
        
        public IActionResult Review()
        {
            return View();
        }
        
        public IActionResult Interactive()
        {
            return View();
        }
    }
}
