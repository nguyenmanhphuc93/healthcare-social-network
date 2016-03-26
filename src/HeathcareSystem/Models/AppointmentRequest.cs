using Healthcare.Models;
using HeathcareSystem.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.Models
{
    public class AppointmentRequest
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [ForeignKey("Hospital")]
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Profile Patient { get; set; }
        [ForeignKey("Doctor")]
        public int? DoctorId { get; set; }
        public Profile Doctor { get; set; }
        [ForeignKey("Appointment")]
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
        public string Description { get; set; }
        public RequestStatus Status { get; set; }
    }
}
