using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace C.B.Common.Mvc
{
    public class LoggerFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Action方法之后调用
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }


        /// <summary>
        /// Action方法之前调用
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //throw new NotImplementedException();
        }
    }
}
