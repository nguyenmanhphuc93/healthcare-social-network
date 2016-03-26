using Healthcare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.ViewModels
{
    public class PrescriptionViewmodel
    {
        public PrescriptionViewmodel(Prescription prescription)
        {
            if(prescription != null)
            {
                this.Id = prescription.Id;
                this.Note = prescription.Note;
                this.Medicines = prescription.Medicines?.Select(x => new MedicineViewmodel(x)).ToList() ?? new List<MedicineViewmodel>();
            }
        }
        public int Id { get; set; }
        public string Note { get; set; }
        public List<MedicineViewmodel> Medicines { get; set; }
    }
}
