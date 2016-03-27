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
                    this.repository = new MedicalRecordRespository(context);
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
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }
            if (!User.IsInRole("Doctor"))
            {
                return new HttpForbiddenResult();
            }
            model.DiseaseIds = model.DiseaseIds.Distinct().ToList();
            var request = new RequestRecord
            {
                DoctorId = CurrentUser.Id,
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

            CreateNotification(CurrentUser.Id, model.PatientId, $"Dr.{CurrentUser.FirstName} sent a request to ask your permission. ", $"/notification/1/{request.Id}");

            return Ok(request.Id);
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
                return Ok();
            }
            var request = context.RequestRecords.SingleOrDefault(x => x.Id == id);
            if (request == null || request.Status != RequestRecordStatus.Pending)
            {
                return new HttpNotFoundResult();
            }
            request.Status = status;
            context.SetState(request, EntityState.Modified);
            context.SaveChange();
            return Ok();
        }

        [HttpPut]
        public IActionResult FinishRecord(int id)
        {
            var record = context.MedicalRecords.Include(n => n.Appointment).SingleOrDefault(n => n.Id == id);
            if (record == null)
            {
                return new HttpNotFoundResult();
            }
            record.Appointment.Status = AppointmentStatus.Completed;
            context.SetState(record.Appointment, EntityState.Modified);
            context.SaveChange();
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
            var record = new MedicalRecord
            {
                AppointmentId = model.AppointmentId,
                CreatedDate = DateTime.Now,
            };
            context.MedicalRecords.Add(record);
            context.SaveChange();
            return Ok(record.Id);
        }
    }

    public class MedicalRecordRespository
    {
        IHealthcareContext context;
        public MedicalRecordRespository(IHealthcareContext context)
        {
            this.context = context;
        }
        private IEnumerable<MedicalRecord> GetMedicalRecord(Expression<Func<MedicalRecord, bool>> predicate)
        {
            var medicalRecords = context.MedicalRecords.Where(predicate)
                                                       .Include(n => n.Appointment).ThenInclude(x => x.Department).ThenInclude(x => x.Hospital)
                                                       .Include(n => n.Appointment).ThenInclude(x => x.Doctor)
                                                       .Include(n => n.Appointment).ThenInclude(x => x.Patient)
                                                       .Include(n => n.MedicalResults).ThenInclude(n => n.Disease)
                                                       .Include(n => n.Prescription).ThenInclude(n => n.Medicines);
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
            return GetMedicalRecord(x => x.Appointment.PatientId == id).Select(n => new MedicalRecordViewmodel(n)).OrderByDescending(n => n.CreatedDate);
        }

        internal IEnumerable<MedicalRecordViewmodel> GetRequestedRecordByPatient(int id, int currentRecordId)
        {
            var requests = context.RequestRecords
                                  .Include(n => n.Diseases)
                                  .Where(n => n.Status == RequestRecordStatus.Accepted && n.RecordId == currentRecordId && n.PatientId == id);
            var requestedDiseases = requests.SelectMany(request => request.Diseases.Select(d => d.DiseaseId)).Distinct().ToList();
            var records = GetMedicalRecord(record => record.Appointment.PatientId == id && record.MedicalResults.Any(result => requestedDiseases.Contains(result.DiseaseId)));
            return records.Select(n => new MedicalRecordViewmodel(n)).OrderByDescending(n => n.CreatedDate);
        }
    }

}
