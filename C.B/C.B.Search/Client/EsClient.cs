using System;
using C.B.Common.Config;
using Elasticsearch.Net;
using Nest;

namespace C.B.Search.Client {
    public class EsClient {

        private static string _url = AppSettingConfig.EsUrl;
        private static string _defaultIndex = AppSettingConfig.EsDefaultIndex;
        private static ElasticClient _client;
        private EsClient () { }

        public static ElasticClient GetClient () {
            if (_client != null)
                return _client;

            InitClient ();
            return _client;
        }

        private static void InitClient () {
            System.Console.WriteLine ($"url: {_url} , defaultIndex: {_defaultIndex}");
            var node = new Uri (_url);
            _client = new ElasticClient (new ConnectionSettings (node).DefaultIndex (_defaultIndex));
        }
    }
}