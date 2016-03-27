using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.DataStuff
{
    public static class RoleStuff
    {
        public static void SeedRole(this RoleManager<IdentityRole<long>> _userRole)
        {
            _userRole.CreateAsync(new IdentityRole<long>() { Name = "Doctor" }).Wait();
            _userRole.CreateAsync(new IdentityRole<long>() { Name = "Patient" }).Wait();
            _userRole.CreateAsync(new IdentityRole<long>() { Name = "Manager" }).Wait();
            _userRole.CreateAsync(new IdentityRole<long>() { Name = "Pharmacist" }).Wait();

        }
    }
}
