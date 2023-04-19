using Project.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Project.Model;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Project.Observer;

namespace Project.View.Guest1View
{
    /// <summary>
    /// Interaction logic for YourReservationsWindow.xaml
    /// </summary>
    public partial class YourReservationsWindow : Window, IObserver
    {

        private readonly AccommodationReservationService _reservationService;

        private OwnerNotificationService _ownerNotificationService;
        public ObservableCollection<AccommodationReservation> CurrentReservations { get; set; }
        public ObservableCollection<AccommodationReservation> FormerReservations { get; set; }

        public User User { get; private set; }

        public AccommodationReservation SelectedReservation { get; set; }


        public YourReservationsWindow(User user)
        {
            InitializeComponent();
            DataContext = this;
            User = user;

            _reservationService = new AccommodationReservationService();
            _ownerNotificationService = new OwnerNotificationService();
            _reservationService.SubscribeToReservationRepository(this);

            CurrentReservations = new ObservableCollection<AccommodationReservation>(_reservationService.GetGuestsCurrentReservations(User.Id));
            FormerReservations = new ObservableCollection<AccommodationReservation>(_reservationService.GetGuestsFormerReservations(User.Id));

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckCancellationPeriod())
            {
                return;
            }
            string msg = $"Guest {User.Username} has cancelled reservation: Accommodation name: {SelectedReservation.Accommodation.Name}, Start date: {SelectedReservation.StartDate}, End date: {SelectedReservation.EndDate} ";
            NotifyOwner(msg);

            _reservationService.Remove(SelectedReservation);

            MessageBox.Show("Reservation successfully cancelled!");
        }

        private bool CheckCancellationPeriod()
        {
            if (SelectedReservation == null)
            {
                ItemNotSelectedMessageBox("Reservation");
                return false;
            }

            if (DateTime.Now.AddDays((double)SelectedReservation.Accommodation.CancellationPeriod) > SelectedReservation.StartDate)
            {
                MessageBox.Show("You can not cancel this reservation, cancellation period has passed!");
                return false;
            }

            return true;
        }

        private void ItemNotSelectedMessageBox(string item)
        {
            string sMessageBoxText = $"Choose an {item.ToLower()} first!";
            string sCaption = $"{item} not chosen";
            MessageBoxButton btnMessageBox = MessageBoxButton.OK;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
        }

        public void NotifyOwner(string msg)
        {
            _ownerNotificationService.Create(User.Id, SelectedReservation.Accommodation.OwnerId, msg);
        }

        public void Update()
        {
            CurrentReservations.Clear();
            foreach (var reservation in _reservationService.GetGuestsCurrentReservations(User.Id))
            {
                CurrentReservations.Add(reservation);
            }
        }

        private void btnRate_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckRateConditions())
            {
                return;
            }

            RateOwnerForm rateOwnerForm = new RateOwnerForm(User, SelectedReservation);
            rateOwnerForm.Show();

        }

        private bool CheckRateConditions()
        {
            if (SelectedReservation == null)
            {
                ItemNotSelectedMessageBox("Reservation");
                return false;
            }

            if (SelectedReservation.EndDate >= DateTime.Now.Date)
            {
                MessageBox.Show("You will be able to rate owner and accommodation when reservation finishes.");
                return false;
            }

            if (SelectedReservation.EndDate.AddDays(5).Date < DateTime.Now.Date)
            {
                MessageBox.Show($"Rate period has passed - {SelectedReservation.EndDate.AddDays(5).Date} was final date for rating!");
                return false;
            }

            if (SelectedReservation.GuestReview != null)
            {
                MessageBox.Show("You have already rated this reservation!");
                return false;
            }

            return true;
        }
    }
}
