using Project.Model;
using Project.Repository;
using Project.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class AccommodationService
    {
        private IAccommodationRepository _accommodationRepository;
        private IAccommodationImageRepository _imageRepository;
        private IUserRepository _userRepository;

        public AccommodationService() 
        {
            _accommodationRepository = Injector.Injector.CreateInstance<IAccommodationRepository>();
            _imageRepository = Injector.Injector.CreateInstance<IAccommodationImageRepository>();
            _userRepository = Injector.Injector.CreateInstance<IUserRepository>();
            LinkAccommodationsAndImages();
            LinkAccommodationsAndOwners();
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

        private void LinkAccommodationsAndOwners()
        {
            foreach (var accommodation in _accommodationRepository.GetAllAccommodations())
            {
                var owner = _userRepository.GetById(accommodation.OwnerId);
                if (owner != null)
                {
                    accommodation.Owner = owner;
                }
            }
        }

        public List<Location> GetAccommodationLocationsList()
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
