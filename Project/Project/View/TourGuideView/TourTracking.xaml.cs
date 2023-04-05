using Project.Controller;
using Project.Model;
using Project.Observer;
using Project.Repository;
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
    /// Interaction logic for TourTracking.xaml
    /// </summary>
    public partial class TourTracking : Window, IObserver, INotifyPropertyChanged
    {
        private readonly TourGuideController _tourGuideController;
        private readonly TourPointController _tourPointController;
        private readonly TourPointsListController _tourPointsListController;
        private readonly AppointmentController _appointmentController;

        private readonly TourReservationRepository reservationRepository;
        private readonly UserRepository userRepository;
        private readonly PresentGuestsRepository presentGuestsRepository;

        public int tourId { get; set; }
        public int appointmentId { get; set; }
        public DateTime date { get; set; }

        public ObservableCollection<TourPoint> tourPoints { get; set; }


        private int numberOfNextClicks = 0;

        public TourPoint selectedPoint { get; set; }



        private List<Rezervacija> _rezervacije;

        public event PropertyChangedEventHandler? PropertyChanged;

        public List<Rezervacija> Rezervacije
        {
            get => _rezervacije;
            set
            {
                if (value != _rezervacije)
                {
                    _rezervacije = value;
                    OnPropertyChanged();
                }
            }
        }




        public ObservableCollection<User> Presents { get; set; }

        public TourTracking(int sendedId, int appointmentid)
        {
            InitializeComponent();
            DataContext = this;

            _tourGuideController = new TourGuideController();
            _tourPointController = new TourPointController();
            _tourPointsListController = new TourPointsListController();
            _appointmentController = new AppointmentController();

            reservationRepository = new TourReservationRepository();
            userRepository = new UserRepository();
            presentGuestsRepository = new PresentGuestsRepository();
            presentGuestsRepository.Subscribe(this);

            

            tourId = sendedId;
            appointmentId = appointmentid;

            Presents = new ObservableCollection<User>(presentGuestsRepository.GetUserByAppointmentId(appointmentId));




            tourPoints = new ObservableCollection<TourPoint>(_tourPointsListController.GetPointsByTourId(tourId));

            pointsListBox.SelectedIndex = 0;


            //Rezervacija rezervacija1 = new Rezervacija(0, "Pera", false, 0);
            //Rezervacija rezervacija2 = new Rezervacija(1, "Zika", true, 0);
            //Rezervacija rezervacija3 = new Rezervacija(2, "Mika", false, 0);
            //Rezervacija rezervacija4 = new Rezervacija(3, "Marko", true, 0);
            //List<Rezervacija> rez = new List<Rezervacija>();
            //rez.Add(rezervacija1);
            //rez.Add(rezervacija2);
            //rez.Add(rezervacija3);
            //rez.Add(rezervacija4);
            //Rezervacije = new List<Rezervacija>(rez);

        }

        public List<User> GetApproprietReservations()
        {
            List<TourReservation> tourReservations = reservationRepository.GetAllTourReservations();
            List<User> approprietReservations = new List<User>();

            foreach (TourReservation reservation in tourReservations)
            {
                if (reservation.TourId == tourId)
                {
                    approprietReservations.Add(userRepository.GetById(reservation.GuestId));
                }
            }

            return approprietReservations;
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tourName.Content = _tourGuideController.GetById(tourId).Name;
            selectedPoint = (TourPoint)pointsListBox.SelectedItem;
            ChangeActivity(selectedPoint);


            
        }


        private void endTour_Click(object sender, RoutedEventArgs e)
        {
            selectedPoint.Action = false;
            EndTheAppointment();
        }

        private void EndTheAppointment()
        {
           
            presentGuestsRepository.ClearPresents();
            MessageBox.Show("The tour is over");
            Close();
        }

        private void nextPoint_Click(object sender, RoutedEventArgs e)
        {
            ChangeActivity(selectedPoint);

            pointsListBox.SelectedIndex++;
            numberOfNextClicks++;
            int lastIndex = pointsListBox.Items.Count - 1;

            if(numberOfNextClicks > lastIndex)
            {
                EndTheAppointment();
            }
            else
            {
                selectedPoint = (TourPoint)pointsListBox.SelectedItem;
                ChangeActivity(selectedPoint);
            }


       
        }

        public void ChangeActivity(TourPoint point)
        {
            TourPoint tourPoint = _tourPointController.GetById(point.Id);

            if(tourPoint.Action == true)
            {
                _tourPointController.UpdateAction(point.Id, false);
            }
            else
            {
                _tourPointController.UpdateAction(point.Id, true);
            }


        }
        


        public void Update()
        {
            UpdatePoints();
            UpdatePresents();
        }

        public void UpdatePoints()
        {
            tourPoints.Clear();

            foreach (var point in _tourPointsListController.GetPointsByTourId(tourId))
            {
                tourPoints.Add(point);
            }
        }

        public void UpdatePresents()
        {

            Presents.Clear();




            foreach (var present in presentGuestsRepository.GetUserByAppointmentId(appointmentId))
            {
                Presents.Add(present);
            }
        }

        private void AddGuests_Click(object sender, RoutedEventArgs e)
        {
            AddPresentGuests addPresentGuests = new AddPresentGuests(tourId, selectedPoint.Id, appointmentId, presentGuestsRepository);
            addPresentGuests.Show();

        }
    }
}
