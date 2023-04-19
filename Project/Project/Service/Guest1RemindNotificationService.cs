using Project.Injector;
using Project.Model;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToastNotifications;
using ToastNotifications.Messages;

namespace Project.Service
{
    public class Guest1RemindNotificationService
    {
        private readonly Guest1RemindNotificationRepository _repository;
        public Guest1RemindNotificationService()
        {
            _repository = new();
        }

        public void NotifyGuest(Notifier notifier, int userId)
        {
            List<Guest1RemindNotification> notifications = new(GetGuestsNotifications(userId));

            foreach (var notification in notifications)
            {
                if (notification.Date.Date.AddDays(5) >= DateTime.Now.Date)
                {
                    notifier.ShowInformation(notification.Message);
                }else
                    Remove(notification.Id);
            }

        }


        private List<Guest1RemindNotification> GetGuestsNotifications(int userId)
        {
            List<Guest1RemindNotification> notifications = new List<Guest1RemindNotification>();

            foreach (var notification in _repository.GetAllNotifications())
            {
                if (notification.Reservation.GuestId == userId)
                {
                    notifications.Add(notification);
                }
            }

            return notifications;
        }

        private void Add(Guest1RemindNotification notification)
        {
            _repository.Add(notification);
        }

        public void Remove(int id)
        {
            _repository.Remove(id);
        }

        public void RemoveByReservation(int reservationId)
        {
            var notification = _repository.GetAllNotifications().Find(n => n.ReservationId == reservationId);

            if (notification != null)
            {
                _repository.Remove(notification.Id);
            }
        }

    }
}
