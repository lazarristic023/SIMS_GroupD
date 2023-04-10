using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Serializer;

namespace Project.Model
{
    public enum MoveRequestStatus { PENDING, ACCEPTED, DECLINED}
    public class MoveRequest : ISerializable
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int GuestId { get; set; }
        public int ReservationId { get; set; }
        public AccommodationReservation Reservation { get; set; }
        public string OwnerMessage { get; set; }
        public string GuestMessage { get; set; }

        public DateTime NewStartDate { get; set; }
        public DateTime NewEndDate { get; set; }
        public MoveRequestStatus Status { get; set;}

        public MoveRequest() 
        {
            OwnerMessage = string.Empty;
            GuestMessage = string.Empty;
            NewStartDate = DateTime.MinValue;
            NewEndDate = DateTime.MinValue;
        }

        public MoveRequest(int ownerId, int guestId, int reservationId, AccommodationReservation reservation, MoveRequestStatus status, string ownerMessage = "", string guestMessage = "", DateTime newStartDate = default, DateTime newEndDate = default)
        {
            OwnerId = ownerId;
            GuestId = guestId;
            ReservationId = reservationId;
            Reservation = reservation;
            OwnerMessage = ownerMessage;
            GuestMessage = guestMessage;
            Status = status;
            NewStartDate = newStartDate;
            NewEndDate = newEndDate;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), GuestId.ToString(), OwnerId.ToString(), ReservationId.ToString(), OwnerMessage, GuestMessage, RequestStatusToString(), NewStartDate.ToString(), NewEndDate.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            OwnerId = Convert.ToInt32(values[2]);
            ReservationId = Convert.ToInt32(values[3]);
            OwnerMessage = values[4];
            GuestMessage = values[5];
            Status = StringToRequestStatus(values[6]);
            NewStartDate = Convert.ToDateTime(values[7]);
            NewEndDate = Convert.ToDateTime(values[8]);
        }

        private string RequestStatusToString()
        {
            if (Status == MoveRequestStatus.PENDING)
            {
                return "PENDING";
            }
            else if (Status == MoveRequestStatus.ACCEPTED)
            {
                return "ACCEPTED";
            }
            else
            {
                return "DECLINED";
            }

        }

        private MoveRequestStatus StringToRequestStatus(string status)
        {
            if (status == "PENDING")
            {
                return MoveRequestStatus.PENDING;
            }
            else if (status == "ACCEPTED")
            {
                return MoveRequestStatus.ACCEPTED;
            }
            else
                return MoveRequestStatus.DECLINED;

        }



    }
}
