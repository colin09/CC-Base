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
using C.B.Common.helper;

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
           var result = _userRepository.Where(m=>true).ToList();
            return Json (result);
        }

        //public IActionResult CreateSysUsers (string userName,string trueName,string department,int authType) {
        [HttpPost]
        public IActionResult CreateSysUsers (UserInfo model) {
            System.Console.WriteLine("CreateSysUsers ---> ");
            /*var model = new UserInfo(){
                UserName = userName,
                Password= "123456",
                TrueName= trueName,
                Department= department,
                State = 1,
                AuthType = (UserAuthType)authType,
            };*/
            System.Console.WriteLine(model.ToJson());
            var result = _userRepository.Insert(model);
            var result2 = _userRepository.Insert(new UserInfo(){
                UserName = "a",
                TrueName="a",
                Department= "a",
                State=1,
                AuthType = UserAuthType.develop,
            });
            add();
            return Json( result>0 ? BaseResponse.SuccessResponse():BaseResponse.ErrorResponse("添加失败。"));
        }






    private void add (){
        var noticeR = new NoticeRepository();
        noticeR.Insert(new Notice(){});

        var messageR = new MessageRepository();
        messageR.Insert(new Message(){});


    }



    }
}