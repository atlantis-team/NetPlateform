using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_Metric
{
    class APICallHandler
    {
        public HttpClient _client = new HttpClient();

        string _path;

        public object JsonConvert { get; private set; }

        public APICallHandler(string path)
        {
            // Update port # in the following line.
            _client.BaseAddress = new Uri(path);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            _path = path;
        }

        public void ChangePath(string path)
        {
            _client = new HttpClient();
            // Update port # in the following line.
            _client.BaseAddress = new Uri(path);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            _path = path;
        }

        //public Task CallAPI()
        //{
        //    //TODO : To implement
        //    return new Task(() => { });
        //}

        public async Task<string> PostMetric(string msg)
        {
            Console.WriteLine("Send to JEE : " + msg);
            return "";
        }

        public async Task<string> PostMetricAsync(string path, String item)
        {
            try
            {
                var content = new StringContent(item, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(_path + path, content);
                if (response.IsSuccessStatusCode)
                {
                    String res = await response.Content.ReadAsStringAsync();//   ReadAsAsync<String>();
                    //Console.WriteLine(res);
                    return res;
                }
                else
                {
                    return "response status code : " + response.StatusCode + ", json : " + content;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Error while sending... " + e.Message;
            }
        }

        //public async Task<string> GetCommandAsync(string path)
        //{
        //    HttpResponseMessage response = await _client.GetAsync(_path + path);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        string s = await response.Content.ReadAsAsync<string>();
        //        return s;
        //    }
        //    return "Error while sending...";
        //}
    }
}
//}
