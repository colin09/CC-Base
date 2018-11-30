using C.B.Common.Config;
using C.B.Common.helper;
using C.B.Common.logger;
using System.Web;
using System.Web.Mvc;

namespace C.B.Common.mvc.attribute
{
    public class UserAuthorizeAttribute: AuthorizeAttribute
    {

        private readonly string cookieName = "COOKIE_MANAGER_INFO";

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            Logger.Current().Info("<HR />");
            var flag = false;
            var cookie = HttpHelper.GetCookie(Public_const_Enum.LonginCookieName);
            if (!string.IsNullOrEmpty(cookie))
                flag = true;
            return flag;
        }




        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var returnUri = filterContext.RequestContext.HttpContext.Request.Url;
            Logger.Current().Info("获取用户Cookie失败，跳转到登录。");
            
                filterContext.Result = new RedirectResult("~/Account/Login");
        }
    }
}
