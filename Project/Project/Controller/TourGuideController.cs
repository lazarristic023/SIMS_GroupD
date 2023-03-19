using Project.Model;
using Project.Observer;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.Controller
{
    public class TourGuideController
    {

        TourRepository tourRepository;
        private readonly LocationController _locationController;


        public TourGuideController()
        {
            tourRepository = new TourRepository();
        }


        public void Subscribe(IObserver observer)
        {
            tourRepository.Subscribe(observer);
        }


        public int Create(int locationId,string name, string description, string language ,int maxGuests, int duration)
        {
            

            Tour tour = new Tour(locationId,name,description,language,maxGuests,duration);
            
            int tourId = tourRepository.Add(tour);

            return tourId;

        }

        public List<Tour> GetAllTours()
        {
            return tourRepository.GetAll();
        }
    }
}
