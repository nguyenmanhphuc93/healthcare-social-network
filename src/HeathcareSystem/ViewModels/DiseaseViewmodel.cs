using HeathcareSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.ViewModels
{
    public class DiseaseViewmodel
    {
        public DiseaseViewmodel(Disease disease)
        {
            if(disease != null)
            {
                this.Id = disease.Id;
                this.Description = disease.Description;
                this.Name = disease.Name;
            }
        }
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}
