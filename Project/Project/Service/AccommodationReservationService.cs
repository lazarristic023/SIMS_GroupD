using Project.Model;
using Project.Observer;
using Project.Repository;
using System;
using System.Collections.Generic;
using Project.RepositoryInterfaces;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class AccommodationReservationService
    {
        private readonly IAccommodationReservationRepository _reservationRepository;

        private readonly AccommodationService _accommodationService;


        public AccommodationReservationService()
        {
            _reservationRepository = Injector.Injector.CreateInstance<IAccommodationReservationRepository>();
            _accommodationService = new AccommodationService();
            LinkAccommodationsAndReservations();
        }


        public List<AccommodationReservation> GetGuestReservations(int guestId)
        {
            List<AccommodationReservation> reservations = new List<AccommodationReservation>();
            foreach (var reservation in GetAllReservations())
            {
                if (reservation.GuestId == guestId)
                {
                    reservations.Add(reservation);
                }
            }

            return reservations;
        }

        public List<AccommodationReservation> GetOwnerReservations(int ownerId)
        {
            List<AccommodationReservation> reservations = new List<AccommodationReservation>();
            foreach (var reservation in GetAllReservations())
            {
                if (reservation.Accommodation.OwnerId == ownerId)
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


        public List<AccommodationReservation> GetGuestsCurrentReservations(int guestId)
        {
            List<AccommodationReservation> allReservations = new(GetGuestReservations(guestId));

            foreach (var reservation in GetGuestReservations(guestId))
            {
                if (reservation.StartDate <= DateTime.Now.Date)
                {
                    allReservations.Remove(reservation);
                }
            }

            return allReservations;

        }

        public List<AccommodationReservation> GetGuestsFormerReservations(int guestId)
        {
            List<AccommodationReservation> allReservations = new(GetGuestReservations(guestId));

            foreach (var reservation in GetGuestReservations(guestId))
            {
                if (reservation.StartDate > DateTime.Now.Date)
                {
                    allReservations.Remove(reservation);
                }
            }

            return allReservations;

        }

        public List<AccommodationReservation> GetOwnersCurrentReservations(int ownerId)
        {
            List<AccommodationReservation> allReservations = new(GetOwnerReservations(ownerId));

            foreach (var reservation in GetOwnerReservations(ownerId))
            {
                if (reservation.StartDate <= DateTime.Now.Date)
                {
                    allReservations.Remove(reservation);
                }
            }

            return allReservations;

        }

        public List<AccommodationReservation> GetOwnersFormerReservations(int ownerId)
        {
            List<AccommodationReservation> allReservations = new(GetOwnerReservations(ownerId));

            foreach (var reservation in GetOwnerReservations(ownerId))
            {
                if (reservation.StartDate > DateTime.Now.Date)
                {
                    allReservations.Remove(reservation);
                }
            }

            return allReservations;

        }

        public bool IsAccommodationFree(DateTime start, DateTime end, int accommodatonId)
        {
            AccommodationReservation reservation = 
                GetAccommodationReservations(accommodatonId).Find(r => !(r.EndDate < start) && !(r.StartDate > end));

            return reservation == null;
        }

        public List<AccommodationReservation> GetAccommodationReservations(int accommodationId)
        {
            List<AccommodationReservation> reservations = new();

            foreach (var reservation in GetAllReservations())
            {
                if (reservation.AccommodationId == accommodationId)
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

        public AccommodationReservation GetReservationById(int reservationId)
        {
            return _reservationRepository.GetReservationById(reservationId);
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
