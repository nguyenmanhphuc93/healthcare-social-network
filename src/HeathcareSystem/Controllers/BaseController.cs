using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Healthcare.Models;
using Microsoft.Data.Entity;
using System.Net;

namespace HeathcareSystem.Controllers
{
    public class BaseController : Controller
    {
        public readonly IHealthcareContext context;
        private Profile _currentUser { get; set; }
        public Profile CurrentUser
        {
            get
            {
                if (_currentUser != null)
                {
                    return _currentUser;
                }
                var userName = User.Identity.Name;
                _currentUser = context.Users.Include(n => n.Profile)
                    .Single(n => n.UserName == userName).Profile;
                return _currentUser;
            }
        }

        public BaseController(IHealthcareContext context)
        {
            this.context = context;
        }


        protected class HttpForbiddenResult : HttpStatusCodeResult
        {
            public HttpForbiddenResult()
       : base((int)HttpStatusCode.Forbidden)
            {
            }
        }
    }
}
