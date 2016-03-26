using Healthcare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.Models
{
    public class AppointmentRequest
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public long PatientId { get; set; }
        public Profile Patient { get; set; }
    }
}
