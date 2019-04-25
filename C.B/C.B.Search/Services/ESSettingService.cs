using System;
using System.Collections.Generic;
using C.B.Common.helper;
using C.B.Search.Client;
using C.B.Search.Data;
using Elasticsearch.Net;
using Nest;

namespace C.B.Search.Services {

    public class ESSettingService {
        private IElasticClient _client;

        public ESSettingService () {
            // var uris = new [] {
            //     new Uri (connStr),
            // };
            // var connectionPool = new SniffingConnectionPool (uris);
            // var settings = new ConnectionSettings (connectionPool).DefaultIndex ("_developIndex");
            // var settings = new ConnectionSettings (new Uri (connStr)).DefaultIndex ("dev");

            // _client = new ElasticClient (settings);
            _client = EsClient.GetClient ();
        }

        public void AutoMap () {

            var indexName = $"{nameof(EventInfoES).ToLower()}";
            if (_client.IndexExists (indexName).Exists)
                _client.DeleteIndex (indexName);
            var createIndexResponse = _client.CreateIndex (indexName, c => c
                .Mappings (ms => ms
                    .Map<EventInfoES> (m => m.AutoMap ())
                )
            );
            /*
            var createIndexResponse = _client.CreateIndex("myindex");*/
            System.Console.WriteLine (createIndexResponse.DebugInformation.ToJson ());

            var searchResponse = _client.Search<EventInfoES> (s => s
                .Query (q => q
                    .MatchAll ()
                )
            );
            System.Console.WriteLine (searchResponse.Documents.ToJson ());
        }

        public void IndexDocument () {
            var model = new EventInfoES {
                Id = "2",
                Title = "发送请求",
                Content = "在生产应用程序中，您将需要使用实际发送请求的IConnection。",
                Author = "ddd",
                SortNo = 1,
            };
            var indexResponse = _client.IndexDocument (model);

            var content = new EsContent { Id = "16tyjty", Title = "Title", Content = "Content", Author = "author", Url = "url" };
            var indexResponse2 = _client.IndexDocument (content);

            // _client.BulkAll(new List<EventInfoES>(),null);
            System.Console.WriteLine (indexResponse);
            System.Console.WriteLine (indexResponse2);
        }

        public void Search (string key) {
            var result = _client.Search<EventInfoES> (s => s
                .From (0)
                .Size (10)
                .Query (q => q.Match (m => m.Field (f => f.Content).Query (key)))).Documents;
            System.Console.WriteLine (result);
        }
    }
}