using Healthcare.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace HeathcareSystem.Controllers
{
    [Authorize]
    public class RoleController : BaseController
    {
        private readonly UserManager<User> _userManager;

        public RoleController(IHealthcareContext context, UserManager<User> UserManager) : base(context)
        {
            _userManager = UserManager;
        }

        public async Task<IActionResult> GetCurrentUserRoles()
        {
            var user = context.Users.SingleOrDefault(x => x.UserName == User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(user);
            return Ok(roles);
        }
    }
}
