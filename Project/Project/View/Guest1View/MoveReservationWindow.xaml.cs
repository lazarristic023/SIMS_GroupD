using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Project.Model;
using Project.Service;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Project.Repository;
using System.Collections.ObjectModel;
using Project.Observer;

namespace Project.View.Guest1View
{
    /// <summary>
    /// Interaction logic for MoveReservationWindow.xaml
    /// </summary>
    public partial class MoveReservationWindow : Window, IObserver
    {
        public User User { get; set; }
        private AccommodationReservationService _reservationService;

        private MoveRequestService _requestService;

        public ObservableCollection<AccommodationReservation> CurrentReservations { get; set; }

        public ObservableCollection<MoveRequest> PendingRequests { get; set; }
        public ObservableCollection<MoveRequest> AcceptedRequests { get; set; }
        public ObservableCollection<MoveRequest> DeclinedRequests { get; set; }

        public AccommodationReservation SelectedReservation { get; set; }

        public MoveReservationWindow(User u)
        {
            InitializeComponent();
            DataContext = this;
            User = u;

            _reservationService = new AccommodationReservationService();
            _requestService = new MoveRequestService();
            _requestService.SubscribeToRepository(this);
            CurrentReservations = new ObservableCollection<AccommodationReservation>(_reservationService.GetGuestsCurrentReservations(User.Id));
            PendingRequests = new ObservableCollection<MoveRequest>(_requestService.GetGuestsPendingRequests(User.Id));
            AcceptedRequests = new ObservableCollection<MoveRequest>(_requestService.GetGuestsAcceptedRequests(User.Id));
            DeclinedRequests = new ObservableCollection<MoveRequest>(_requestService.GetGuestsDeclinedRequests(User.Id));
        }

        private void btnMove_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation == null)
            {
                MessageBox.Show("Choose a reservation first!", "Reservation not chosen", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MakeMoveRequestView makeMoveRequestView = new(SelectedReservation, User, _requestService);
            makeMoveRequestView.Show();

        }

        private void UpdatePendingRequests()
        {
            PendingRequests.Clear();
            foreach (var request in _requestService.GetGuestsPendingRequests(User.Id))
            {
                PendingRequests.Add(request);
            }
        }

        private void UpdateAcceptedRequests()
        {
            AcceptedRequests.Clear();
            foreach (var request in _requestService.GetGuestsAcceptedRequests(User.Id))
            {
                AcceptedRequests.Add(request);
            }
        }

        private void UpdateDeclinedRequests()
        {
            DeclinedRequests.Clear();
            foreach (var request in _requestService.GetGuestsDeclinedRequests(User.Id))
            {
                DeclinedRequests.Add(request);
            }
        }

        public void Update()
        {
            UpdatePendingRequests();
            UpdateAcceptedRequests();
            UpdateDeclinedRequests();
        }

        private void tbYourReservations_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            YourReservationsWindow yourReservationsWindow = new YourReservationsWindow(User);
            yourReservationsWindow.Show();
            Close();
        }
    }
}
