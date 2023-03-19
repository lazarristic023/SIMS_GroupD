using Project.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class TourReservation : ISerializable
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int GuestId { get; set; } 

        public User Guest { get; set; } 

        public int TourId { get; set; }

        public Tour Tour { get; set; }

        public TourReservation() { }

        public TourReservation(int id, DateTime start, DateTime end, int guestId, int tourId)
        {
            Id = id;
            StartDate = start;
            EndDate = end;
            GuestId = guestId;
            TourId = tourId;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            StartDate = DateTime.Parse(values[1]);
            EndDate = DateTime.Parse(values[2]);
            GuestId = int.Parse(values[3]);
            TourId = int.Parse(values[4]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), StartDate.ToString(), EndDate.ToString(), GuestId.ToString(), TourId.ToString() };
            return csvValues;
        }
    }
}
