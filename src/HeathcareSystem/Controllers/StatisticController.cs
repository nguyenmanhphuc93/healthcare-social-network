using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Healthcare.Models;
using HeathcareSystem.DataStuff;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HeathcareSystem.Controllers
{
    
    [Route("api/[controller]/[action]")]
    public class StatisticController : Controller
    {
        IHealthcareContext context;
        public StatisticController(IHealthcareContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetProvinces()
        {
            var stuff = new GraphStuff();
            return Ok(stuff.SeedSicknessInProvince());
        }
        [HttpGet]
        public IActionResult GetDiceases()
        {
            var stuff = new GraphStuff();
            return Ok(stuff.SeedLocationSickness());
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
