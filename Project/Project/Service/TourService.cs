using Project.Model;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Project.Service
{
    public class TourService
    {
        TourRepository tourRepository;

        public TourService()
        {
            tourRepository = new TourRepository();

        }

        public int Create(Location location, string name, string description, string language, int maxGuests, int duration)
        {


            Tour tour = new Tour(location, name, description, language, maxGuests, duration);

            int tourId = tourRepository.Add(tour);

            return tourId;

        }

        public DateTime BuildDate(DateTime date, string time)
        {
            string[] splitedTime = time.Split(':');
            DateTime newDate = new DateTime(date.Year, date.Month, date.Day, int.Parse(splitedTime[0]), int.Parse(splitedTime[1]), 0);
            return newDate;
        }


    }
}
