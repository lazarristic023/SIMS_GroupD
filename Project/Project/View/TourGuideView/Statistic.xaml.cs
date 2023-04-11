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

        private string _nameoftour;
        public string NameOfTour
        {
            get => _nameoftour;
            set
            {
                if (value != _nameoftour)
                {
                    _nameoftour = value;
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

        private string _bestnameoftour;
        public string bestNameOfTour 
        {
            get => _bestnameoftour;
            set
            {
                if(value != _bestnameoftour)
                {
                    _bestnameoftour = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _bestappointmentdate;
        public DateTime bestAppointmentDate 
        {
            get => _bestappointmentdate;
            set
            {
                if(value != _bestappointmentdate)
                {
                    _bestappointmentdate = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _bestunder18;
        public int bestUnder18
        {
            get => _bestunder18;
            set
            {
                if(value != _bestunder18)
                {
                    _bestunder18 = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _bestpercent18;
        public double bestPercent18
        {
            get => _bestpercent18;
            set
            {
                if (value != _bestpercent18)
                {
                    _bestpercent18 = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _bestover50;
        public int bestOver50
        {
            get => _bestover50;
            set
            {
                if (value != _bestover50)
                {
                    _bestover50 = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _bestpercent50;
        public double bestPercent50
        {
            get => _bestpercent50;
            set
            {
                if (value != _bestpercent50)
                {
                    _bestpercent50 = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _bestpercent1850;
        public double bestPercent1850
        {
            get => _bestpercent1850;
            set
            {
                if (value != _bestpercent1850)
                {
                    _bestpercent1850 = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _bestbetween18and50;
        public int bestBetween18and50
        {
            get => _bestbetween18and50;
            set
            {
                if (value != _bestbetween18and50)
                {
                    _bestbetween18and50 = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _bestcoupon;
        public double bestCoupon
        {
            get => _bestcoupon;
            set
            {
                if (value != _bestcoupon)
                {
                    _bestcoupon = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _bestcouponpercent;
        public string bestCouponPercent
        {
            get => _bestcouponpercent;
            set
            {
                if (value != _bestcouponpercent)
                {
                    _bestcouponpercent = value;
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

            yearComboBox.Items.Add("Overall");
            yearComboBox.Items.Add("2023");
            yearComboBox.Items.Add("2022");
            

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

        private void yearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Tour tour = GetBestTour();
            bestNameOfTour = tour.Name;
            bestAppointmentDate = tour.TourAppointment.DateAndTimeOfAppointment;
            bestUnder18 = presentGuestsService.GetUnder18(tour.TourAppointment.Id);
            bestOver50 = presentGuestsService.GetOver50(tour.TourAppointment.Id);
            bestBetween18and50 = presentGuestsService.GetBetween18and50(tour.TourAppointment.Id);

            bestPercent18 = 0;
            bestPercent50 = 0;
            bestPercent1850 = 0;
            bestCoupon = 0;

            if (presentGuestsService.GetNumberOfGuests(tour.TourAppointment.Id) != 0)
            {

                bestPercent18 = presentGuestsService.GetUnder18(tour.TourAppointment.Id) * 100 / presentGuestsService.GetNumberOfGuests(tour.TourAppointment.Id);
                bestPercent1850 = presentGuestsService.GetBetween18and50(tour.TourAppointment.Id) * 100 / presentGuestsService.GetNumberOfGuests(tour.TourAppointment.Id);
                bestPercent50 = presentGuestsService.GetOver50(tour.TourAppointment.Id) * 100 / presentGuestsService.GetNumberOfGuests(tour.TourAppointment.Id);
                bestCoupon = presentGuestsService.GetNumberOfGuestsWithCoupon(tour.TourAppointment.Id) * 100 / presentGuestsService.GetNumberOfGuests(tour.TourAppointment.Id);
            }

            bestCouponPercent = bestCoupon.ToString() + "%";


        }

        public Tour GetBestTour()
        {
            List<Tour> completed = tourService.GetCompletedTours();
            int highestNumberOfGuests = 0;
            Tour bestTour = new Tour();

            if (yearComboBox.SelectedItem.ToString() == "Overall")
            {
                foreach (Tour tour in completed)
                {
                    if (presentGuestsService.GetNumberOfGuests(tour.TourAppointment.Id) > highestNumberOfGuests)
                    {
                        highestNumberOfGuests = presentGuestsService.GetNumberOfGuests(tour.TourAppointment.Id);
                        bestTour = tour;
                    }
                }
            }
            else
            {
                foreach (Tour tour in completed)
                {
                    if (presentGuestsService.GetNumberOfGuests(tour.TourAppointment.Id) > highestNumberOfGuests &&
                        tour.TourAppointment.DateAndTimeOfAppointment.Year.ToString() == yearComboBox.SelectedItem.ToString())
                    {
                        highestNumberOfGuests = presentGuestsService.GetNumberOfGuests(tour.TourAppointment.Id);
                        bestTour = tour;
                    }
                }
            }
            return bestTour;
        }
    }
}
