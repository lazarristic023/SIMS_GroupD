using Project.Model;
using Project.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModel.TourGuideViewModel
{
    public class RequestDatePickerViewModel: ViewModelBase
    {
		private DateTime _date;
		public DateTime Date
		{
			get
			{
				return _date;
			}
			set
			{
				_date = value;
				OnPropertyChanged(nameof(Date));
			}
		}

		private DateTime _time;
		public DateTime Time
		{
			get
			{
				return _time;
			}
			set
			{
				_time = value;
				OnPropertyChanged(nameof(Time));
			}
		}

		private TourRequest tourRequest;

        private readonly TourRequestService tourRequestService;
        private readonly TourService tourService;
		private readonly AppointmentService appointmentService;

        public RequestDatePickerViewModel(TourRequest request)
        {
			tourRequestService = new TourRequestService();
			tourService = new TourService();

            tourRequest = request;

        }


    }
}
