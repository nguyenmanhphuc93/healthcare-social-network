using HeathcareSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.ViewModels
{
    public class MedicalRecordViewmodel
    {
        public MedicalRecordViewmodel(MedicalRecord record)
        {
            if(record != null)
            {
                this.Id = record.Id;
                this.CreatedDate = record.CreatedDate;
                this.Appointment = new AppointmentViewModel(record.Appointment);
                this.Prescription = new PrescriptionViewmodel(record.Prescription);
                this.MedicalResults = record.MedicalResults?.Select(x => new MedicalResultViewmodel(x)).ToList() ?? new List<MedicalResultViewmodel>();
            }
        }

        public int Id { get; set; }
        public AppointmentViewModel Appointment { get; set; }
        public List<MedicalResultViewmodel> MedicalResults { get; set; }
        public PrescriptionViewmodel Prescription { get; set; }
        public DiseaseViewmodel Disease { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
