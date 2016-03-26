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
            this.Id = disease.Id;
            this.Description = disease.Description;
        }
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
