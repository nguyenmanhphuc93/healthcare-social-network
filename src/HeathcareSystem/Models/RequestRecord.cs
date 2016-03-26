using Healthcare.Models;
using HeathcareSystem.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.Models
{
    public class RequestRecord
    {
        public int Id { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Profile Doctor { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public User Patient { get; set; }
        [ForeignKey("Disease")]
        public int DiseaseId { get; set; }
        public Disease Disease { get; set; }
        public RequestRecordStatus Status { get; set; }
        [ForeignKey("Record")]
        public int RecordId { get; set; }
        public MedicalRecord Record { get; set; }
    }

  
}
