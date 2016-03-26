using HeathcareSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.ViewModels
{
    public class MedicalResultViewmodel
    {
        public MedicalResultViewmodel(MedicalResult medicalResult)
        {
            this.Id = medicalResult.Id;
            this.Status = medicalResult.Status;
            this.Disease = new DiseaseViewmodel(medicalResult.Disease);
        }
        public int Id { get; set; }
        public DiseaseViewmodel Disease { get; set; }
        public string Status { get; set; }
    }
}
