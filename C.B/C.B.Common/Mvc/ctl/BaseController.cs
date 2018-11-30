using C.B.Common.Config;
using C.B.Common.helper;
using C.B.Common.logger;
using C.B.Common.mvc.attribute;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Mvc;

namespace C.B.Common.mvc.ctl
{

    [ErrorFilter]
    public class BaseController : System.Web.Mvc.Controller
    {

        protected ILog log { get; private set; }

        protected ViewResult View()
        {
            return base.View();
        }
        protected ViewResult View(object userModel)
        {
            return base.View(userModel);
        }

        protected BaseController()
        {
            log = Logger.Current();
            var cookie = HttpHelper.GetCookie(Public_const_Enum.LonginCookieName);
         
            if (!string.IsNullOrEmpty(cookie))
            {
                var user = (JObject)JsonConvert.DeserializeObject(cookie);
                if (user != null)
                {
                  
                }
            }
        }
    }
}
