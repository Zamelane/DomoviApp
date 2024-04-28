using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DomoviApp.Server.RequestClient
{
    internal class Client
    {
        public static async Task<Response> Post<TResult>(Uri site, object objToPost, string token = null)
        {
            var _client = new System.Net.Http.HttpClient();
            using (var req = new HttpRequestMessage(HttpMethod.Post, site))
            {
                req.Content = new StringContent(JsonConvert.SerializeObject(objToPost),
                    Encoding.UTF8, "application/json");
                if (token != null)
                    req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                using (var resp = await _client.SendAsync(req))
                {
                    using (var s = await resp.Content.ReadAsStreamAsync())
                    using (var sr = new StreamReader(s))
                    using (var jtr = new JsonTextReader(sr))
                    {
                        return new Response(JsonConvert.SerializeObject(new JsonSerializer().Deserialize<TResult>(jtr)), resp);
                    }
                }
            }
        }
        public static async Task<Response> Get<TResult>(Uri site, string token = null)
        {
            var _client = new System.Net.Http.HttpClient();
            if (token != null)
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            using (var resp = await _client.GetAsync(site))
            {
                using (var s = await resp.Content.ReadAsStreamAsync())
                using (var sr = new StreamReader(s))
                using (var jtr = new JsonTextReader(sr))
                {
                    return new Response(JsonConvert.SerializeObject(new JsonSerializer().Deserialize<TResult>(jtr)), resp);
                }
            }
        }
        public static async Task<Response> Patch<TResult>(Uri site, object objToPost, string token = null)
        {
            var _client = new System.Net.Http.HttpClient();
            using (var req = new HttpRequestMessage(new HttpMethod("PATCH"), site))
            {
                req.Content = new StringContent(JsonConvert.SerializeObject(objToPost),
                    Encoding.UTF8, "application/json");
                if (token != null)
                    req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                using (var resp = await _client.SendAsync(req))
                {
                    using (var s = await resp.Content.ReadAsStreamAsync())
                    using (var sr = new StreamReader(s))
                    using (var jtr = new JsonTextReader(sr))
                    {
                        return new Response(JsonConvert.SerializeObject(new JsonSerializer().Deserialize<TResult>(jtr)), resp);
                    }
                }
            }
        }
    }
}
