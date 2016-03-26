using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.ViewModels
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public ProfileViewmodel Patient { get; set; }
        public ProfileViewmodel Doctor { get; set; }
        public DateTime Time { get; set; }
        public string Description { get; set; }
        public DepartmentViewmodel Department { get; set; }

    }
}
