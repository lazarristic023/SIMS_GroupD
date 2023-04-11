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
            foreach (var reservation in GetAllReservations())
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
            List<AccommodationReservation> reservations = new(GetAllReservations());

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

            foreach (var reservation in GetUserReservations(userId))
            {
                if (reservation.StartDate <= DateTime.Now.Date)
                {
                    allReservations.Remove(reservation);
                }
            }

            return allReservations;

        }

        public List<AccommodationReservation> GetUsersFormerReservations(int userId)
        {
            List<AccommodationReservation> allReservations = new(GetUserReservations(userId));

            foreach (var reservation in GetUserReservations(userId))
            {
                if (reservation.StartDate > DateTime.Now.Date)
                {
                    allReservations.Remove(reservation);
                }
            }

            return allReservations;

        }

        public bool IsAccommodationFree(DateTime start, DateTime end, int id)
        {
            AccommodationReservation reservation = 
                GetAccommodationReservations(id).Find(r => !(r.EndDate < start) && !(r.StartDate > end));

            return reservation == null;
        }

        public List<AccommodationReservation> GetAccommodationReservations(int id)
        {
            List<AccommodationReservation> reservations = new();

            foreach (var reservation in GetAllReservations())
            {
                if (reservation.AccommodationId == id)
                {
                    reservations.Add(reservation);
                }
            }

            return reservations;

        }

        public List<AccommodationReservation> GetAllReservations()
        {
            return _reservationRepository.GetAllReservations();
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
