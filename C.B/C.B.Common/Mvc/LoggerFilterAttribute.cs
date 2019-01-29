using System;
using System.Diagnostics;
using System.Text;
using C.B.Common.helper;
using C.B.Common.logger;
using Microsoft.AspNetCore.Mvc.Filters;

namespace C.B.Common.Mvc {
    public class LoggerFilterAttribute : ActionFilterAttribute {

        private Stopwatch Stopwatch { get; set; }
        private log4net.ILog log { get; set; }
        private string args { get; set; }

        /// <summary>
        /// Action方法之后调用
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted (ActionExecutedContext context) {
            Stopwatch.Stop ();

            var host = context.HttpContext.Request.Host;
            var method = context.HttpContext.Request.Method;
            var path = context.HttpContext.Request.Path;
            var queryString = context.HttpContext.Request.QueryString;
            var response = "";

            try {
                dynamic result = context.Result.GetType ().Name == "EmptyResult" ? new { Value = "无返回结果" } : context.Result as dynamic;
                response = result == null? "": result.Value.ToJson ();
            } catch (System.Exception) {
            }

            var action = new StringBuilder ();
            action.AppendLine ($"[{method}] {host}{path}");
            action.AppendLine ($" {queryString}");
            action.AppendLine ($" {args}");
            action.AppendLine ($" ====>> ");
            action.AppendLine ($" {response}");
            action.AppendLine ($"time:{Stopwatch.Elapsed.TotalMilliseconds} ");
            log.Info (action.ToString ());
        }

        /// <summary>
        /// Action方法之前调用
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting (ActionExecutingContext context) {

            Stopwatch = new Stopwatch ();
            Stopwatch.Start ();
            var log = Logger.Current ();
            args = context.ActionArguments.ToJson ();

            /*
            var host = context.HttpContext.Request.Host;
            var method = context.HttpContext.Request.Method;
            var path = context.HttpContext.Request.Path;
            var queryString = context.HttpContext.Request.QueryString;
            var args = context.ActionArguments.ToJson ();

            var action = new StringBuilder ();
            action.AppendLine ($"[{method}] {host}{path}");
            action.AppendLine ($" {queryString}");
            action.AppendLine ($" {args}");
            action.AppendLine ($"  ");
            log.Info (action.ToString ());
            */
        }
    }
}