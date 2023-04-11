using Project.Model;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class TourReservationService
    {
        TourReservationRepository tourReservationRepository;
        UserRepository userRepository;

        public TourReservationService()
        {
            tourReservationRepository = new TourReservationRepository();
            userRepository = new UserRepository();
        }

        public List<User> GetGuestsWithReservation(int id)
        {
            List<User> guestList = new List<User>();
            foreach(TourReservation reservation in tourReservationRepository.GetReservationByTourId(id))
            {
                guestList.Add(userRepository.GetById(reservation.GuestId));
            }

            return guestList;
        }
    }
}
