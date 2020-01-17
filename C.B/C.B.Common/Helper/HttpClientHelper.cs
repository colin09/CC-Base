using System;
using System.Net.Http;
using System.Net.Http.Headers;
using C.B.Common.logger;
using Newtonsoft.Json;

namespace C.B.Common.helper {

    public class HttpClientHelper {
        private readonly HttpClient _client;
        private log4net.ILog log;
        public HttpClientHelper () {
            _client = new HttpClient ();
            log = Logger.Current ();
        }

        public string GetString (string uri, string authorizationToken = null, string authorizationMethod = "Bearer") {
            // var origin = GetOriginFromUri (uri);
            var requestMessage = new HttpRequestMessage (HttpMethod.Get, uri);
            if (authorizationToken != null)
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue (authorizationMethod, authorizationToken);
            var response = _client.SendAsync (requestMessage).Result;
            log.Debug ($"GET,Url : {uri}");
            log.Debug ($"GET Response : {response}");

            if (!response.IsSuccessStatusCode) {
                log.Error ($"{uri}调用失败，返回结果：{response.ToString()}");
                throw new HttpRequestException ();
            }
            return response.Content.ReadAsStringAsync ().Result;
        }

        public string DoPostPut<T> (HttpMethod method, string uri, T item, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer") {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
                throw new ArgumentException ("Value must be either post or put.", method.GetType ().Name);

            // var origin = GetOriginFromUri (uri);
            var requestMessage = new HttpRequestMessage (method, uri);
            requestMessage.Content = new StringContent (item == null ? "" : JsonConvert.SerializeObject (item), System.Text.Encoding.UTF8, "application/json");
            if (authorizationToken != null)
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue (authorizationMethod, authorizationToken);
            if (requestId != null)
                requestMessage.Headers.Add ("x-requestid", requestId);
            var response = _client.SendAsync (requestMessage).Result;
            log.Debug ($"Post.Put,Url : {uri}");
            log.Debug ($"Post.Put,Request : {item.ToJson()}");
            log.Debug ($"Post.Put Response : {response}");

            if (!response.IsSuccessStatusCode) {
                log.Error ($"{uri}调用失败，返回结果：{response.ToString()}");
                throw new HttpRequestException ();
            }
            var result = response.Content.ReadAsStringAsync ().Result;
            log.Debug ($"Post.Put Result; : {result}");
            return result;
        }

    }

}