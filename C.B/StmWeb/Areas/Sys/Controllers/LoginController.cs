using C.B.Models.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StmWeb.Area.Sys.Controllers
{
    [Area("Sys")]
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }







        [HttpPost]
        public async Task<IActionResult> SignIn(string userName, string password)
        {
            var json = new
            {
                UserName = userName,
                Password = password,
            };

            var authSuccess = true;
            if (authSuccess)
            {
                //用户标识
                var identity = new ClaimsIdentity();
                identity.AddClaim(new Claim(ClaimTypes.Sid, userName));
                identity.AddClaim(new Claim(ClaimTypes.Name, userName));
                identity.AddClaim(new Claim(ClaimTypes.Role, "system"));

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                //return Redirect("/Sys/Manager/Index");
                return Json(BaseResponse.SuccessResponse());
            }
            return Json(BaseResponse.ErrorResponse("用户名或密码错误。"));
        }

        public async Task<IActionResult> SignOut()
        {
            //HttpContext.User.Identity.Name;
            //HttpContext.User.Identity.AuthenticationType
            //HttpContext.User.Claims.Select(c => new string[] { c.Type, c.Value })
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Home/Index");
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
