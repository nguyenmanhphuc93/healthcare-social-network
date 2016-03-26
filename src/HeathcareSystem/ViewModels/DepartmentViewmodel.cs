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
            this.Id = department.Id;
            this.Name = department.Name;
            if (department.Hospital != null)
            {
                Hospital = new HospitalViewmodel(department.Hospital);
            }

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public HospitalViewmodel Hospital { get; set; }
        public List<ProfileViewmodel> Doctors { get; set; }
        public DepartmentViewmodel() { }
    }
}
