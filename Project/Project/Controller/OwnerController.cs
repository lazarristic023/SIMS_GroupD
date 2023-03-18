using Project.Model;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controller
{
    public class OwnerController
    {
        public Owner Owner { get; set; }
        public AccommodationRepository AccommodationRepository { get; set; }

        public OwnerController()
        {
            Owner = new Owner();
            AccommodationRepository = new AccommodationRepository();
            LinkOwnerAccommodation();
        }
        public OwnerController(User u)
        {
            Owner = new Owner(u);
            AccommodationRepository = new AccommodationRepository();
            LinkOwnerAccommodation();
        }
        private void LinkOwnerAccommodation()
        {
            foreach(Accommodation accommodation in AccommodationRepository.GetAllAccommodations())
            {
                if(accommodation.OwnerId == Owner.User.Id)
                {
                    Owner.Accommodations.Add(accommodation);
                }
            }
        }

    }
}