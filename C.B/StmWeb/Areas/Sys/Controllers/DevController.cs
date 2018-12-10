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
using C.B.Models.Enums;

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

        [HttpGet]
        public IActionResult GetSysUsersByPage (Pager pager) {
           var result = _userRepository.Where(m=>true);
            return Json (result);
        }
        //public IActionResult CreateSysUsers (UserInfo model) {
        public IActionResult CreateSysUsers (string userName,string trueName,string department,int authType) {
            var model = new UserInfo(){
                UserName = userName,
                Password= "123456",
                TrueName=trueName,
                Department= department,
                State = 1,
                AuthType = (UserAuthType)authType,
            };
            System.Console.Write(model);
           var result = _userRepository.Insert(model);
            return Json( result>0 ? BaseResponse.SuccessResponse():BaseResponse.ErrorResponse("添加失败。"));
        }


    }
}