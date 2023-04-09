using Project.Model;
using Project.Observer;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class AccommodationReservationService
    {
        private readonly AccommodationReservationRepository _reservationRepository;
        public AccommodationReservationService()
        {
            _reservationRepository = new AccommodationReservationRepository();
        }


        public List<AccommodationReservation> GetUserReservations(int userId)
        {
            List<AccommodationReservation> reservations = new List<AccommodationReservation>();
            foreach (var reservation in _reservationRepository.GetAllReservations())
            {
                if (reservation.GuestId == userId)
                {
                    reservations.Add(reservation);
                }
            }

            return reservations;
        }

        public void Remove(AccommodationReservation reservation)
        {
            _reservationRepository.Remove(reservation.Id);
        }
        public void SubscribeToReservationRepository(IObserver observer)
        {
            _reservationRepository.Subscribe(observer);
        }


    }
}
