using HeathcareSystem.Models;
using Microsoft.AspNet.Mvc;
using System.Linq;
using Microsoft.Data.Entity;

namespace HeathcareSystem.Controllers
{
    public partial class MedicalRecordController : BaseController
    {
        private Notification CreateNotification(int sender, int receiverId, string content, string url)
        {
            var notification = new Notification { Content = content, Url = url, SenderId = sender, ReceiverId = receiverId };
            context.Notifications.Add(notification);
            context.SaveChange();
            return notification;
        }

        public IActionResult GetNotifications()
        {
            var notifions = context.Notifications.Where(n => n.ReceiverId == CurrentUser.Id).Select(n => new
            {
                Content = n.Content,
                Read = n.Read,
                Url = n.Url
            });
            return Ok(notifions);
        }

        public IActionResult ReadNotification(int id)
        {
            var notification = context.Notifications.SingleOrDefault(n => n.Id == id);
            if (notification != null)
            {
                notification.Read = true;
                context.SetState(notification, EntityState.Modified);
                context.SaveChange();
            }
            return Ok();
        }
    }
}
