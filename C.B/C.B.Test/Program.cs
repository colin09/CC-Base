using System;
using System.Linq;
using C.B.Common.Helper;
using C.B.MySql;
using C.B.Test.MySql;

namespace C.B.Test {
    class Program {
        static void Main (string[] args) {
            Console.WriteLine ("Hello start...");

            // GetVerifyCodeImage ();
            //SqliteRepositoryInsert();
            EsIndex ();
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

        static void SqliteRepositoryInsert () {
            var repository = new C.B.Sqlite.Repository.EntityRepositories.UserInfoRepository ();
            repository.Insert (new C.B.Sqlite.Data.UserInfo ());
            Console.WriteLine ("RepositoryInsert Over!");

            var users = repository.Where (m => true).ToList ();
            Console.WriteLine (users.Count ());

        }

        static void EsIndex () {
            var service = new C.B.Search.Services.ESSettingService ();
            service.AutoMap ();
            service.IndexDocument ();
        }

    }
}