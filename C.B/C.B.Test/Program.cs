using System;
using System.Collections.Generic;
using System.Linq;
using C.B.Common.Helper;
using C.B.Models.Data;
using C.B.Mongo.service;
using C.B.MySql;
using C.B.Search.Data;
using C.B.Search.Services;
using C.B.Test.MySql;

namespace C.B.Test {
    class Program {
        static void Main (string[] args) {
            Console.WriteLine ("Hello start...");

            // GetVerifyCodeImage ();
            //SqliteRepositoryInsert();
            //EsIndex ();
            // Mg2Es ();
            SearchEs ();
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

        static void Mg2Es () {
            var mgService = new MgContentService ();
            var esService = new EsContentSevcice ();

            // var content = new EsContent { Id = "16tyjty", Title = "Title", Content = "Content", Author = "author", Url = "url" };
            // esService.Index (content);

            int pageIndex = 1, pageSize = 50;
            var pageCount = 0;
            do {
                var mgList = mgService.SearchPage (new Models.Data.Pager { PageIndex = pageIndex, PageSize = pageSize });
                pageCount = mgList.Count ();

                if (pageCount > 0) {
                    var esList = new List<EsContent> ();
                    mgList.ForEach (m => {
                        esList.Add (new EsContent {
                            Id = m.StringId,
                                Title = m.Title,
                                Content = m.Content,
                                Author = m.Author,
                                Url = m.Url,
                        });
                    });
                    esService.BulkAll (esList);
                }
                pageIndex++;
            }
            while (pageCount > 0);
        }

        static void SearchEs () {
            var esService = new EsContentSevcice ();
            var page = new Pager ();
            esService.Search ("多维数组", page);
        }

    }
}