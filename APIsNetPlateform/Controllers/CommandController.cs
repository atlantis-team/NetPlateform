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
    public class CommandController : Controller
    {
        private APICallHandler apiHandler;

        public CommandController()
        {
            apiHandler = new APICallHandler("http://192.168.43.63:5000/");
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "API Command : ", "Receive the command and send them to the device or the simulator" };
        }

        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody]JEECommandItem command)
        {
            apiHandler.ChangePath("http://" + command.IPAdress + ":5000/");
            CommandItem item = new CommandItem();
            item.MACAdress = command.MACAdress;
            item.State = command.State;
            var task = apiHandler.PostCommandAsync("api/command", item);//GetAsync("api/command/" + command.Command);
            task.Wait();
            return task.Result;
            //TODO : call apiHandler to go to the JEE
            //return command;
        }
    }
}
