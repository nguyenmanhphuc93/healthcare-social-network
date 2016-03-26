using Healthcare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.ViewModels
{
    public class HospitalViewmodel
    {
        public HospitalViewmodel(Hospital hospital)
        {
            if(hospital != null)
            {
                this.Id = hospital.Id;
                this.Name = hospital.Name;
                this.Address = hospital.Address;
                this.PhoneNumber = hospital.PhoneNumber;
                this.Departments = hospital.Departments?.Select(x => new DepartmentViewmodel(x)).ToList() ?? new List<DepartmentViewmodel>();
            }
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public List<DepartmentViewmodel> Departments { get; set; }

        public HospitalViewmodel() { }
    }
}
