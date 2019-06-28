using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CalculationAPI.Models;
using Database;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CalculationAPI.Controllers
{
    [Route("api/[controller]")]
    public class CalculationController : Controller
    {

        private DBConnection connection;

        public CalculationController()
        {
            connection = DBConnection.Instance();
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "API Calculation : ", "Page for getting the calculated metrics from the .Net" };
        }

        // POST api/<controller>
        [HttpPost]
        public /*Task<ActionResult<float[]>>*/string[,] Post([FromBody]CalculationItem value)
        {
            if (connection.IsConnect())
            {
                //suppose col0 and col1 are defined as VARCHAR in the DB
                string query = "SELECT id, device_id FROM devices";
                var reader = connection.ExecuteQuery(query);

                DataTable dt = new DataTable();
                dt.Load(reader);
                string[,] arr = new string[dt.Rows.Count, 2];
                for(int i=0; i<dt.Rows.Count; i++)// DataRow dr in dt.Rows)
                {
                    arr[i,0] = ""+dt.Rows[i]["id"];
                    arr[i,1] = ""+dt.Rows[i]["device_id"];
                }
                return arr;
            }
            return null;
            { }
        }
    }
}
