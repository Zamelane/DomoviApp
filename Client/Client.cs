using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using Newtonsoft.Json;

namespace DomoviApp.Server.RequestClient
{
    internal class Client
    {
        private enum RequestType
        {
            GET,
            POST,
            PATCH,
            PUT,
            DELETE
        }

        // Обёртки запросов
        public static async Task<Response> Get<TResult>(Uri site, object data, string token = null, string authType = "Bearer")
            => await Request<TResult>(RequestType.GET, site, data, token, authType);

        public static async Task<Response> Post<TResult>(Uri site, object data, string token = null, string authType = "Bearer")
            => await Request<TResult>(RequestType.POST, site, data, token, authType);

        public static async Task<Response> Patch<TResult>(Uri site, object data, string token = null, string authType = "Bearer")
            => await Request<TResult>(RequestType.PATCH, site, data, token, authType);

        public static async Task<Response> Put<TResult>(Uri site, object data, string token = null, string authType = "Bearer")
            => await Request<TResult>(RequestType.PUT, site, data, token, authType);

        public static async Task<Response> Delete<TResult>(Uri site, object data, string token = null, string authType = "Bearer")
            => await Request<TResult>(RequestType.DELETE, site, data, token, authType);

        private static async Task<Response> Request<TResult>(RequestType type, Uri site, object data = null, string token = null, string authType = "Bearer")
        {
            // Заводим клиента
            var _client = new System.Net.Http.HttpClient();

            // Подготавливаем запрос
            if ((type == RequestType.GET || type == RequestType.PATCH || type == RequestType.DELETE) && data != null)
            {
                var builder = new UriBuilder(site);
                var query = HttpUtility.ParseQueryString(builder.Query);
                foreach (var prop in data.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    query[prop.Name] = prop.GetValue(data, null).ToString();
                }
                builder.Query = query.ToString();
                site = new Uri(builder.ToString());
            } else if (data != null)
            {
                data = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            }

            using (var req = new HttpRequestMessage(new HttpMethod(type.ToString()), site))
            {
                // Если есть тело, то крепим
                if (data != null)
                    req.Content = data as System.Net.Http.HttpContent;
                // Если есть токен, то крепим
                if (token != null)
                    req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(authType, token);

                using (var resp = await _client.SendAsync(req))
                {
                    using (var s = await resp.Content.ReadAsStreamAsync())
                    using (var sr = new StreamReader(s))
                    using (var jtr = new JsonTextReader(sr))
                    {
                        MessageBox.Show(site.ToString());
                        return new Response(JsonConvert.SerializeObject(new JsonSerializer().Deserialize<TResult>(jtr)), resp);
                    }
                }
            }
        }
    }
}
