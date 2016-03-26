using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using HeathcareSystem.BindingModels;
using HeathcareSystem.Models;
using Healthcare.Models;
using HeathcareSystem.Enums;
using Microsoft.AspNet.Authorization;
using Microsoft.Data.Entity;
using HeathcareSystem.ViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HeathcareSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class AppointmentController : BaseController
    {
        // GET: api/values
        public AppointmentController(IHealthcareContext context) : base(context) { }

        [HttpPost]
        public IActionResult RequestAppointment(AppointmentRequestBindingModel model)
        {
            var appointmentRequest = new AppointmentRequest
            {
                HospitalId = model.HospitalId,
                DepartmentId = model.DepartmentId,
                PatientId = CurrentUser.Id,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                Description = model.Description
            };
            context.AppointmentRequests.Add(appointmentRequest);
            context.SaveChange();
            var appointment = ProccessRequest(appointmentRequest);
            //var appointment
            return Ok(context);
        }

        private Appointment ProccessRequest(AppointmentRequest request)
        {
            var random = new Random();
            if (request.DoctorId != null)
            {
                var appointment = new Appointment { DoctorId = request.DoctorId.Value, Time = request.StartTime, Status = AppointmentStatus.InProcess, RequestId = request.Id };
                return CreateAppointment(appointment, context);

            }

            if (request.DepartmentId != null)
            {
                var doctorIs = context.DoctorInDepartments.Where(n => n.DepartmentId == request.DepartmentId.Value).Select(n => n.DepartmentId).ToList();
                var appointment = new Appointment { DoctorId = doctorIs[random.Next(doctorIs.Count)], Time = request.StartTime, Status = AppointmentStatus.InProcess, RequestId = request.Id };
                return CreateAppointment(appointment, context);
            }
            return null;
        }

        [HttpGet]
        public IActionResult GetPattientInCommingAppointment()
        {
            var appointments = context.Appointments.Include(n => n.Request.Doctor).Where(n => n.Request.PatientId == CurrentUser.Id && (n.Status == AppointmentStatus.InProcess || n.Status == AppointmentStatus.Inqueue));
            var doctors = context.DoctorInDepartments.Include(n => n.Department.Hospital);
            var appointmentViewModels = new List<AppointmentViewModel>();
            foreach (var a in appointments)
            {
                var hospital = doctors.SingleOrDefault(n => n.DoctorId == a.Id);
                appointmentViewModels.Add(new AppointmentViewModel
                {
                    Department = new DepartmentViewmodel(hospital.Department),
                    Doctor = new ProfileViewmodel(a.Request.Doctor),
                    Description = a.Request.Description,
                    Id = a.Id,
                    Time = a.Time
                });
            }
            return Ok(appointmentViewModels);
        }

        [HttpGet]
        public IActionResult GetDoctorInCommingAppoitnment()
        {
            var appointments = context.Appointments.Include(n => n.Request.Doctor).Where(n => n.DoctorId == CurrentUser.Id && (n.Status == AppointmentStatus.InProcess || n.Status == AppointmentStatus.Inqueue));
            var appointmentViewModels = new List<AppointmentViewModel>();
            foreach (var a in appointments)
            {
                appointmentViewModels.Add(new AppointmentViewModel
                {
                    Patient = new ProfileViewmodel(a.Request.Patient),
                    Description = a.Request.Description,
                    Id = a.Id,
                    Time = a.Time
                });
            }
            return Ok(appointmentViewModels);
        }


        #region Helpers
        private Appointment CreateAppointment(Appointment appointment, IHealthcareContext context)
        {
            context.Appointments.Add(appointment);
            context.SaveChange();
            return appointment;
        }
        #endregion 
    }
}
