using Project.Model;
using Project.Repository;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{

    public class PresentGuestsService
    {
        PresentGuestsRepository presentGuestsRepository;
        UserRepository userRepository;

        

        public PresentGuestsService()
        {
            presentGuestsRepository = new PresentGuestsRepository();
            userRepository = new UserRepository();

            
        }

        public List<User> GetPresentGuestsOfTheAppointment(int id)
        {
            List<User> guestList = new List<User>();
            List<PresentGuests> presentGuests = presentGuestsRepository.GetAll();

            foreach(PresentGuests present in presentGuests)
            {
                if(present.AppointmentId == id)
                {
                    guestList.Add(userRepository.GetById(present.GuestId));
                }
            }

            return guestList;
        }

        public int GetUnder18(int id)
        {
            List<User> guests = GetPresentGuestsOfTheAppointment(id);
            int count = 0;
            foreach(User user in guests)
            {
                if (user.Age < 18) { count++; }
            }

            return count;
        }

        public int GetBetween18and50(int id)
        {
            List<User> guests = GetPresentGuestsOfTheAppointment(id);
            int count = 0;
            foreach (User user in guests)
            {
                if (user.Age > 18 && user.Age < 50) { count++; }
            }

            return count;
        }

        public int GetOver50(int id)
        {
            List<User> guests = GetPresentGuestsOfTheAppointment(id);
            int count = 0;
            foreach (User user in guests)
            {
                if (user.Age > 50) { count++; }
            }

            return count;
        }

        public int GetNumberOfGuests(int id)
        {
            List<User> guests = GetPresentGuestsOfTheAppointment(id);
            return guests.Count;
        }

        public int GetNumberOfGuestsWithCoupon(int id)
        {
            int guestsWithCoupon = 0;
            List<PresentGuests> guests = presentGuestsRepository.GetAll();
            foreach(PresentGuests guest in guests)
            {
                if(guest.AppointmentId == id && guest.Coupon)
                {
                    guestsWithCoupon++;
                }
            }
            return guestsWithCoupon;
        }

        

    }
}
