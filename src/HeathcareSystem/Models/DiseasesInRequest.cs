using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.Models
{
    public class DiseasesInRequest
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("RequestRecord")]
        public int RequestId { get; set; }
        public RequestRecord RequestRecord { get; set; }
        [ForeignKey("Disease")]
        public int DiseaseId { get; set; }
        public Disease Disease { get; set; }
    }
}
