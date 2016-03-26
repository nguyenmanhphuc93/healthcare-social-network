using System.ComponentModel.DataAnnotations.Schema;

namespace Healthcare.Models
{
    public class Treatment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        [ForeignKey("Prescription")]
        public int PrescriptionId { get; set; }
        public virtual Prescription Prescription { get; set; }
    }
}