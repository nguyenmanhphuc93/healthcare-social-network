using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using HeathcareSystem.BindingModels;
using HeathcareSystem.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HeathcareSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AppointmentController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };

        }

        [HttpPost]
        public IActionResult RequestAppointment(AppointmentRequestBindingModel model)
        {
            var appointmentRequest = new AppointmentRequest();
            return Ok();
        }
    }
}
