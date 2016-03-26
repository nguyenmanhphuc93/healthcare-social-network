using Healthcare.Models;
using HeathcareSystem.Models;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeathcareSystem.DataStuff
{
    public static class NotificationStuff
    {
        public static void SeedNotification(this HealthCareContext context)
        {
            var patient1 = context.Users.Include(n => n.Profile).SingleOrDefault(n => n.UserName.ToLower() == "patient1");
            var doctor0 = context.Users.Include(n => n.Profile).SingleOrDefault(n => n.UserName.ToLower() == "doctor0");

            context.Notifications.Add(new Notification()
            {
                Active = true,
                Content = "Sleep",
                Read = false,
                SenderId = doctor0.Profile.Id,
                ReceiverId = patient1.Profile.Id,
                Url = $"healthcare.com/Chat/{patient1.Profile.Id}"
            });
            context.SaveChange();

        }
    }
}
