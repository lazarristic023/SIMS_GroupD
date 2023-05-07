using Project.Model;
using Project.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class TourRequestService
    {
        private ITourRequestRepository tourRequestRepository;
        private readonly LocationService locationService;
        public TourRequestService()
        {
            tourRequestRepository = Injector.Injector.CreateInstance<ITourRequestRepository>();
            locationService = new LocationService();
        }

        public void Create(int locationId, string description, string language, int guestNum, DateTime startDate, DateTime endDate, DateTime acceptedAppointment, TourRequest.STATUS status)
        {
            TourRequest tourRequest = new TourRequest(locationId,description,language,guestNum,startDate,endDate,acceptedAppointment,status);

            int requestId = tourRequestRepository.Add(tourRequest);
        }

        public TourRequest GetById(int id)
        {
            return tourRequestRepository.GetById(id);
        }

        public List<TourRequest> GetAll()
        {
            List<TourRequest> requests = new List<TourRequest>();

            foreach(TourRequest req in tourRequestRepository.GetAll())
            {
                req.Location = locationService.GetById(req.LocationId);
                requests.Add(req);
            }

            return requests;
        }

        public void Remove(int id)
        {
            tourRequestRepository.Remove(id);
        }
    }
}
