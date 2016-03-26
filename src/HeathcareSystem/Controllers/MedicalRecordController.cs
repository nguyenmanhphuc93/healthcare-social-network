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
        public IActionResult Get()
        {
            return Ok(Repository.GetMedicalRecordByPatient(CurrentUser.Id));
        }

        [HttpGet("{id}")]
        public IActionResult GetByPatient(int id, int diseaseId = 0)
        {
            if (CurrentUser.Id == id || IsRequested(id))
            {
                IEnumerable<MedicalRecordViewmodel> records = null;
                if (diseaseId == 0)
                {
                    records = Repository.GetMedicalRecordByPatient(id);
                }
                else
                {
                    records = Repository.GetMedicalRecordByDisease(diseaseId, id);
                }
                return Ok(records);
            }
            return new HttpForbiddenResult();


        }

        private bool IsRequested(int id)
        {
            return true;
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
                                                           .Include(n => n.Department)
                                                           .Include(n => n.Doctor)
                                                           .Include(n => n.Hospital)
                                                           .Include(n => n.Patient)
                                                           .Include(n => n.Prescription)
                                                           .ThenInclude(n => n.Medicines)
                                                           .Include(n => n.Treatment)
                                                           .Include(n => n.Disease)
                                                           .AsEnumerable()
                                                           .Select(n => new MedicalRecordViewmodel(n));
                return medicalRecords;

            }
        }

        internal IEnumerable<MedicalRecordViewmodel> GetMedicalRecordByPatient(int id)
        {
            return GetMedicalRecord(x => x.PatientId == id && x.Status);
        }

        internal IEnumerable<MedicalRecordViewmodel> GetMedicalRecordByDisease(int id, int patientId)
        {
            return GetMedicalRecord(x => x.DiseaseId == id && x.PatientId == patientId && x.Status);
        }
    }

}
