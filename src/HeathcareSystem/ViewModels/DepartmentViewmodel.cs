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
        }
        public int Id { get; set; }
        public int Name { get; set; }
    }
}
