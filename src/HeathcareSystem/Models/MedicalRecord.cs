using Healthcare.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.Models
{
    public class MedicalRecord
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public virtual Profile Patient { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public virtual Profile Doctor { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        [ForeignKey("Hospital")]
        public int HospitalId { get; set; }
        public virtual Hospital Hospital { get; set; }
        [ForeignKey("Treatment")]
        public int TreatmentId { get; set; }
        public virtual Treatment Treatment { get; set; }
        [ForeignKey("Prescription")]
        public int PrescriptionId { get; set; }
        public virtual Prescription Prescription { get; set; }
        [ForeignKey("Disease")]
        public int DiseaseId { get; set; }
        public virtual Disease Disease { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }//false == deleted
    }
}
