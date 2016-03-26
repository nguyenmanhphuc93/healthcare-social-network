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
            for (var i = 0; i < doctors.Count; i++)
            {
                var user = users[random.Next(users.Count)].Profile;
                while (user.Id == doctors[i].Id)
                {
                    user = users[random.Next(users.Count)].Profile;
                }
                context.Notifications.Add(new Notification()
                {
                    Active = true,
                    Content = "Sleep more!!!",
                    Read = random.Next(2) > 0 ? true : false,
                    Sender = doctors[i],
                    SenderId = doctors[i].Id,
                    Receiver = user,
                    ReceiverId = user.Id,
                    Url = $"healthcare.com/Chat/{user.Id}"

                });
            }
            var user2 = context.Users.SingleOrDefault(a => a.UserName == "Patient1").Profile;
            context.Notifications.Add(new Notification()
            {
                Active = true,
                Content = "Take medicine!!!",
                Read = true,
                Sender = doctors[1],
                SenderId = doctors[1].Id,
                Receiver = user2,
                ReceiverId = user2.Id,
                Url =$"healthcare.com/Chat/{user2.Id}"
            });
            context.Notifications.Add(new Notification()
            {
                Active = true,
                Content = "Sleep",
                Read = true,
                Sender = doctors[0],
                SenderId = doctors[0].Id,
                Receiver = user2,
                ReceiverId = user2.Id,
                Url = $"healthcare.com/Chat/{user2.Id}"
            });
            context.SaveChange();

        }
    }
}
