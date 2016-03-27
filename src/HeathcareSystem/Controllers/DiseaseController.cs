using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Healthcare.Models;
using HeathcareSystem.Models;
using Microsoft.Data.Entity;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HeathcareSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DiseaseController : Controller
    {
        IHealthcareContext context;
       public DiseaseController(IHealthcareContext context)
        {
            this.context = context;
        }
        // GET: api/values
        [HttpGet("id")]
        public IActionResult Get(long id)
        {
            var disease = context.Diseases.SingleOrDefault(a => a.Id==id);
            return Ok(disease);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = context.Diseases.ToList();
            return Ok(result) ;
        }

        
        [HttpPost]
        public IActionResult AddNewDisease([FromBody]Disease model)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest();
            }
            context.Diseases.Add(model);
            context.SaveChange();
            return Ok();
        }
        

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var disease = context.Diseases.SingleOrDefault(a => a.Id == id);
            context.SetState(disease, EntityState.Deleted);
            context.SaveChange();
            return Ok();
        }
    }
}
