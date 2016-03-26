using HeathcareSystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.BindingModels
{
    public class AppointmentRequestBindingModel
    {
        public int HospitalId { get; set; }
        public int? DepartmentId { get; set; }
        public int DoctorId { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public RequestStatus Status { get; set; }
    }
}
