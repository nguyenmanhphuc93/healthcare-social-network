using Healthcare.Models;
using HeathcareSystem.Enums;
using HeathcareSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.ViewModels
{
    public class RequestRecordViewmodel
    {
        public RequestRecordViewmodel(RequestRecord requestRecord)
        {
            if (requestRecord != null)
            {
                this.Id = requestRecord.Id;
                this.Status = requestRecord.Status;
                this.Doctor = new ProfileViewmodel(requestRecord.Doctor);
                this.Patient = new ProfileViewmodel(requestRecord.Patient);
                this.Record = new MedicalRecordViewmodel(requestRecord.Record);
                this.Diseases = requestRecord.Diseases?.Select(x => new DiseaseViewmodel(x.Disease)).ToList() ?? new List<DiseaseViewmodel>();
            }
        }
        public int Id { get; set; }
        public ProfileViewmodel Doctor { get; set; }
        public ProfileViewmodel Patient { get; set; }
        public List<DiseaseViewmodel> Diseases { get; set; }
        public RequestRecordStatus Status { get; set; }
        public MedicalRecordViewmodel Record { get; set; }
    }
}
