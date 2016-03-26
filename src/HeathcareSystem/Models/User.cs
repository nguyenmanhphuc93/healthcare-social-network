using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Healthcare.Models
{
    public class User : IdentityUser<long>
    {
        [ForeignKey("Profile")]
        public int ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
        public static object Identity { get; internal set; }
    }
}