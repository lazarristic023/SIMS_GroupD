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

namespace Project.View.Guest1View
{
    /// <summary>
    /// Interaction logic for MoveReservationWindow.xaml
    /// </summary>
    public partial class MoveReservationWindow : Window
    {
        public User User { get; set; }
        private AccommodationReservationService reservationService;

        public ObservableCollection<AccommodationReservation> CurrentReservations { get; set; }

        public AccommodationReservation SelectedReservation { get; set; }

        public MoveReservationWindow(User u)
        {
            InitializeComponent();
            DataContext = this;
            User = u;
            reservationService = new AccommodationReservationService();
            CurrentReservations = new ObservableCollection<AccommodationReservation>(reservationService.GetUsersCurrentReservations(User.Id));
        }

        private void btnMove_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
