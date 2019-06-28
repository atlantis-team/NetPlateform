using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIsNetPlateform.Models;
using APIsNetPlateform.Utils;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIsNetPlateform.Controllers
{
    [Route("api/[controller]")]
    public class MetricController : Controller
    {
        private APICallHandler apiHandler;

        public MetricController()
        {
            apiHandler = new APICallHandler("https://localhost:44329/api/metric");
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "API Metric : ", "Page for pushing the metrics from the device to the Java EE" };
        }

        // POST api/<controller>
        [HttpPost]
        public MetricItem Post([FromBody]MetricItem metric)
        {
            Console.WriteLine("type : " + metric.deviceType + ", date : " + metric.metricDate + ", value : " + metric.metricValue);
            //TODO : call apiHandler to go to the JEE
            return metric;
        }

        //[HttpGet]
        //public MetricItem GetMetric([FromBody]MetricItem metric)
        //{
        //    return metric;
        //}
    }
}
