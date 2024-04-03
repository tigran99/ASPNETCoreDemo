using ASPNETCoreDemo.Identity;
using ASPNETCoreDemo.Models;
using ASPNETCoreDemo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNETCoreDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMyService _myService;

        public ValuesController(IConfiguration config, IMyService myService)
        {
            _config = config;
            _myService = myService;
        }

        // GET: api/<ValuesController>
        [Authorize]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            _myService.MyMethod();

            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] ValueModel value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [Authorize]
        [RequiresClaim("admin", "true")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
