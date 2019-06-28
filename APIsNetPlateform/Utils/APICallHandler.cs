using APIsNetPlateform.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace APIsNetPlateform.Utils
{
    public class APICallHandler
    {
        public HttpClient _client = new HttpClient();

        string _path = "https://localhost:44329/api/metric";

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

        public async Task<string> PostCommandAsync(string path, CommandItem item)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(path, content);
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
            return "rien";
        }

        public async Task<string> GetCommandAsync(string path)
        {
            HttpResponseMessage response = await _client.GetAsync(_path + path);
            if(response.IsSuccessStatusCode)
            {
                string s = await response.Content.ReadAsAsync<string>();
                return s;
            }
            return "Error while sending...";
        }
    }
}
