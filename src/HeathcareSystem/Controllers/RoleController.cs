using Healthcare.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.Controllers
{
    [Authorize]
    public class RoleController : BaseController
    {
        private readonly UserManager<User> _userManager;

        public RoleController(IHealthcareContext context) : base(context)
        {

        }

        public async Task<IActionResult> GetCurrentUserRoles()
        {
            var user = context.Users.SingleOrDefault(x => x.UserName == User.Identity.Name);
            return Ok(await _userManager.GetRolesAsync(user));
        }
    }
}
