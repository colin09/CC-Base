using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using C.B.Common.Helper;
using C.B.Models.Data;
using C.B.MySql.Repository.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StmWeb.Models;

namespace StmWeb.Controllers {
    public class CommonController : Controller {
        private IMapper _mapper;
        private EditorService _editorService;

        public CommonController (IMapper mapper) {
            _mapper = mapper;
            _editorService = new EditorService (mapper);
        }

        public IActionResult Index () {
            return View ();
        }

        public IActionResult GetVerifyCodeImg () {
            var helper = VerifyCodeHelper.GetSingleObj ();

            var code = helper.CreateVerifyCode (VerifyCodeHelper.VerifyCodeType.MixVerifyCode);
            var base64 = helper.CreateBase64StringByImgVerifyCode (code, 100, 25);

            HttpContext.Session.SetString ("Session.VerifyCode", code);
            //var VerifyCode = HttpContext.Session.GetString ("Session.VerifyCode");

            return Json (BaseResponse.SuccessResponse (base64));
        }


        public IActionResult GetEditorInfo (string type, int id = 0) {
            var result = _editorService.GetEditorModel (type, 0, id);
            return Json (BaseResponse.SuccessResponse (result));
        }

    }
}