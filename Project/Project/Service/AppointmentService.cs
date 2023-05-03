using Project.Model;
using Project.Observer;
using Project.Repository;
using Project.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class AppointmentService
    {
        private IAppointmentRepository appointmentRepository;
        //AppointmentRepository appointmentRepository { get; set; }
        
        private readonly CouponService couponService;
        private readonly TourReservationService tourReservationService;
        public AppointmentService()
        {
            //appointmentRepository = new AppointmentRepository();
            appointmentRepository = Injector.Injector.CreateInstance<IAppointmentRepository>();

            couponService = new CouponService();
            tourReservationService = new TourReservationService();
        }


        public void Subscribe(IObserver observer)
        {
            appointmentRepository.Subscribe(observer);
        }

        public void Create(int tourId, DateTime date)
        {

            Appointment appointment = new Appointment(tourId, date);
            appointmentRepository.Add(appointment);

        }

        public void RefreshAppointments()
        {
            appointmentRepository.RefreshAppointments();
        }

        public List<Appointment> GetByTourId(int id)
        {
            List<Appointment> allAppointments = appointmentRepository.GetAll();
            List<Appointment> appointments = new List<Appointment>();

            foreach (Appointment appoint in allAppointments)
            {
                if (appoint.TourId == id)
                {
                    appointments.Add(appoint);
                }
            }

            return appointments;

        }

        public int GetTourId(int id)
        {
            return appointmentRepository.GetById(id).TourId;
        }

        public Appointment GetById(int id)
        {
            return appointmentRepository.GetById(id);
        }

        public void Cancel(Appointment appointment)
        {
            appointmentRepository.Cancel(appointment.Id);
            List<User> guests =  tourReservationService.GetGuestsWithReservation(appointment.Id);

            foreach(User guest in guests)
            {
                couponService.Create(guest.Id, appointment.DateAndTimeOfAppointment.AddMonths(6));
            }

        }

        public void CompleteTour(int id)
        {
            appointmentRepository.CompleteTour(id);
        }

    }
}
