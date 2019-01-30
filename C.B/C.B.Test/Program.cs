using System;
using C.B.Common.Helper;
using C.B.MySql;
using C.B.Test.MySql;

namespace C.B.Test {
    class Program {
        static void Main (string[] args) {
            Console.WriteLine ("Hello start...");

            //InitLog ();

            //GetVerifyCodeImage ();
            TakeHtml ();
        }

        static void InitLog () {

            //var log = log4net.LogManager.GetLogger (typeof (Program));

            var repository = log4net.LogManager.CreateRepository ("NETCoreRepository");
            log4net.Config.XmlConfigurator.Configure (repository, new System.IO.FileInfo ("configurations/log4net.config"));
            var log = log4net.LogManager.GetLogger (repository.Name, "NETCorelog4net");

            log.Info ("++++++++++++++++++++");
        }

        static void DbInsert () {
            using (var db = new MySqlContext ()) {
                db.UserInfo.Add (new UserInfo () { });
                db.Set<UserInfo> ().Add (new UserInfo () { });

                db.SaveChanges ();
            }
            Console.WriteLine ("DbInsert Over!");
        }

        static void RepositoryInsert () {
            var repository = new C.B.MySql.Repository.EntityRepositories.UserInfoRepository ();
            repository.Insert (new C.B.MySql.Data.UserInfo ());
            Console.WriteLine ("RepositoryInsert Over!");
        }

        static void GetVerifyCodeImage () {
            var helper = VerifyCodeHelper.GetSingleObj ();

            var code = helper.CreateVerifyCode (VerifyCodeHelper.VerifyCodeType.MixVerifyCode);
            var base64 = helper.CreateBase64StringByImgVerifyCode (code, 100, 25);
            System.Console.WriteLine (base64);
        }

        static void TakeHtml () {
            var html = "fdfdddddddddddddddd&nbsp;djdldl&rdquo;dfa";

            System.Console.WriteLine (html.TakeString (200));

            C.B.Common.logger.Logger.Current ().Info (html);
        }

    }

    public static class extension {

        public static string TakeString (this string str, int length) {
            if (string.IsNullOrEmpty (str))
                return str;
            var tem = System.Text.RegularExpressions.Regex.Replace (str, "<[^>]+>", "");
            tem = System.Text.RegularExpressions.Regex.Replace (tem, @"&[^;]*;", "");

            if (!string.IsNullOrEmpty (tem.Trim ()) && tem.Length > length)
                return tem.Substring (0, length);
            return tem;
        }
    }
}