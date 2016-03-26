using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.BindingModels
{
    public class RequestingRecord
    {
        public int PatientId { get; set; }
        public List<int> DiseaseIds { get; set; }
        public int RecordId { get; set; }
    }
}
