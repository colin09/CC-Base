using System.Linq;
using C.B.Common.Config;
using C.B.Common.helper;
using C.B.Models.Data;
using C.B.Search.Data;
using Nest;

namespace C.B.Search.Services {
    public class EsContentSevcice : ESService {

        private static string _indexPrefix = AppSettingConfig.EsIndexPrefix;

        public void Search (string key, Pager page) {
            var indexName = $"{_indexPrefix}{typeof(EsContent).Name}";

            var result = _client.Search<EsContent> (s => s
                //.Index (Indices.All)
                //.Index (IndexName.From<EsContent>())
                .Index (indexName.ToLower ())
                .From ((page.PageIndex - 1) * page.PageSize)
                .Size (page.PageSize)
                .Query (q => q.Match (m => m.Field (f => f.Title).Query (key))));
            // System.Console.WriteLine (result.Documents.ToJson ());
            System.Console.WriteLine ($"{result.Documents.Count()}/{result.Total} , in {result.Took}ms");
        }
    }
}