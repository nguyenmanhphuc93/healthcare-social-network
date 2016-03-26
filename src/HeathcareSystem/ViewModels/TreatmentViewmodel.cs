using Healthcare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.ViewModels
{
    public class TreatmentViewmodel
    {
        public TreatmentViewmodel(Treatment treatment)
        {
            if(treatment == null)
            {
                this.Id = treatment.Id;
                this.Description = treatment.Description;
            }
        }
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
