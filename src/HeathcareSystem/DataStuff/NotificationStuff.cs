using Healthcare.Models;
using HeathcareSystem.Models;
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
            var doctors = context.DoctorInDepartments.Select(a => a.Doctor).ToList();
            var users = context.Users.ToList();
            var random = new Random();
            for(var i = 0; i< doctors.Count; i++)
            {
                var user = users[random.Next(users.Count)].Profile;
                while(user.Id == doctors[i].Id)
                {
                    user = users[random.Next(users.Count)].Profile;
                }
                context.Notifications.Add(new Notification()
                {
                    Active = true,
                    Content = "Sleep more!!!",
                    Read = random.Next(2)>0? true :false,
                    Sender = doctors[i],
                    SenderId = doctors[i].Id,
                    Receiver = user,
                    ReceiverId = user.Id
                });
            }
            context.SaveChange();

        }
    }
}
