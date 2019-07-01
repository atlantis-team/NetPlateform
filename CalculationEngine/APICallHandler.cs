using CalculationEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CalculationEngine
{
    class APICallHandler
    {
        public HttpClient _client;
        private string path = "http://192.168.43.168:21061/........";

        public APICallHandler()
        {
            _client = new HttpClient();
            // Update port # in the following line.
            _client.BaseAddress = new Uri(path);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> PostAsync()
        {
            try
            {
                var content = new StringContent(new Request().ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("api/test", content);
                if (response.IsSuccessStatusCode)
                {
                    String res = await response.Content.ReadAsStringAsync();//   ReadAsAsync<String>();
                    Console.WriteLine(res);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

    }
}
