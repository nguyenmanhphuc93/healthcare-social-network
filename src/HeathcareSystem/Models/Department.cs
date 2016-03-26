using System.ComponentModel.DataAnnotations;

namespace Healthcare.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public int Name { get; set; }
    }
}