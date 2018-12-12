using System;
using C.B.Test.MySql;
using C.B.MySql;

namespace C.B.Test {
    class Program {
        static void Main (string[] args) {
            Console.WriteLine ("Hello World!");
            
            RepositoryInsert ();
        }

        static void DbInsert () {
            using (var db = new MySqlContext ()) {
                db.UserInfo.Add (new UserInfo () { });
                db.Set<UserInfo>().Add (new UserInfo () { });

                db.SaveChanges();
            }
            Console.WriteLine ("DbInsert Over!");
        }

        static void RepositoryInsert () {
            var repository = new C.B.MySql.Repository.EntityRepositories.UserInfoRepository();
            repository.Insert(new C.B.MySql.Data.UserInfo());
            Console.WriteLine ("RepositoryInsert Over!");
        }
    }
}