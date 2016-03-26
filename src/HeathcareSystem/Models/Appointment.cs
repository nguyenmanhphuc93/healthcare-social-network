using HeathcareSystem.Enums;
using HeathcareSystem.Models;
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
        public long DoctorId { get; set; }
        public virtual Profile Doctor { get; set; }
        public DateTime Time { get; set; }

        public AppointmentStatus Status { get; set; }
        [ForeignKey("Request")]
        public int RequestId { get; set; }

        public AppointmentRequest Request { get; set; }
    }
}