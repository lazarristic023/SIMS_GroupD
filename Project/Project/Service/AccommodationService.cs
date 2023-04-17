using Project.Model;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class AccommodationService
    {
        private AccommodationRepository _accommodationRepository;
        private AccommodationImageRepository _imageRepository;

        public AccommodationService() 
        {
            _accommodationRepository = new AccommodationRepository();
            _imageRepository = new AccommodationImageRepository();
            LinkAccommodationsAndImages();
        }

        public List<Accommodation> GetAllAccommodations()
        {
            return _accommodationRepository.GetAllAccommodations();
        }

        private void LinkAccommodationsAndImages()
        {
            foreach (var image in _imageRepository.GetAllImages())
            {
                Accommodation accommodation = _accommodationRepository.GetAllAccommodations().Find(a => a.Id == image.AccommodationId);
                if (accommodation == null)
                {
                    continue;
                }
                if (accommodation.Images.Exists(i => i.Id == image.Id))
                {
                    continue;
                }

                accommodation.Images.Add(image);
            }
        }

        private List<Location> GetAccommodationLocationsList()
        {
            List<Location> accommodationLocations = new List<Location>();
            foreach (var accommodation in _accommodationRepository.GetAllAccommodations())
            {
                Location location =
                    accommodationLocations.Find(l => (l.City == accommodation.Location.City) && (l.Country == accommodation.Location.Country));

                if (location == null)
                {
                    accommodationLocations.Add(accommodation.Location);
                }
            }

            return accommodationLocations;

        }

        public Accommodation GetAccommodationById(int id)
        {
            return _accommodationRepository.GetAccommodationById(id);
        }


    }
}
