using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NG.Data;
using NG.Services;

namespace NG.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly NamesService namesService;
        private readonly NGDbContext dbContext;

        public ValuesController(NamesService namesService)
        {
            this.namesService = namesService;
        }

        [Authorize]
        [HttpGet("Fetch")]
        public ActionResult<List<string>> FetchNames()
        {
            var userNames = this.namesService.FetchUsernames();

            return userNames;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var userNames = this.namesService.FetchUsernames();

            return userNames;
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
    }
}
