using Healthcare.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool Read { get; set; }
        [ForeignKey("Sender")]
        public int SenderId { get; set; }
        public Profile Sender { get; set; }
        [ForeignKey("Receiver")]
        public int ReceiverId { get; set; }
        public Profile Receiver { get; set; }
        public bool Active { get; set; }
        public string Url { get; set; }
    }
}
