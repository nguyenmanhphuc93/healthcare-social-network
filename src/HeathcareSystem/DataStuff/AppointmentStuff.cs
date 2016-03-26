using Healthcare.Models;
using HeathcareSystem.Enums;
using HeathcareSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Data.Entity;
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
            context.SeedAppointmentRequest();
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
                    Status = AppointmentStatus.Completed,
                    Time = requests[i].StartTime
                });
            }
            context.SaveChange();

        }
        public static void SeedAppointmentRequest(this HealthCareContext context, int count = 20)
        {
            var random = new Random();
            var doctors = context.DoctorInDepartments.Select(a => a.Doctor).ToList();
            var doctorInDepartments = context.DoctorInDepartments.ToList();
            var users = context.Users.Include(a=> a.Profile).ToList();
            var hospitals = context.Hospitals.ToList();
            var appoinments = context.Appointments.ToList();
            var patient1 = context.Users.Include(a => a.Profile).SingleOrDefault(a => a.UserName.ToLower() == "patient1").Profile;
            var doctorInDepartment = doctorInDepartments.SingleOrDefault(a=> a.Doctor.DisplayName.ToLower() == "doctor1");
            for (var i = 0; i < count; i++)
            {
                
                var day = random.Next(1, 28);
                var month = random.Next(1, 12);
                var year = random.Next(2000, 2014);
                while (patient1.Id == doctorInDepartment.DoctorId)
                {
                    patient1 = users[i].Profile;
                }
                context.AppointmentRequests.Add(new AppointmentRequest()
                {
                    Department = doctorInDepartment.Department,
                    DepartmentId = doctorInDepartment.DepartmentId,
                    Doctor = doctorInDepartment.Doctor,
                    DoctorId = doctorInDepartment.DoctorId,
                    Hospital = doctorInDepartment.Department.Hospital,
                    HospitalId = doctorInDepartment.Department.Hospital.Id,
                    Patient = patient1,
                    PatientId = patient1.Id,
                    StartTime = new DateTime(year, month, day),
                    EndTime = new DateTime(year, month, day + 1)
                });
            }
            context.SaveChange();

        }
    }
}
