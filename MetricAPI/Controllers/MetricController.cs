using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricAPI.Models;
using MetricAPI.Utils;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MetricAPI.Controllers
{
    [Route("api/[controller]")]
    public class MetricController : Controller
    {

        private APICallHandler apiHandler;

        public MetricController()
        {
            apiHandler = new APICallHandler();
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "API Metric : ", "Page for pushing the metrics from the device to the Java EE" };
        }

        string s = "";
        int cpt = 0;

        // POST api/<controller>
        [HttpPost]
        public MetricItem Post([FromBody]MetricItem metric)
        {
            //s += cpt + " : " + metric.metricValue + ", " + metric.metricDate + ", " + metric.deviceType + ";\n";
            //cpt++;
            //System.IO.File.WriteAllText(@"D:\Developpement\C#\TestApi1\test.txt", s);

            //TODO : call apiHandler to go to the JEE
            return metric;
        }
    }
}
