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

        private readonly AccommodationService _accommodationService;
        public AccommodationReservationService()
        {
            _reservationRepository = new AccommodationReservationRepository();
            _accommodationService = new AccommodationService();
            LinkAccommodationsAndReservations();
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

        private void LinkAccommodationsAndReservations()
        {
            List<AccommodationReservation> reservations = new(_reservationRepository.GetAllReservations());

            foreach (var reservation in reservations)
            {
                Accommodation accommodation = _accommodationService.GetAllAccommodations().Find(a => a.Id == reservation.AccommodationId);

                if (accommodation == null)
                    continue;

                reservation.Accommodation = accommodation;

            }

        }

        public List<AccommodationReservation> GetUsersCurrentReservations(int userId)
        {
            List<AccommodationReservation> allReservations = new(GetUserReservations(userId));

            foreach (var reservation in allReservations)
            {
                if (reservation.EndDate < DateTime.Now.Date)
                {
                    allReservations.Remove(reservation);
                }
            }

            return allReservations;

        }

        public List<AccommodationReservation> GetUsersFormerReservations(int userId)
        {
            List<AccommodationReservation> allReservations = new(GetUserReservations(userId));

            foreach (var reservation in allReservations)
            {
                if (!(reservation.EndDate < DateTime.Now.Date))
                {
                    allReservations.Remove(reservation);
                }
            }

            return allReservations;

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
