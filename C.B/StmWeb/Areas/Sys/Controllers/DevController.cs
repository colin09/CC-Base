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
        private readonly AuthRoleRepository _authRoleRepository;
        private readonly AuthUserRepository _authUserRepository;
        private readonly AuthRoleNavsRepository _authRoleNavsRepository;
        private readonly AuthUserNavsRepository _authUserNavsRepository;

        public DevController () {
            _userRepository = new UserInfoRepository ();
            _authNavsRepository = new AuthNavsRepository ();
            _authRoleRepository = new AuthRoleRepository ();
            _authUserRepository = new AuthUserRepository ();
            _authRoleNavsRepository = new AuthRoleNavsRepository ();
            _authUserNavsRepository = new AuthUserNavsRepository ();
        }

        public IActionResult Index () {
            return View ();
        }

        public IActionResult AuthNavs () {
            return View ();
        }
        public IActionResult AuthUserNavs () {
            return View ();
        }
        public IActionResult AuthRoleNavs () {
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

        #region  --  Auth Nav  --
        [HttpPost]
        public IActionResult SaveAuthNav ([FromBody] AuthNavs model) {
            var result = 0;
            if (model.Id == 0)
                result = _authNavsRepository.Insert (model);
            else
                result = _authNavsRepository.Update (model);
            return Json (result > 0 ? BaseResponse.SuccessResponse () : BaseResponse.ErrorResponse ("添加失败。"));
        }

        [HttpGet]
        public IActionResult GetAuthNavs () {
            var result = _authNavsRepository.Where (m => m.IsDeleted == 0).OrderBy (m => m.Sort);
            return Json (BaseResponse.SuccessResponse (result));
        }

        #endregion

        #region  --  Auth Role Nav  --

        [HttpPost]
        public IActionResult SaveAuthRole ([FromBody] AuthRole model) {
            var result = 0;
            if (model.Id == 0)
                result = _authRoleRepository.Insert (model);
            else
                result = _authRoleRepository.Update (model);
            return Json (result > 0 ? BaseResponse.SuccessResponse () : BaseResponse.ErrorResponse ("添加失败。"));
        }

        [HttpPost]
        public IActionResult GetAuthRoleByPage ([FromBody] BasePageRequest<SearchRequest> request) {
            var result = _authRoleRepository.Where (request.pager, m => m.IsDeleted == 0, s => s.Id, true);
            return Json (BaseResponse.SuccessResponse (result));
        }

        [HttpPost]
        public IActionResult SaveAuthRoleNavs ([FromBody] List<AuthRoleNavs> models) {
            var dels = _authRoleNavsRepository.Delete (m => m.AuthRoleId == models.FirstOrDefault ().AuthRoleId);
            var result = _authRoleNavsRepository.Insert (models);
            return Json (result > 0 ? BaseResponse.SuccessResponse () : BaseResponse.ErrorResponse ("添加失败。"));
        }

        [HttpGet]
        public IActionResult GetAuthRoleNavs (long authRoleId) {
            var authRoleNavs = _authRoleNavsRepository.Where (m => m.IsDeleted == 0 && m.AuthRoleId == authRoleId);
            var navIds = authRoleNavs.Select (m => m.AuthNavId).ToList ();
            var navs = _authNavsRepository.Where (m => m.IsDeleted == 0 && navIds.Contains (m.Id));
            return Json (BaseResponse.SuccessResponse (navs.ToList ()));
        }

        #endregion

        #region  --  Auth User Nav  --

        [HttpPost]
        public IActionResult SaveAuthUser ([FromBody] AuthUser model) {
            var result = 0;
            if (model.Id == 0)
                result = _authUserRepository.Insert (model);
            else
                result = _authUserRepository.Update (model);
            return Json (result > 0 ? BaseResponse.SuccessResponse () : BaseResponse.ErrorResponse ("添加失败。"));
        }

        [HttpPost]
        public IActionResult GetAuthUserByPage ([FromBody] BasePageRequest<SearchRequest> request) {
            var result = _authUserRepository.Where (request.pager, m => m.IsDeleted == 0, s => s.Id, true);
            return Json (BaseResponse.SuccessResponse (result));
        }

        #endregion
    }
}