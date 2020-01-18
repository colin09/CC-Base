using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using C.B.Common.helper;
using C.B.Models.Data;
using C.B.Models.Enums;
using C.B.MySql.Data;
using C.B.MySql.Repository.EntityRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StmWeb.Models;

namespace StmWeb.Area.Sys.Controllers {
    [Area ("Sys")]
    [Authorize (Roles = "system")]
    public class DevController : Controller {
        private readonly UserInfoRepository _userRepository;
        private readonly AuthNavsRepository _authNavsRepository;
        public DevController () {
            _userRepository = new UserInfoRepository ();
        }

        public IActionResult Index () {
            return View ();
        }

        public IActionResult Navs () {
            return View ();
        }

        public IActionResult SysUsers () {
            return View ();
        }

        [HttpGet]
        public IActionResult GetSysUsersByPage (Pager pager) {
            var result = _userRepository.Where (pager, m => true, s => s.Id, true).ToList ();
            return Json (BaseResponse.SuccessResponse (result));
        }

        [HttpPost]
        public IActionResult CreateSysUsers ([FromBody] UserInfo model) {
            if (model.UserName.Length < 5)
                return Json (BaseResponse.ErrorResponse ("用户名不能少于6个字母。"));
            if (_userRepository.Where (m => m.UserName == model.UserName).Count () > 0)
                return Json (BaseResponse.ErrorResponse ("用户名已被使用。"));

            model.Password = CryptoHelper.MD5Encrypt ("123456");
            model.Gender = 0;
            var result = _userRepository.Insert (model);
            return Json (result > 0 ? BaseResponse.SuccessResponse () : BaseResponse.ErrorResponse ("添加失败。"));
        }

        public IActionResult SaveNav ([FromBody] AuthNavs model) {
            var result = 0;
            if (model.Id == 0)
                result = _authNavsRepository.Insert (model);
            else
                result = _authNavsRepository.Update (model);
            return Json (result > 0 ? BaseResponse.SuccessResponse () : BaseResponse.ErrorResponse ("添加失败。"));
        }

    }
}