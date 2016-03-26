using Healthcare.Models;
using HeathcareSystem.Enums;
using HeathcareSystem.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.DataStuff
{
    public static class AppointmentStuff
    {
        public static void SeedAppointment(this HealthCareContext context)
        {
            var random = new Random();
            var requests = context.AppointmentRequests.ToList();
            var doctorInDepartments = context.DoctorInDepartments.ToList();
            for(var i = 0; i<requests.Count; i++)
            {
                context.Appointments.Add(new Appointment
                {
                    Department = requests[i].Department,
                    DepartmentId = requests[i].DepartmentId.Value,
                    Doctor = requests[i].Doctor,
                    DoctorId = requests[i].DoctorId.Value,
                    Patient = requests[i].Patient,
                    PatientId = requests[i].PatientId,
                    Request = requests[i],
                    RequestId = requests[i].Id,
                    Status = AppointmentStatus.Completed
                });
            }

        }
        public static void SeedAppointmentRequest(this HealthCareContext context, int count = 20)
        {
            var random = new Random();
            var doctors = context.DoctorInDepartments.Select(a => a.Doctor).ToList();
            var doctorInDepartments = context.DoctorInDepartments.ToList();
            var users = context.Users.ToList();
            var hospitals = context.Hospitals.ToList();
            var appoinments = context.Appointments.ToList();
            for (var i = 0; i < count; i++)
            {
                var doctorInDepartment = doctorInDepartments[random.Next(doctorInDepartments.Count)];
                var day = random.Next(1, 28);
                var month = random.Next(1, 12);
                var year = random.Next(2000, 2014);
                var user = users[random.Next(users.Count)].Profile;
                while (user.Id == doctorInDepartment.DoctorId)
                {
                    user = users[random.Next(users.Count)].Profile;
                }
                context.AppointmentRequests.Add(new AppointmentRequest()
                {
                    Department = doctorInDepartment.Department,
                    DepartmentId = doctorInDepartment.DepartmentId,
                    Doctor = doctorInDepartment.Doctor,
                    DoctorId = doctorInDepartment.DoctorId,
                    Hospital = doctorInDepartment.Department.Hospital,
                    HospitalId = doctorInDepartment.Department.Hospital.Id,
                    Patient = user,
                    PatientId = user.Id,
                    StartTime = new DateTime(year, month, day),
                    EndTime = new DateTime(year, month, day + 1)
                });
            }

        }
    }
}
