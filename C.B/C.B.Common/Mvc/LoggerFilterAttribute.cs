using System;
using System.Text;
using C.B.Common.helper;
using C.B.Common.logger;
using Microsoft.AspNetCore.Mvc.Filters;

namespace C.B.Common.Mvc {
    public class LoggerFilterAttribute : ActionFilterAttribute {
        /// <summary>
        /// Action方法之后调用
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted (ActionExecutedContext context) {

        }

        /// <summary>
        /// Action方法之前调用
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting (ActionExecutingContext context) {
            //throw new NotImplementedException();
            var log = Logger.Current ();

            var method = context.HttpContext.Request.Method;
            var path = context.HttpContext.Request.Path;
            var queryString = context.HttpContext.Request.QueryString;
            var args = context.ActionArguments.ToJson ();

            var action = new StringBuilder ();
            action.Append ($"[{method}] {path}");
            action.Append ($" {args}");
            log.Info (action.ToString ());
        }
    }
}