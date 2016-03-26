using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.Enums
{
    public enum RequestStatus
    {
        Spending,
        Confirmed,
        PatientCancel,
        Delay,
        DoctorCancel,
    }

    public enum AppointmentStatus
    {
        InProcess,
        Cancel,
        Delayed,
        Completed
    }
}
