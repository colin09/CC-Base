using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using C.B.Common.Mvc;
using Microsoft.AspNetCore.Mvc;
using StmWeb.Models;

namespace StmWeb.Controllers {
    public class HomeController : BaseController // Controller
    {
        private readonly AuthUserRepository _authUserRepository;
        private readonly AuthRoleRepository _authRoleRepository;

        public UserController () {
            _authUserRepository = new AuthUserRepository ();
            _authRoleRepository = new AuthRoleRepository ();
        }

        public IActionResult Index () {
            var log = C.B.Common.logger.Logger.Current ();
            log.Info ("111111111111111111");

            return View ();
        }

        public async Task<IActionResult> SignIn (string name, string passwprd) {
            if (verifyCode.IsEmpty ())
                return Json (BaseResponse.ErrorResponse ("请填写验证码。"));
            var code = HttpContext.Session.GetString ("Session.SignInValidateCode");
            if (verifyCode.ToLower () != code.ToLower ())
                return Json (BaseResponse.ErrorResponse ("验证码错误。"));
            HttpContext.Session.SetString ("Session.SignInValidateCode", "empty-empty");

            if (userName.IsEmpty () || password.IsEmpty ())
                return Json (BaseResponse.ErrorResponse ("用户名或密码错误。"));

            var user = _authUserRepository.FirstOrDefault (m => m.UserName == userName && m.Password == CryptoHelper.MD5Encrypt (password));
            if (user == null) return Json (BaseResponse.ErrorResponse ("用户名或密码错误。"));
            // var role = _authRoleRepository.FirstOrDefault (m => m.Id == user.AuthRoleId);
            // if (role == null) return Json (BaseResponse.ErrorResponse ("权限不足，无法登录。"));

            //用户标识
            var identity = new ClaimsIdentity ();

            identity.AddClaim (new Claim (ClaimTypes.PrimarySid, user.Id.ToString ()));
            identity.AddClaim (new Claim (ClaimTypes.Sid, user.UserName));
            identity.AddClaim (new Claim (ClaimTypes.Name, user.TrueName));

            identity.AddClaim (new Claim (ClaimTypes.PrimaryGroupSid, user.AuthRoleId.ToString ()));
            identity.AddClaim (new Claim (ClaimTypes.Role, "registered"));

            identity.AddClaim (new Claim (ClaimTypes.Gender, "0"));
            identity.AddClaim (new Claim (ClaimTypes.MobilePhone, user.MobileNo));
            identity.AddClaim (new Claim (ClaimTypes.Email, user.EMail));
            // identity.AddClaim(new Claim(ClaimTypes.UserData, user.ToJson())); 
            // identity.AddClaim (new Claim (ClaimTypes.Authentication, role.ToJson ()));
            await HttpContext.SignInAsync (CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal (identity));
            return Json (BaseResponse.SuccessResponse ());
        }

        [HttpPost]
        public async Task<IActionResult> SignUp ([FromBody] SignUpDTO model) {
            var validateMsg = model.Validate ();
            if (validateMsg.IsNotEmpty ()) return Json (BaseResponse.ErrorResponse (validateMsg));

            var user = _authUserRepository.FirstOrDefault (m => m.UserName == model.userName);
            if (user != null) return Json (BaseResponse.ErrorResponse ("用户名已存在。"));

            var user = _authUserRepository.FirstOrDefault (m => m.EMail == model.email);
            if (user != null) return Json (BaseResponse.ErrorResponse ("邮箱地址已存在。"));

            var code = HttpContext.Session.GetString ("Session.SignUpValidateCode");
            HttpContext.Session.SetString ("Session.SignInValidateCode", "empty-empty");
        }

        public async Task<IActionResult> SendValidateCode (string email) {
            var helper = VerifyCodeHelper.GetSingleObj ();
            var code = helper.CreateVerifyCode (VerifyCodeHelper.VerifyCodeType.MixVerifyCode);

            MailHelper.SendMail ("注册验证码", email, code);
            HttpContext.Session.SetString ("Session.SignUpValidateCode", code);
        }

        public async Task<IActionResult> SignOut () {
            await HttpContext.SignOutAsync (CookieAuthenticationDefaults.AuthenticationScheme);
            return Json (BaseResponse.SuccessResponse ());
        }
    }
}