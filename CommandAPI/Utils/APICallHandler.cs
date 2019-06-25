using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CommandAPI.Utils
{
    public class APICallHandler
    {
        public HttpClient _client = new HttpClient();

        string path = "https://localhost:44329/api/value";

        public APICallHandler()
        {
            // Update port # in the following line.
            _client.BaseAddress = new Uri(path);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Task CallAPI()
        {
            //TODO : To implement
            return new Task(() => { });
        }
    }
}
