using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIsNetPlateform.Models;
using APIsNetPlateform.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIsNetPlateform.Controllers
{
    [Route("api/[controller]")]
    public class CalculationController : Controller
    {
        private readonly CalculatedMetricContext _context;

        public CalculationController(CalculatedMetricContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "API Calculation : ", "Page for getting the calculated metrics from the .Net" };
        }

        // POST api/<controller>
        [HttpPost]
        public /*Task<ActionResult<float[]>>*//*string[,]*/List<CalculatedMetricItem> Post([FromBody]CalculationItem value)
        {
            var list = _context.MetricItems.ToList();//Select(x=>x).ToList();
                                                     //FromSql("SELECT * FROM calculatedmetric").ToList();

            //if (connection.IsConnect())
            //{
            //    //suppose col0 and col1 are defined as VARCHAR in the DB
            //    string query = "SELECT id, device_id FROM devices";
            //    var reader = connection.ExecuteQuery(query);

            //    DataTable dt = new DataTable();
            //    dt.Load(reader);
            //    string[,] arr = new string[dt.Rows.Count, 2];
            //    for (int i = 0; i < dt.Rows.Count; i++)// DataRow dr in dt.Rows)
            //    {
            //        arr[i, 0] = "" + dt.Rows[i]["id"];
            //        arr[i, 1] = "" + dt.Rows[i]["device_id"];
            //    }
            //    return arr;
            //}
            return list;
        }
    }
}
