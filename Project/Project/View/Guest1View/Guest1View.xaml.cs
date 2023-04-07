using Project.Controller;
using Project.Model;
using Project.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    public partial class Guest1View : Window, IObserver
    {
        private Guest1Controller _controller;

        public ObservableCollection<AccommodationReservation> GuestReservations { get; set; }
        public ObservableCollection<Accommodation> Accommodations { get; set; }

        public ObservableCollection<string> Countries { get; set; }
        public ObservableCollection<string> CountryCities { get; set; }

        public string SelectedCountry { get; set; }
        public string SelectedCity { get; set; }

        public Accommodation SelectedAccommodation { get; set; }
        public Guest1View(User u)
        {
            InitializeComponent();
            DataContext = this;

            _controller = new Guest1Controller(u);
            GuestReservations = new ObservableCollection<AccommodationReservation>(_controller.GetGuestReservations());
            Accommodations = new ObservableCollection<Accommodation>(_controller.GetAllAccommodations());
            _controller.SubscribeToReservationRepository(this);

            Countries = new ObservableCollection<string>();
            CountryCities = new ObservableCollection<string>();
            FillCountriesList();
            
        }

        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            SignInView signInView = new SignInView();
            Close();
            signInView.Show();
        }

        private void FillCountriesList()
        {
            foreach (var location in _controller.GetAccommodationLocations())
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
            foreach (var location in _controller.GetAccommodationLocations())
            {
                if (location.Country == SelectedCountry)
                {
                    CountryCities.Add(location.City);
                }
            }
        }

        private void FilterAccommodations(object sender, RoutedEventArgs e)
        {

            ReInitializeAccommodations();

            if (!IsFieldEmpty(tbName.Text)) 
            { 
                FilterAccommodationsByName();           
            }

            if (!IsFieldEmpty(SelectedCountry))
            {
                FilterAccommodationsByLocation();
            }


            if (!IsFieldEmpty(tbGuestNum.Text))
            {
                if (!IsDigitsOnly(tbGuestNum.Text))
                {
                    InputErrorMessageBox("Number of guests");
                    return;
                }

                FilterAccommodationsByGuests();
            }

            if (!IsFieldEmpty(tbDaysNum.Text))
            {
                if (!IsDigitsOnly(tbDaysNum.Text))
                {
                    InputErrorMessageBox("Number of days");
                    return;
                }

                FilterAccommodationsByDays();
            }

            if (!(bool)chbHouse.IsChecked && !(bool)chbAppartment.IsChecked && !(bool)chbCottage.IsChecked)
            {
                return;
            }

            FilterAccommodationsByType();

        }

        private bool IsDigitsOnly(string str)
        {
            return str.All(c => c >= '0' && c <= '9');
        }

        private bool IsFieldEmpty(string fieldInput)
        {
            return string.IsNullOrWhiteSpace(fieldInput);
        }


        private void tbViewDetails_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AccommodationInfoView accommodationInfoView = new AccommodationInfoView(_controller, SelectedAccommodation);
            accommodationInfoView.Show();
        }

        private void UpdateGuestReservationsList()
        {
            GuestReservations.Clear();
            foreach (var reservation in _controller.GetGuestReservations())
            {
                GuestReservations.Add(reservation);
            }
        }

        public void Update()
        {
            UpdateGuestReservationsList();
        }

        private void btnMakeReservation_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAccommodation == null)
            {
                ItemNotSelectedMessageBox("Accommodation");
                return;
            }

            ReserveView reserveView = new ReserveView(_controller, SelectedAccommodation);
            reserveView.Show();
        }


        private void InputErrorMessageBox(string fieldName)
        {
            string sMessageBoxText = $"{fieldName} field must contain only digits!";
            string sCaption = $"Input error: {fieldName}";

            MessageBoxButton btnMessageBox = MessageBoxButton.OK;
            MessageBoxImage icnMessageBox = MessageBoxImage.Error;

            MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
        }

        private void ItemNotSelectedMessageBox(string item)
        {
            string sMessageBoxText = $"Choose an {item.ToLower()} first!";
            string sCaption = $"{item} not chosen";

            MessageBoxButton btnMessageBox = MessageBoxButton.OK;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;


            MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
        }

        private void FilterAccommodationsByName()
        {
            
            List<Accommodation> tempAccommodations = new List<Accommodation>(Accommodations);

            foreach (Accommodation accommodation in tempAccommodations)
            {
                if (!accommodation.Name.Contains(tbName.Text))
                {
                    Accommodations.Remove(accommodation);
                }
            }
            
        }

        private void FilterAccommodationsByLocation()
        {
            bool isCityChosen = false;
            if (!IsFieldEmpty(SelectedCity))
            {
                isCityChosen = true;
            }

            List<Accommodation> tempAccommodations = new List<Accommodation>(Accommodations);

            foreach (Accommodation accommodation in tempAccommodations)
            {
                if (accommodation.Location.Country != SelectedCountry)
                {
                    Accommodations.Remove(accommodation);
                }
                else if (isCityChosen && accommodation.Location.City != SelectedCity)
                {
                    Accommodations.Remove(accommodation);
                }
                else
                    continue;

            }
        }

        private void ReInitializeAccommodations()
        {
            Accommodations.Clear();
            foreach (Accommodation accommodation in _controller.GetAllAccommodations())
            {
                Accommodations.Add(accommodation);
            }
        }

        private void FilterAccommodationsByGuests()
        {
            List<Accommodation> tempAccommodations = new List<Accommodation>(Accommodations);

            int guests = Convert.ToInt32(tbGuestNum.Text);

            foreach (Accommodation accommodation in tempAccommodations)
            {
                if (guests > accommodation.MaxGuests)
                {
                    Accommodations.Remove(accommodation);
                }
            }
        }

        private void FilterAccommodationsByDays()
        {
            List<Accommodation> tempAccommodations = new List<Accommodation>(Accommodations);

            int days = Convert.ToInt32(tbDaysNum.Text);

            foreach (Accommodation accommodation in tempAccommodations)
            {
                if (days < accommodation.MinReservationDays)
                {
                    Accommodations.Remove(accommodation);
                }
            }
        }

        private void FilterAccommodationsByType()
        {
            List<Accommodation> temp = new List<Accommodation>(Accommodations);

            foreach (Accommodation accommodation in temp)
            {
                if (accommodation.AccommodationType == AccommodationType.HOUSE)
                {
                    if (!(bool)chbHouse.IsChecked)
                    {
                        Accommodations.Remove(accommodation);
                    }
                }
                else if (accommodation.AccommodationType == AccommodationType.APPARTMENT)
                {
                    if (!(bool)chbAppartment.IsChecked)
                    {
                        Accommodations.Remove(accommodation);
                    }
                }
                else
                {
                    if (!(bool)chbCottage.IsChecked)
                    {
                        Accommodations.Remove(accommodation);
                    }
                }
            }
        }
    }
}
