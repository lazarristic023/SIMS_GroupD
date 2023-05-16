using Project.Command.Guest2Commands;
using Project.Command.Guest2Commands.LinkCommands;
using Project.Controller;
using Project.Model;
using Project.Service;
using Project.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project.ViewModel
{
    public class Guest2ViewModel : ViewModelBase
    {
        private string _selectedCountry;
        public string SelectedCountry
        {
            get { return _selectedCountry; }
            set 
            { 
                _selectedCountry = value; 
                OnPropertyChanged(nameof(_selectedCountry));
            }
        }

        public string _selectedCity;
        public string SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                _selectedCity = value;
                OnPropertyChanged(nameof(_selectedCity));
            }
        }


        public string _selectedLanguage;
        public string SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                _selectedLanguage = value;
                OnPropertyChanged(nameof(_selectedLanguage));
            }
        }

        private string _guests;
        public string Guests
        {
            get { return _guests; }
            set
            {
                if (value == "0")
                {
                    return;
                }
                if (!IsDigitsOnly(value)) { return; }
                _guests = value;
                OnPropertyChanged(nameof(_guests));
            }
        }

        private string _hours;
        public string Hours
        {
            get { return _hours; }
            set
            {
                if(value == "0") { return; }
                if(!IsDigitsOnly(value)) { return; }
                _hours = value;
                OnPropertyChanged(nameof(_hours));
            }
        }


        private Guest2Controller controller;
        private User user;
        public ObservableCollection<Coupon> Coupons { get; set; }
        public ObservableCollection<TourReservation> TourReservations { get; set; }
        public ObservableCollection<Appointment> TourReservationsForReview { get; set; }
        public ObservableCollection<Tour> Tours { get; set; }
        public ObservableCollection<Tour> FilteredTours { get; set; }
        public ObservableCollection<string> Countries { get; set; }
        public ObservableCollection<string> CountryCities { get; set; }
        public ObservableCollection<string> Languages { get; set; }
       
        public ICommand Guest2LinkCommand { get; set; }
        public ICommand Guest2Command { get; set; }

        public Tour SelectedTour { get; set; }
        public Appointment SelectedAppointment { get; set; }
        private readonly CouponService couponService;
        private readonly TourService tourService;
        private readonly AppointmentService appointmentService;
        private readonly TourReviewService tourReviewService;


        public Guest2ViewModel(User user, Window window)
        {
            User = user;
            Window = window;

            Guest2LinkCommand = new Guest2LinkCommand(this);
            Guest2Command = new Guest2Command(this);

            tourReviewService = new TourReviewService();
            TourReservations = new ObservableCollection<TourReservation>(controller.GetTourReservations());
            TourReservationsForReview = new ObservableCollection<Appointment>(controller.GetAppointmentsForReview());
            Tours = new ObservableCollection<Tour>(controller.GetTours());
            Coupons = new ObservableCollection<Coupon>(couponService.GetGuest2Coupons());
            FilteredTours = new ObservableCollection<Tour>(Tours);
            Countries = new ObservableCollection<string>();
            CountryCities = new ObservableCollection<string>();
            Languages = new ObservableCollection<string>();
            tourService = new TourService();

            appointmentService = new AppointmentService();

            FilteredTours = new ObservableCollection<Tour>(tourService.GetAllTourAppointments());
            Tours = new ObservableCollection<Tour>(tourService.GetAllTourAppointments());
            FillCountriesList();
            FillLanguagesList();
        }

        private void btSignOut_Click(object sender, RoutedEventArgs e)
        {
            SignInView signInView = new SignInView();
            //Close();
            signInView.Show();
        }



        //private void cbCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    CountryCities.Clear();
        //    foreach (var location in controller.GetTourLocations())
        //    {
        //        if (location.Country == SelectedCountry)
        //        {
        //            CountryCities.Add(location.City);
        //        }
        //    }
        //}

        //private void cbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    Languages.Clear();
        //    foreach (var language in controller.GetTourLanguages())
        //    {
        //        if (language.ToString() == SelectedLanguage)
        //        {
        //            Languages.Add(language.ToString());
        //        }
        //    }
        //}

        public void SelectedCountryChanged()
        {
            CountryCities.Clear();
            foreach(var location in controller.GetTourLocations())
            {
                if (location.Country == SelectedCountry)
                {
                    CountryCities.Add(location.City);
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            List<Tour> temp = new List<Tour>();
            List<Tour> tempFiltered = new List<Tour>();
            bool hasEntered = false;

            temp.AddRange(Tours);

            // Location comboboxes

            if (!string.IsNullOrEmpty(SelectedCountry))
            {
                bool isCityChosen = false;
                hasEntered = true;
                if (!string.IsNullOrEmpty(SelectedCity))
                {
                    isCityChosen = true;
                }

                foreach (Tour tour in temp)
                {
                    if (tour.Location.Country == SelectedCountry)
                    {
                        if (isCityChosen)
                        {
                            if (tour.Location.City == SelectedCity)
                            {
                                tempFiltered.Add(tour);

                            }

                            continue;
                        }
                        tempFiltered.Add(tour);
                    }
                }

            }

            if (hasEntered)
            {
                hasEntered = false;
                temp.Clear();
                temp.AddRange(tempFiltered);
                tempFiltered.Clear();
            }

            // Number of guests

            if (!IsFieldEmpty(Guests))
            {
                if (!IsFieldEmpty(Guests.ToString()))
                {
                    string sMessageBoxText = $"Number of guests field must contain only digits!";
                    string sCaption = "Input error - Number of guests";

                    MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                    MessageBoxImage icnMessageBox = MessageBoxImage.Error;

                    MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                    return;
                }

                hasEntered = true;
                int guestNum = Convert.ToInt32(Guests.ToString());

                foreach (Tour tour in temp)
                {
                    if (guestNum >= tour.MaxGuests)
                    {
                        tempFiltered.Add(tour);
                    }
                }

            }

            if (hasEntered)
            {
                hasEntered = false;
                temp.Clear();
                temp.AddRange(tempFiltered);
                tempFiltered.Clear();
            }


            // Duration of tour

            if (!IsFieldEmpty(Hours))
            {
                if (!IsDigitsOnly(Hours.ToString()))
                {
                    string sMessageBoxText = $"Duration of tour field must contain only digits!";
                    string sCaption = "Input error - Number of days";

                    MessageBoxButton btnMessageBox = MessageBoxButton.OK;
                    MessageBoxImage icnMessageBox = MessageBoxImage.Error;

                    MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
                    return;
                }

                hasEntered = true;
                int durationInHours = Convert.ToInt32(Hours.ToString());

                foreach (Tour tour in temp)
                {
                    if (durationInHours >= tour.Duration)
                    {
                        tempFiltered.Add(tour);
                    }
                }

            }

            if (hasEntered)
            {
                hasEntered = false;
                temp.Clear();
                temp.AddRange(tempFiltered);
                tempFiltered.Clear();
            }

            FilteredTours.Clear();
            foreach (Tour t in temp)
            {
                FilteredTours.Add(t);
            }


        }

        private bool IsFieldEmpty(string fieldInput)
        {
            return string.IsNullOrWhiteSpace(fieldInput);
        }

        private void FillCountriesList()
        {
            foreach (var location in controller.GetTourLocations())
            {
                if (!Countries.Contains(location.Country))
                {
                    Countries.Add(location.Country);
                }
            }
        }

        private void FillLanguagesList()
        {
            foreach (var language in controller.GetTourLanguages())
            {
                if (!Languages.Contains(language.ToString()))
                {
                    Languages.Add(language.ToString());
                }
            }
        }

        private bool IsDigitsOnly(string str)
        {
            return str.All(c => c >= '0' && c <= '9');
        }
    }
}
