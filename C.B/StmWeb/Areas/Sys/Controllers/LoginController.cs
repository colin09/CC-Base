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
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Home/Index");

        }
    }
}
