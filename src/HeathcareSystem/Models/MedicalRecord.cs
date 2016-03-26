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
        [ForeignKey("Apointment")]
        public int AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; }
        public DateTime CreatedDate { get; set; }
        [ForeignKey("Prescription")]
        public int PrescriptionId { get; set; }
        public virtual Prescription Prescription { get; set; }
        public virtual ICollection<MedicalResult> MedicalResults { get; set; }
    }
}
