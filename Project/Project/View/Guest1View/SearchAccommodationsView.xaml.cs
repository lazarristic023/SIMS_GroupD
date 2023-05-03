using Project.Command.Guest1Commands.WindowLinkCommands;
using Project.Model;
using Project.Service;
using Project.ViewModel;
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
using System.Xml.Linq;

namespace Project.View.Guest1View
{
    /// <summary>
    /// Interaction logic for SearchAccommodationsView.xaml
    /// </summary>
    public partial class SearchAccommodationsView : Window
    {
        private readonly AccommodationService _accommodationService;
        public User User { get; private set; }

        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public ObservableCollection<string> Countries { get; set; }
        public ObservableCollection<string> CountryCities { get; set; }

        public string SelectedCountry { get; set; }
        public string SelectedCity { get; set; }

        public Accommodation SelectedAccommodation { get; set; }

        private ViewModelBase viewModelBase;

        public ICommand ProfileLinkCommand { get; }
        public ICommand YourReservationsLinkCommand { get; }
        public ICommand MoveReservationLinkCommand { get; }
        public ICommand SearchAccommodationsLinkCommand { get; }


        public SearchAccommodationsView(User user)
        {
            InitializeComponent();
            DataContext = this;
            User = user;
            viewModelBase = new ViewModelBase();
            viewModelBase.User = User;
            viewModelBase.Window = this;

            ProfileLinkCommand = new ProfileLinkCommand(viewModelBase);
            YourReservationsLinkCommand = new YourReservationsLinkCommand(viewModelBase);
            MoveReservationLinkCommand = new MoveReservationLinkCommand(viewModelBase);
            SearchAccommodationsLinkCommand = new SearchAccommodationsLinkCommand(viewModelBase);

            _accommodationService = new AccommodationService();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAllAccommodations());
            Countries = new ObservableCollection<string>();
            CountryCities = new ObservableCollection<string>();
            FillCountriesList();

        }


        private void FillCountriesList()
        {
            foreach (var location in _accommodationService.GetAccommodationLocationsList())
            {
                if (!Countries.Contains(location.Country))
                {
                    Countries.Add(location.Country);
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
            foreach (Accommodation accommodation in _accommodationService.GetAllAccommodations())
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

        private void btnMakeReservation_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedAccommodation == null)
            {
                MessageBox.Show("Select an accommodation first!", "Choose accommodation", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            ReserveAccommodationWindow reserveAccommodationWindow = new ReserveAccommodationWindow(SelectedAccommodation, User);
            reserveAccommodationWindow.Show();
        }

        private void DataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (SelectedAccommodation == null)
            {
                return;
            }
            if (e.Key == Key.Return)
            {
                AccommodationInfoWindow accommodationInfoWindow = new AccommodationInfoWindow(SelectedAccommodation, User);
                accommodationInfoWindow.Show();
            }
        }
    }
}
