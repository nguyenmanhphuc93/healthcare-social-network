using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Healthcare.Models
{
    public class Hospital
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}