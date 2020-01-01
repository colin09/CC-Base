using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C.B.WorkApi.Utinity
{
    public static class Extensions
    {
        public static string TakeString(this string html, int length)
        {
            if (string.IsNullOrEmpty(html))
                return html;
            var tem = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "");
            tem = System.Text.RegularExpressions.Regex.Replace(tem, @"&[^;]*;", "");

            if (!string.IsNullOrEmpty(tem.Trim()) && tem.Length > length)
                return tem.Substring(0, length);
            return tem;
        }
    }
}