using Project.Controller;
using Project.Model;
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
    /// Interaction logic for Guest1View.xaml
    /// </summary>
    public partial class Guest1View : Window
    {
        private Guest1Controller controller;
        private User user;
        public ObservableCollection<AccommodationReservation> Reservations { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get; set; }

        public ObservableCollection<string> Countries { get; set; }
        public ObservableCollection<string> CountryCities { get; set; }

        public string SelectedCountry { get; set; }
        public string SelectedCity { get; set; }
        public Guest1View(User u)
        {
            InitializeComponent();
            DataContext = this;
            controller = new Guest1Controller(u);
            Reservations = new ObservableCollection<AccommodationReservation>(controller.GetAccommodationReservations());
            Accommodations = new ObservableCollection<Accommodation>(controller.GetAccommodations());
            Countries = new ObservableCollection<string>();
            CountryCities = new ObservableCollection<string>();
            FillCountriesList();

        }

        private void btSignOut_Click(object sender, RoutedEventArgs e)
        {
            SignInView signInView = new SignInView();
            Close();
            signInView.Show();
        }

        private void FillCountriesList()
        {
            foreach (var location in controller.GetAccommodationLocations())
            {
                if (!Countries.Contains(location.Country))
                {
                    Countries.Add(location.Country);
                }
            }
        }

        private void cbCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CountryCities.Clear();
            foreach (var location in controller.GetAccommodationLocations())
            {
                if (location.Country == SelectedCountry)
                {
                    CountryCities.Add(location.City);
                }
            }
        }
    }
}
