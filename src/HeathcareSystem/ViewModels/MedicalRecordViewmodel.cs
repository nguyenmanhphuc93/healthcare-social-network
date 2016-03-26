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
            this.Id = record.Id;
            this.CreatedDate = record.CreatedDate;
            this.Patient = new ProfileViewmodel(record.Patient);
            this.Doctor = new ProfileViewmodel(record.Doctor);
            this.Department = new DepartmentViewmodel(record.Department);
            this.Hospital = new HospitalViewmodel(record.Hospital);
            this.Treatment = new TreatmentViewmodel(record.Treatment);
            this.Prescription = new PrescriptionViewmodel(record.Prescription);
            this.Disease = new DiseaseViewmodel(record.Disease);
        }

        public int Id { get; set; }
        public ProfileViewmodel Patient { get; set; }
        public ProfileViewmodel Doctor { get; set; }
        public DepartmentViewmodel Department { get; set; }
        public HospitalViewmodel Hospital { get; set; }
        public TreatmentViewmodel Treatment { get; set; }
        public PrescriptionViewmodel Prescription { get; set; }
        public DiseaseViewmodel Disease { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
