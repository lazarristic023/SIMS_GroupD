using Project.Model;
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
        }

        public bool Create(AccommodationReservation reservation, DateTime startDate, DateTime endDate, string message)
        {
            if(!_reservationService.IsAccommodationFree(startDate, endDate, reservation.AccommodationId))
                return false;

            MoveRequest request = new(reservation.Accommodation.OwnerId, reservation.GuestId, reservation.Id, MoveRequestStatus.PENDING, "", message, startDate, endDate);
            _requestRepository.Add(request);

            return true;
        }

    }
}
