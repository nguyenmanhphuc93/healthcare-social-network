using System.Collections.Generic;

namespace Healthcare.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        public virtual ICollection<Medicine> Medicines { get; set; }
        public string Note { get; set; }
    }
}