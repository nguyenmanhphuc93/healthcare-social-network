
using Healthcare.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Healthcare.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public virtual Profile Patient { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public virtual Profile Doctor { get; set; }
        public DateTime Date { get; set; }
    }
}