using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DomoviApp.Server.RequestClient
{
    internal class Response
    {
        private string content;
        private HttpResponseMessage raw;
        public Response(string content, HttpResponseMessage raw)
        {
            this.content = content;
            this.raw = raw;
        }

        public T Deserialize<T>()
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        public System.Net.HttpStatusCode GetStatusCode() => raw.StatusCode;
        public bool IsSuccefful() => raw.IsSuccessStatusCode;
    }
}
