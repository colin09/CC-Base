using System.Collections.Generic;
using System.Threading;
using C.B.Common.Config;
using C.B.Search.Client;
using C.B.Search.Data;
using Nest;

namespace C.B.Search.Services {
    public class ESService {
        protected IElasticClient _client;
        private static string _indexPrefix = AppSettingConfig.EsIndexPrefix;

        public ESService () {
            _client = EsClient.GetClient ();
        }

        public void Index<T> (T model) where T : ESBase {
            var indexName = $"{_indexPrefix}{typeof(T).Name}";
            var response = _client.Index (model, s => s.Index (indexName.ToLower ()));
            System.Console.WriteLine (response);
        }

        public void BulkAll<T> (IEnumerable<T> models) where T : ESBase {
            var indexName = $"{_indexPrefix}{typeof(T).Name}";
            // System.Console.WriteLine ($"indexName => {indexName.ToLower ()}");

            // var response = _client.BulkAll (models, s => s.Index (indexName.ToLower ()));
            // System.Console.WriteLine (response);

            var waitHandle = new CountdownEvent (1);

            var bulkAll = _client.BulkAll (models, b => b
                .Index (indexName.ToLower ()) /* index */
                .BackOffRetries (2)     //how many times to retry before failing
                .BackOffTime ("5s")    //how long we should wait before retrying
                .RefreshOnCompleted (true)
                .MaxDegreeOfParallelism (4) //number of concurrent requests
                .Size (1000)    //number of documents to send in each request
            );

            bulkAll.Subscribe (new BulkAllObserver (
                onNext: (b) => { System.Console.WriteLine($"[next] page:{b.Page}, retries:{b.Retries}"); },
                onError: (e) => { throw e; },
                onCompleted: () => waitHandle.Signal ()
            ));

            waitHandle.Wait ();
        }

        public void Delete<T> (T model) where T : ESBase {
            var response = _client.Delete (new DocumentPath<T> (new Id (model.Id)));
            System.Console.WriteLine (response);
        }

    }
}