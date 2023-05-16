using Project.Controller;
using Project.Model;
using Project.Observer;
using Project.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace Project.View
{
    /// <summary>
    /// Interaction logic for ProfileGuest2.xaml
    /// </summary>
    public partial class ProfileGuest2 : Window, IObserver
    {
        private Guest2Controller controller;
        private User user;
        public ObservableCollection<Coupon> Coupons { get; set; }
        public ObservableCollection<TourReview> GuestReviews { get; set; }
        public ObservableCollection<TourReservation> TourReservations { get; set; }
        public ObservableCollection<Appointment> TourReservationsForReview { get; set; }
        public ObservableCollection<Tour> Tours { get; set; }
        public ObservableCollection<Tour> FilteredTours { get; set; }
        public ObservableCollection<string> Countries { get; set; }
        public ObservableCollection<string> CountryCities { get; set; }
        public ObservableCollection<string> Languages { get; set; }
        public string SelectedCountry { get; set; }
        public string SelectedCity { get; set; }
        public string SelectedLanguage { get; set; }
        public Tour SelectedTour { get; set; }
        public Appointment SelectedAppointment { get; set; }
        private readonly CouponService couponService;
        private readonly TourService tourService;
        private readonly AppointmentService appointmentService;
        private readonly TourReviewService tourReviewService;

        public ProfileGuest2(User u)
        {
            InitializeComponent();
            DataContext = this;
            //controller = new Guest2Controller(u);
            ////couponService = new CouponService(u);
            //tourReviewService = new TourReviewService();
            //TourReservations = new ObservableCollection<TourReservation>(controller.GetTourReservations());
            //TourReservationsForReview = new ObservableCollection<Appointment>(controller.GetAppointmentsForReview());
            //Tours = new ObservableCollection<Tour>(controller.GetTours());
            ////Coupons = new ObservableCollection<Coupon>(couponService.GetGuest2Coupons());
            //GuestReviews = new ObservableCollection<TourReview>();
            //FilteredTours = new ObservableCollection<Tour>(Tours);
            //controller.SubscribeToReservationRepo(this);
            //Countries = new ObservableCollection<string>();
            //CountryCities = new ObservableCollection<string>();
            //Languages = new ObservableCollection<string>();
            //tourService = new TourService();
            //tourService.Subscribe(this);

            //appointmentService = new AppointmentService();
            //appointmentService.Subscribe(this);

            //FilteredTours = new ObservableCollection<Tour>(tourService.GetAllTourAppointments());
            //Tours = new ObservableCollection<Tour>(tourService.GetAllTourAppointments());
        }

        private ObservableCollection<Review> review;

        public ProfileGuest2()
        {
            InitializeComponent();
            review = new ObservableCollection<Review>();
            myDataGrid.ItemsSource = review;
            AddRows();
        }

        private void AddRows()
        {
            review.Add(new Review { Name = "Obilazak Subotice", Location = "Srbija, Subotica", Duration = 2, Guide = "Guide 1" });
            review.Add(new Review { Name = "Obilazak Beograda", Location = "Srbija, Beograd", Duration = 4, Guide = "Guide 2" });
            review.Add(new Review { Name = "Obilazak Novog Sada", Location = "Srbija, Novi Sad", Duration = 2, Guide = "Guide 3" });
            review.Add(new Review { Name = "Obilazak Zrenjanina", Location = "Srbija, Zrenjanin", Duration = 2, Guide = "Guide 1" });

        }

        private void btSignOut_Click(object sender, RoutedEventArgs e)
        {
            SignInView signInView = new SignInView();
            Close();
            signInView.Show();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        private void Button_Click_AvailableTours(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_Vouchers(object sender, RoutedEventArgs e)
        {
            VoucherView vouchers = new VoucherView();
            vouchers.Top = this.Top;
            vouchers.Left = this.Left;
            this.Close();
            vouchers.Show();
        }

        private void Button_Click_Tour_History(object sender, RoutedEventArgs e)
        {
            TourHistoryView tourHistory = new TourHistoryView();
            tourHistory.Top = this.Top;
            tourHistory.Left = this.Left;
            this.Close();
            tourHistory.Show();
        }
    }
}
