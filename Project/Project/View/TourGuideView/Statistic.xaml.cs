using Project.Model;
using Project.Observer;
using Project.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project.View.TourGuideView
{
    /// <summary>
    /// Interaction logic for Statistic.xaml
    /// </summary>
    /// 



    public partial class Statistic : Window,INotifyPropertyChanged,IObserver
    {
        public ObservableCollection<Tour> CompletedTours { get; set; }
        private readonly TourService tourService;
        private readonly PresentGuestsService presentGuestsService;

        

        public event PropertyChangedEventHandler? PropertyChanged;

        public Tour SelectedCompletedTour { get; set; }

        private string _name;
        public string NameOfTour
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();

                }
            }
        }

        private int _under18;
        public int Under18
        {
            get => _under18;
            set
            {
                if (value != _under18)
                {
                    _under18 = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _over50;
        public int Over50
        {
            get => _over50;
            set
            {
                if (value != _over50)
                {
                    _over50 = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _percent18;
        public double Percent18
        {
            get => _percent18;
            set
            {
                if(value != _percent18)
                {
                    _percent18 = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _percent50;
        public double Percent50
        {
            get => _percent50;
            set
            {
                if (value != _percent50)
                {
                    _percent50 = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _percent1850;
        public double Percent1850
        {
            get => _percent1850;
            set
            {
                if (value != _percent1850)
                {
                    _percent1850 = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _between18and50;
        public int Between18and50
        {
            get => _between18and50;
            set
            {
                if (value != _between18and50)
                {
                    _between18and50 = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _appointmentdate;
        public DateTime AppointmentDate
        {
            get => _appointmentdate;
            set
            {
                if(value != _appointmentdate)
                {
                    _appointmentdate = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _coupon;
        public double Coupon
        {
            get => _coupon;
            set
            {
                if(value != _coupon)
                {
                    _coupon = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _couponpercent;
        public string CouponPercent
        {
            get => _couponpercent;
            set
            {
                if (value != _couponpercent)
                {
                    _couponpercent = value;
                    OnPropertyChanged();
                }
            }
        }

        public Statistic()
        {
            InitializeComponent();
            DataContext = this;

            tourService = new TourService();
            presentGuestsService = new PresentGuestsService();
            CompletedTours = new ObservableCollection<Tour>(tourService.GetCompletedTours());

            NameOfTour = "";
            AppointmentDate = DateTime.Now;
            Under18 = 0;
            Over50 = 0;
            Between18and50 = 0;
            Percent18 = 0;
            Percent50 = 0;
            Percent1850 = 0;
            Coupon = 0;
            CouponPercent = "0%";
            dateElement.IsEnabled = false;
            

        }

        public void Update()
        {
            UpdateCompletedTours();
        }

        public void UpdateCompletedTours() {
            CompletedTours.Clear();
            foreach (var tour in tourService.GetCompletedTours()) 
            {
                CompletedTours.Add(tour);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void tourList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NameOfTour = SelectedCompletedTour.Name;
            AppointmentDate = SelectedCompletedTour.TourAppointment.DateAndTimeOfAppointment;
            Under18 = presentGuestsService.GetUnder18(SelectedCompletedTour.TourAppointment.Id);
            Over50 = presentGuestsService.GetOver50(SelectedCompletedTour.TourAppointment.Id);
            Between18and50 = presentGuestsService.GetBetween18and50(SelectedCompletedTour.TourAppointment.Id);

            Percent18 = 0;
            Percent50 = 0;
            Percent1850 = 0;
            Coupon = 0;

            if (presentGuestsService.GetNumberOfGuests(SelectedCompletedTour.TourAppointment.Id) != 0){

                Percent18 = presentGuestsService.GetUnder18(SelectedCompletedTour.TourAppointment.Id) * 100 / presentGuestsService.GetNumberOfGuests(SelectedCompletedTour.TourAppointment.Id);
                Percent1850 = presentGuestsService.GetBetween18and50(SelectedCompletedTour.TourAppointment.Id) * 100 / presentGuestsService.GetNumberOfGuests(SelectedCompletedTour.TourAppointment.Id);
                Percent50 = presentGuestsService.GetOver50(SelectedCompletedTour.TourAppointment.Id) * 100 / presentGuestsService.GetNumberOfGuests(SelectedCompletedTour.TourAppointment.Id);
                Coupon = presentGuestsService.GetNumberOfGuestsWithCoupon(SelectedCompletedTour.TourAppointment.Id) * 100 / presentGuestsService.GetNumberOfGuests(SelectedCompletedTour.TourAppointment.Id);
            }

            CouponPercent = Coupon.ToString() + "%";

            
            dateElement.Visibility = Visibility.Visible;
            
        }
    }
}
