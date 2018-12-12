﻿using System;
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
using C.B.Common.helper;

namespace StmWeb.Area.Sys.Controllers
{
    [Area("Sys")]
    [Authorize(Roles = "develop")]
    public class DevController : Controller
    {
        private readonly UserInfoRepository _userRepository;
        public DevController()
        {
            _userRepository = new UserInfoRepository();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SysUsers()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetSysUsersByPage(Pager pager)
        {
            var result = _userRepository.Where(pager, m => true, s => s.Id, true).ToList();
            return Json(BaseResponse.SuccessResponse(result));
        }


        [HttpPost]
        public IActionResult CreateSysUsers([FromBody]UserInfo model)
        {
            if (model.UserName.Length < 5)
                return Json(BaseResponse.ErrorResponse("用户名不能少于6个字母。"));
            if (_userRepository.Where(m => m.UserName == model.UserName).Count() > 0)
                return Json(BaseResponse.ErrorResponse("用户名已被使用。"));

            model.Password = CryptoHelper.MD5Encrypt("123456");
            model.Gender = 0;
            var result = _userRepository.Insert(model);
            return Json(result > 0 ? BaseResponse.SuccessResponse() : BaseResponse.ErrorResponse("添加失败。"));
        }





    }
}