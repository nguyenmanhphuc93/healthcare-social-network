using Healthcare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.ViewModels
{
    public class ProfileViewmodel
    {
        public ProfileViewmodel(Profile profile)
        {
            this.Id = profile.Id;
            this.FirstName = profile.FirstName;
            this.LastName = profile.LastName;
            this.DisplayName = profile.DisplayName;
            this.MobiPhone = profile.MobiPhone;
            this.HomePhone = profile.HomePhone;
            this.Avatar = profile.Avatar;
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string MobiPhone { get; set; }
        public string HomePhone { get; set; }
        public string Avatar { get; set; }
    }

}
