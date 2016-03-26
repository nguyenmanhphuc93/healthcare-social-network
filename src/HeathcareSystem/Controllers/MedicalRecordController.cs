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
    [Authorize]
    [Route("api/[controller]/[action]")]
    public partial class MedicalRecordController : BaseController
    {
        private MedicalRecordRespository repository;
        public MedicalRecordRespository Repository
        {
            get
            {
                if (this.repository == null)
                {
                    this.repository = new MedicalRecordRespository();
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
        public IActionResult RequestRecord(RequestingRecord model)
        {
            if (!User.IsInRole("Doctor"))
            {
                return new HttpForbiddenResult();
            }
            model.DiseaseIds = model.DiseaseIds.Distinct().ToList();

            model.DiseaseIds.ForEach(id =>
            {
                var request = new RequestRecord
                {
                    DoctorId = CurrentUser.Id,
                    PatientId = model.PatientId,
                    RecordId = model.RecordId,
                    Status = RequestRecordStatus.Pending,
                    DiseaseId = id,
                };
                context.RequestRecords.Add(request);
            });
            context.SaveChange();

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetRequestedRecordByPatient(int id, int currentRecordId)
        {
            if (!User.IsInRole("Doctor"))
            {
                return new HttpForbiddenResult();
            }
            return Ok(Repository.GetRequestedRecordByPatient(id, currentRecordId));
        }

        [HttpGet]
        public void A()
        {

        }

        [HttpPost]
        public void Post()
        {

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class MedicalRecordRespository
    {
        private IEnumerable<MedicalRecordViewmodel> GetMedicalRecord(Expression<Func<MedicalRecord, bool>> predicate)
        {
            using (var context = new HealthCareContext())
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
        }

        internal IEnumerable<MedicalRecordViewmodel> GetMedicalRecordByPatient(int id)
        {
            return GetMedicalRecord(x => x.Appointment.PatientId == id);
        }

        internal IEnumerable<MedicalRecordViewmodel> GetRequestedRecordByPatient(int id, int currentRecordId)
        {
            List<int> requestedDiseases = null;
            using (var context = new HealthCareContext())
            {
                var requests = context.RequestRecords.Where(n => n.Status == RequestRecordStatus.Accepted && n.RecordId == currentRecordId && n.PatientId == id);
                requestedDiseases = requests.Select(request => request.DiseaseId).ToList();
            }
            var records = GetMedicalRecord(record => record.Appointment.PatientId == id && record.MedicalResults.Any(result => requestedDiseases.Contains(result.DiseaseId)));
            return records;
        }
    }

}
