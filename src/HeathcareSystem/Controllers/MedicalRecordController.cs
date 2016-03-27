using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Healthcare.Models;
using Microsoft.Data.Entity;
using System.Net;
using HeathcareSystem.ViewModels;
using Microsoft.AspNet.Authorization;
using HeathcareSystem.Models;
using System.Linq.Expressions;
using HeathcareSystem.BindingModels;
using HeathcareSystem.Enums;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HeathcareSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    public partial class MedicalRecordController : BaseController
    {
        private MedicalRecordRespository repository;
        public MedicalRecordRespository Repository
        {
            get
            {
                if (this.repository == null)
                {
                    this.repository = new MedicalRecordRespository(context, CurrentUser);
                }
                return this.repository;
            }
        }

        public MedicalRecordController(IHealthcareContext context) : base(context)
        {
        }

        [HttpGet]
        public IActionResult GetRecords()
        {
            return Ok(Repository.GetMedicalRecordByPatient(CurrentUser.Id));
        }

        [HttpPost]
        public IActionResult RequestRecord([FromBody] RequestingRecord model)
        {
            if (!User.IsInRole("Doctor"))
            {
                return new HttpForbiddenResult();
            }
            var record = context.MedicalRecords.SingleOrDefault(n => n.Appointment.Status == AppointmentStatus.InProcess && n.Appointment.Request.PatientId == model.PatientId);
            if (record == null)
            {
                return new BadRequestResult();
            }
            model.RecordId = record.Id;
            int requestId = Repository.AddRequest(model);


            CreateNotification(CurrentUser.Id, model.PatientId, $"Dr.{CurrentUser.DisplayName} sent a request to ask your permission. ", $"/notification/1/{requestId}");

            return Ok(requestId);
        }

        [HttpGet]
        public IActionResult GetRequestedRecordByPatient(int id, int currentRecordId)
        {
            if (!User.IsInRole("Doctor"))
            {
                return new HttpForbiddenResult();
            }
            return Ok(Repository.GetRequestedRecordByPatient(id, currentRecordId));
        }

        [HttpGet]
        public IActionResult GetRequestRecord(int id)
        {
            return Ok(Repository.GetRequestRecordViewModel(id));
        }

        [HttpPut]
        public IActionResult ComfirmRequest(int id, RequestRecordStatus status)
        {
            if (status == RequestRecordStatus.Pending)
            {
                return new HttpForbiddenResult();
            }
            Repository.ComfirmRequest(id, status);

            return Ok();
        }

        [HttpPost]
        public IActionResult CreateRecord([FromBody] CreateRecordBindingModel model)
        {
            var appointment = context.Appointments.SingleOrDefault(x => x.Id == model.AppointmentId);
            if (appointment == null)
            {
                return new HttpNotFoundResult();
            }
            if (CurrentUser.Id != appointment.DoctorId)
            {
                return new HttpForbiddenResult();
            }
            var record = context.MedicalRecords.SingleOrDefault(n => n.AppointmentId == model.AppointmentId);
            if (record != null)
            {
                return Ok(record.Id);
            }
            return Ok(Repository.CreateRecord(model));
        }
    }

    public class MedicalRecordRespository
    {
        IHealthcareContext context;
        Profile currentUser;
        public MedicalRecordRespository(IHealthcareContext context, Profile currentUser)
        {
            this.context = context;
            this.currentUser = currentUser;
        }
        private IEnumerable<MedicalRecordViewmodel> GetMedicalRecord(Expression<Func<MedicalRecord, bool>> predicate)
        {
            var medicalRecords = context.MedicalRecords.Where(predicate)
                                                       .Include(n => n.Appointment).ThenInclude(x => x.Department).ThenInclude(x => x.Hospital)
                                                       .Include(n => n.Appointment).ThenInclude(x => x.Doctor)
                                                       .Include(n => n.Appointment).ThenInclude(x => x.Patient)
                                                       .Include(n => n.MedicalResults).ThenInclude(n => n.Disease)
                                                       .Include(n => n.Prescription).ThenInclude(n => n.Medicines)
                                                       .AsEnumerable()
                                                       .Select(n => new MedicalRecordViewmodel(n));
            return medicalRecords;

        }

        private RequestRecord GetRequestRecord(Expression<Func<RequestRecord, bool>> predicate)
        {
            var request = context.RequestRecords
                                 .Include(n => n.Patient)
                                 .Include(n => n.Record)
                                 .Include(n => n.Diseases).ThenInclude(n => n.Disease)
                                 .SingleOrDefault(predicate);
            return request;
        }

        internal RequestRecordViewmodel GetRequestRecordViewModel(int id)
        {
            return new RequestRecordViewmodel(GetRequestRecord(x => x.Id == id));
        }

        internal RequestRecord GetRequestRecord(int id)
        {
            return GetRequestRecord(x => x.Id == id);
        }

        internal IEnumerable<MedicalRecordViewmodel> GetMedicalRecordByPatient(int id)
        {
            return GetMedicalRecord(x => x.Appointment.PatientId == id);
        }

        internal IEnumerable<MedicalRecordViewmodel> GetRequestedRecordByPatient(int id, int currentRecordId)
        {
            var requests = context.RequestRecords
                                  .Include(n => n.Diseases)
                                  .Where(n => n.Status == RequestRecordStatus.Accepted && n.RecordId == currentRecordId && n.PatientId == id).ToList();
            var requestedDiseases = requests.SelectMany(request => request.Diseases.Select(d => d.DiseaseId)).Distinct().ToList().ToList();
            var records = GetMedicalRecord(record => record.Appointment.PatientId == id && record.MedicalResults.Any(result => requestedDiseases.Contains(result.DiseaseId)));
            return records;
        }

        internal int AddRequest(RequestingRecord model)
        {
            model.DiseaseIds = model.DiseaseIds.Distinct().ToList();

            var request = new RequestRecord
            {
                DoctorId = currentUser.Id,
                PatientId = model.PatientId,
                RecordId = model.RecordId,
                Status = RequestRecordStatus.Pending,
                Diseases = new List<DiseasesInRequest>(),
            };

            model.DiseaseIds.ForEach(id =>
            {
                request.Diseases.Add(
                                    new DiseasesInRequest
                                    {
                                        DiseaseId = id,
                                    });
            });
            context.RequestRecords.Add(request);
            context.SaveChange();
            return request.Id;
        }

        internal int CreateRecord(CreateRecordBindingModel model)
        {
            var record = new MedicalRecord
            {
                AppointmentId = model.AppointmentId,
                CreatedDate = DateTime.Now,
            };
            var appointment = context.Appointments.SingleOrDefault(x => x.Id == model.AppointmentId);
            appointment.Status = AppointmentStatus.InProcess;
            context.SetState(appointment, EntityState.Modified);
            context.MedicalRecords.Add(record);
            context.SaveChange();
            return record.Id;
        }

        internal void ComfirmRequest(int id, RequestRecordStatus status)
        {
            var request = context.RequestRecords.SingleOrDefault(x => x.Id == id);
            if (request == null || request.Status != RequestRecordStatus.Pending)
            {
                return;
            }
            request.Status = status;
            context.SetState(request, EntityState.Modified);
            context.SaveChange();
        }
    }

}
