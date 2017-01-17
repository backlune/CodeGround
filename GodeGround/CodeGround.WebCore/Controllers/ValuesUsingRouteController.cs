using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CodeGround.WebCore.Controllers
{
    [Route("api/[controller]")]
    public class ValuesUsingRouteController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

      // GET api/values/5
      [HttpGet("id={id}&name={name}")]
      public string Get2(int id, string name)
      {
         // string s = null;
         //var test = s?.ToString();
         //var state = new iCore.Public.Entities.JobType();
         //return state.ToString();
         return string.Empty;
      }

      // POST api/values
      [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
