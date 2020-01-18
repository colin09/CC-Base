using System.Security.Claims;
using System.Threading.Tasks;
using C.B.Common.helper;
using C.B.Models.Data;
using C.B.Models.Enums;
using C.B.MySql.Data;
using C.B.MySql.Repository.EntityRepositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StmWeb.Area.Sys.Controllers {
    [Area ("Sys")]
    public class LoginController : Controller {
        //private readonly UserInfoRepository _userRepository;
        private readonly AuthUserRepository _authUserRepository;
        private readonly AuthRoleRepository _authRoleRepository;
        public LoginController () {
            // _userRepository = new UserInfoRepository ();
            _authUserRepository = new AuthUserRepository ();
            _authRoleRepository = new AuthRoleRepository ();
        }

        public IActionResult Index () {
            return View ();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn (string userName, string password, string verifyCode) {
            if (verifyCode.IsEmpty ())
                return Json (BaseResponse.ErrorResponse ("请填写验证码。"));
            var code = HttpContext.Session.GetString ("Session.VerifyCode");
            if (verifyCode.ToLower () != code.ToLower ())
                return Json (BaseResponse.ErrorResponse ("验证码错误。"));
            HttpContext.Session.SetString ("Session.VerifyCode", "empty-empty");

            if (userName.IsEmpty () || password.IsEmpty ())
                return Json (BaseResponse.ErrorResponse ("用户名或密码错误。"));

            // var user = _userRepository.FirstOrDefault (m => m.UserName == userName && m.Password == CryptoHelper.MD5Encrypt (password));
            var user = _authUserRepository.FirstOrDefault (m => m.UserName == userName && m.Password == CryptoHelper.MD5Encrypt (password));
            if (user == null) return Json (BaseResponse.ErrorResponse ("用户名或密码错误。"));
            var role = _authRoleRepository.FirstOrDefault (m => m.Id == user.AuthRoleId);
            if (role == null || (int) role.AuthRoleType > 1) return Json (BaseResponse.ErrorResponse ("权限不足，无法登录。"));

            System.Console.WriteLine ($"-------------> role.type : {role.AuthRoleType.ToString()}");
            //用户标识
            var identity = new ClaimsIdentity ();

            identity.AddClaim (new Claim (ClaimTypes.PrimarySid, user.Id.ToString ()));
            identity.AddClaim (new Claim (ClaimTypes.Sid, user.UserName));
            identity.AddClaim (new Claim (ClaimTypes.Name, user.TrueName));

            identity.AddClaim (new Claim (ClaimTypes.PrimaryGroupSid, user.AuthRoleId.ToString ()));
            identity.AddClaim (new Claim (ClaimTypes.Role, role.AuthRoleType.ToString ()));

            identity.AddClaim (new Claim (ClaimTypes.Gender, "0"));
            identity.AddClaim (new Claim (ClaimTypes.MobilePhone, user.MobileNo));
            identity.AddClaim (new Claim (ClaimTypes.Email, user.EMail));
            // identity.AddClaim(new Claim(ClaimTypes.UserData, user.ToJson())); 
            identity.AddClaim (new Claim (ClaimTypes.Authentication, role.ToJson ()));
            await HttpContext.SignInAsync (CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal (identity));
            return Json (BaseResponse.SuccessResponse ());
        }

        public async Task<IActionResult> SignOut () {
            await HttpContext.SignOutAsync (CookieAuthenticationDefaults.AuthenticationScheme);
            return Json (BaseResponse.SuccessResponse ());
        }

        /* AuthenticationScheme

            1.---------------------------------------------------------------------------------------
                dotnet add package Microsoft.AspNetCore.Authentication.Cookies --version 2.0.0

            2.---------------------------------------------------------------------------------------
                services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    // 在这里可以根据需要添加一些Cookie认证相关的配置，在本次示例中使用默认值就可以了。
                });

            3.---------------------------------------------------------------------------------------
                app.UseAuthentication();

            4.---------------------------------------------------------------------------------------
                var claimIdentity = new ClaimsIdentity("Cookie");
                // var claimIdentity = new ClaimsIdentity("Cookie",ClaimTypes.Name,ClaimTypes.Role);
                claimIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                claimIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
                claimIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                claimIdentity.AddClaim(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
                claimIdentity.AddClaim(new Claim(ClaimTypes.DateOfBirth, user.Birthday.ToString()));

                var claimsPrincipal = new ClaimsPrincipal(claimIdentity);
                // 在上面注册AddAuthentication时，指定了默认的Scheme，在这里便可以不再指定Scheme。
                await context.SignInAsync(claimsPrincipal);

                await HttpContext.SignInAsync("MyCookieAuthenticationScheme", principal, new AuthenticationProperties
                {
                    // 持久保存
                    IsPersistent = true
                    // 指定过期时间
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                });

         */

        /* JwtClaimTypes

            1.---------------------------------------------------------------------------------------
                dotnet add package IdentityModel --version 2.12.0

            2.---------------------------------------------------------------------------------------
                services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    // 在这里可以根据需要添加一些Cookie认证相关的配置，在本次示例中使用默认值就可以了。
                });

            3.---------------------------------------------------------------------------------------
                app.UseAuthentication();

            4.---------------------------------------------------------------------------------------
                var claimIdentity = new ClaimsIdentity("Cookie", JwtClaimTypes.Name, JwtClaimTypes.Role);
                claimIdentity.AddClaim(new Claim(JwtClaimTypes.Id, user.Id.ToString()));
                claimIdentity.AddClaim(new Claim(JwtClaimTypes.Name, user.Name));
                claimIdentity.AddClaim(new Claim(JwtClaimTypes.Email, user.Email));
                claimIdentity.AddClaim(new Claim(JwtClaimTypes.PhoneNumber, user.PhoneNumber));
                claimIdentity.AddClaim(new Claim(JwtClaimTypes.BirthDate, user.Birthday.ToString()));

                var claimsPrincipal = new ClaimsPrincipal(claimIdentity);
                // 在上面注册AddAuthentication时，指定了默认的Scheme，在这里便可以不再指定Scheme。
                await context.SignInAsync(claimsPrincipal);


         */

    }
}