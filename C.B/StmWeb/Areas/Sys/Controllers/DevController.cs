using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using C.B.Models.Data;
using C.B.MySql.Repository.EntityRepositories;
using C.B.MySql.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StmWeb.Models;

namespace StmWeb.Area.Sys.Controllers {
    [Area ("Sys")]
    [Authorize (Roles = "develop")]
    public class DevController : Controller {

        private readonly UserInfoRepository _userRepository;

        public DevController () {
            _userRepository = new UserInfoRepository ();
        }

        public IActionResult Index () {
            return View ();
        }

        public IActionResult SysUsers () {
            return View ();
        }

        public IActionResult GetSysUsersByPage (Pager pager) {
           var result = _userRepository.Where(m=>true);
            return Json (result);
        }
        public IActionResult CreateSysUsers (UserInfo model) {
           var result = _userRepository.Insert(model);
            return  result>0 Base;
        }


    }
}