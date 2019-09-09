using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("start-stream")]
        public void StartStream([FromServices]IHubContext<MyHub> hub){
            Task.Run(() => {
            Random rnd = new Random();
            Random sleepRnd = new Random();
            for (int i = 0; i < 10000; i++)
            {
                System.Console.WriteLine($"loop {i}");
                hub.Clients.All.SendAsync("onStream",new Models.Payload{
                    Percentage = rnd.Next(0,100),
                    Name = "CPU" 
                });
                System.Threading.Thread.Sleep(sleepRnd.Next(200, 3000));
                hub.Clients.All.SendAsync("onStream",new Models.Payload{
                    Percentage = rnd.Next(0,100),
                    Name = "RAM" 
                });
            }
            });
            
        }
    }
}
