using Project.Serializer;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Appointment: ISerializable
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public DateTime DateAndTimeOfAppointment { get; set; }

        public Appointment()
        {
            Id = -1;
            TourId = -1;
            DateAndTimeOfAppointment = DateTime.MinValue;
        }

        public Appointment(int tourid,DateTime dateAndTime)
        {
            Id = -1;
            TourId = tourid;
            DateAndTimeOfAppointment = dateAndTime;

        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                TourId.ToString(),
                DateAndTimeOfAppointment.ToString("MM/dd/yyyy hh:mm:ss tt") 
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourId = int.Parse(values[1]);
            DateAndTimeOfAppointment = DateTime.Parse(values[2]);
        }
    }
}
