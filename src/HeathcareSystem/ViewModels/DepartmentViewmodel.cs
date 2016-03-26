using Healthcare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.ViewModels
{
    public class DepartmentViewmodel
    {
        public DepartmentViewmodel(Department department)
        {
            if(department != null)
            {
                this.Id = department.Id;
                this.Name = department.Name;
            }

            if (department.Hospital != null)
            {
                this.Hospital = new HospitalViewmodel
                {
                    Address = department.Hospital.Address,
                    Name = department.Hospital.Name,
                    Id = department.Hospital.Id,
                    PhoneNumber = department.Hospital.PhoneNumber,
                };
            }

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public HospitalViewmodel Hospital { get; set; }
        public List<ProfileViewmodel> Doctors { get; set; }
        public DepartmentViewmodel() { }
    }
}
