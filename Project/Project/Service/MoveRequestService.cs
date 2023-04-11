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
    public class MoveRequestService
    {
        private MoveRequestRepository _requestRepository;

        private AccommodationReservationService _reservationService;

        public MoveRequestService()
        {
            _requestRepository = new MoveRequestRepository();
            _reservationService = new AccommodationReservationService();
            LinkRequestsAndReservations();
        }

        public bool Create(AccommodationReservation reservation, DateTime startDate, DateTime endDate, string message)
        {
            if(!_reservationService.IsAccommodationFree(startDate, endDate, reservation.AccommodationId))
                return false;

            if(DoesRequestAlreadyExist(reservation)) return false;

            MoveRequest request = new(reservation.Accommodation.OwnerId, reservation.GuestId, reservation.Id, MoveRequestStatus.PENDING, "", message, startDate, endDate);
            _requestRepository.Add(request);
            LinkRequestsAndReservations();
            return true;
        }

        private bool DoesRequestAlreadyExist(AccommodationReservation reservation)
        {
            MoveRequest request = _requestRepository.GetAllRequests().Find(r => (r.ReservationId == reservation.Id) && (r.Status == MoveRequestStatus.PENDING));

            return request != null;
        }

        public List<MoveRequest> GetGuestsPendingRequests(int guestId)
        {
            List<MoveRequest> requests = new(GetGuestsMoveRequests(guestId));

            foreach (var request in GetGuestsMoveRequests(guestId))
            {
                if (request.Status != MoveRequestStatus.PENDING)
                {
                    requests.Remove(request);
                }
            }

            return requests;
        }

        public List<MoveRequest> GetGuestsAcceptedRequests(int guestId)
        {
            List<MoveRequest> requests = new(GetGuestsMoveRequests(guestId));

            foreach (var request in GetGuestsMoveRequests(guestId))
            {
                if (request.Status != MoveRequestStatus.ACCEPTED)
                {
                    requests.Remove(request);
                }
            }

            return requests;
        }

        public List<MoveRequest> GetGuestsDeclinedRequests(int guestId)
        {
            List<MoveRequest> requests = new(GetGuestsMoveRequests(guestId));

            foreach (var request in GetGuestsMoveRequests(guestId))
            {
                if (request.Status != MoveRequestStatus.DECLINED)
                {
                    requests.Remove(request);
                }
            }

            return requests;
        }

        private List<MoveRequest> GetGuestsMoveRequests(int guestId)
        {
            List<MoveRequest> requests = new();

            foreach (var request in _requestRepository.GetAllRequests())
            {
                if (request.GuestId == guestId)
                {
                    requests.Add(request);
                }
            }

            return requests;
        }

        private void LinkRequestsAndReservations()
        {
            foreach (var request in _requestRepository.GetAllRequests())
            {
                AccommodationReservation reservation = _reservationService.GetAllReservations().Find(r => r.Id == request.ReservationId);

                if(reservation != null)
                {
                    request.Reservation = reservation;
                }
                    
            }
        }

        public void SubscribeToRepository(IObserver observer)
        {
            _requestRepository.Subscribe(observer);
        }

    }
}
