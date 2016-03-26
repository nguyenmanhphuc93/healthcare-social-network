using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.ViewModels
{
    public class HospitalInProvinceViewModel
    {
        public string Province { get; set; }
        public List<HospitalViewmodel> Hospitals { get; set; }
    }
}
