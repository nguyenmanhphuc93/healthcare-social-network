using Healthcare.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.Models
{
    public class MedicalResult
    {
        public int Id { get; set; }
        [ForeignKey("Disease")]
        public int DiseaseId { get; set; }
        public virtual Disease Disease { get; set; }
        public string Status { get; set; }
    }
}
