using Healthcare.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        public ProfileController(IHealthcareContext context) : base(context) { }
        public IActionResult GetCurrentUserName()
        {
            return Ok(CurrentUser.DisplayName);
        }
    }
}
