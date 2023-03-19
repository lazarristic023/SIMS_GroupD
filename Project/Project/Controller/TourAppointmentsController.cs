using Project.Model;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controller
{
    public class TourAppointmentsController
    {
        TourAppointmentsRepositorycs tourAppointmentsRepository { get; set; }

        public TourAppointmentsController()
        {
            tourAppointmentsRepository = new TourAppointmentsRepositorycs();
        }

        public void Create(int tourId, List<DateTime> tourDates)
        {

            TourAppointments tourAppointments = new TourAppointments(tourId,tourDates);
            tourAppointmentsRepository.Add(tourAppointments);

        }
    }
}
