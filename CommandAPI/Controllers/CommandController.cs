using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandAPI.Models;
using CommandAPI.Utils;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CommandAPI.Controllers
{
    [Route("api/[controller]")]
    public class CommandController : Controller
    {

        private APICallHandler apiHandler;

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "API Command : ", "Receive the command and send them to the device or the simulator" };
        }

        // POST api/<controller>
        [HttpPost]
        public CommandItem Post([FromBody]CommandItem command)
        {
            //TODO : call apiHandler to go to the JEE
            return command;
        }
    }
}
