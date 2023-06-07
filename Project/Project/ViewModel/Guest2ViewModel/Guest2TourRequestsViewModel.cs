using Project.Command;
using Project.Model;
using Project.Observer;
using Project.Service;
using Project.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;

namespace Project.ViewModel.Guest2ViewModel
{
    class Guest2TourRequestsViewModel : CloseableViewModel, IObserver
    {
        private List<string> _countriesN;
        public List<string> CountriesN
        {
            get
            {
                return _countriesN;
            }
            set
            {
                _countriesN = value;
                OnPropertyChanged(nameof(CountriesN));
            }
        }

        private List<string> _citiesN;
        public List<string> CitiesN
        {
            get
            {
                return _citiesN;
            }
            set
            {
                _citiesN = value;
                OnPropertyChanged(nameof(CitiesN));
            }
        }

        private string _countryN = string.Empty;
        public string CountryN
        {
            get
            {
                return _countryN;
            }
            set
            {
                _countryN = value;
                OnPropertyChanged(nameof(CountryN));
                //CitiesN = LoadCities();
            }
        }

        private string _cityN = string.Empty;
        public string CityN
        {
            get
            {
                return _cityN;
            }
            set
            {
                _cityN = value;
                OnPropertyChanged(nameof(CityN));
            }
        }

        private List<string> _languagesN;
        public List<string> LanguagesN
        {
            get
            {
                return _languagesN;
            }
            set
            {
                _languagesN = value;
                OnPropertyChanged(nameof(LanguagesN));
            }
        }

        private string _languageN = string.Empty;
        public string LanguageN
        {
            get
            {
                return _languageN;
            }
            set
            {
                _languageN = value;
                OnPropertyChanged(nameof(LanguageN));
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        private DateTime _datePickerDates;
        public DateTime DatePickerDates
        {
            get
            {
                return _datePickerDates;
            }
            set
            {
                _datePickerDates = value;
                OnPropertyChanged(nameof(DatePickerDates));
            }
        }

        private int _guestNumber;
        public int GuestNumber
        {
            get
            {
                return _guestNumber;
            }
            set
            {
                _guestNumber = value;
                OnPropertyChanged(nameof(GuestNumber));
            }
        }

        private string _description = string.Empty;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private Location _location = new Location();
        public Location Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        


        private ObservableCollection<TourRequest> _requests;

        public ObservableCollection<TourRequest> Requests
        {
            get
            {
                return _requests;
            }

            set
            {
                _requests = value;
                OnPropertyChanged(nameof(Requests));
            }
        }

        private string[] _countries;
        public string[] Countries
        {
            get
            {
                return _countries;
            }
            set
            {
                _countries = value;
                OnPropertyChanged(nameof(Countries));
            }
        }

        private List<string> _cities;
        public List<string> Cities
        {
            get
            {
                return _cities;
            }
            set
            {
                _cities = value;
                OnPropertyChanged(nameof(Cities));
            }
        }

        private string _country = string.Empty;
        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
                OnPropertyChanged(nameof(Country));
                //Cities = LoadCities();
            }
        }

        private string _city = string.Empty;
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
                OnPropertyChanged(nameof(City));
            }
        }

        private readonly LocationService locationService;
        private readonly TourRequestService tourRequestService;
        private readonly TourService tourService;
        private readonly AppointmentService appointmentService;

        List<TourRequest> tourRequests = new List<TourRequest>();

        private User Guest;

        public Guest2TourRequestsViewModel(User user)
        {
            Guest = user;

            locationService = new LocationService();
            tourRequestService = new TourRequestService();
            tourService = new TourService();
            tourService.Subscribe(this);
            appointmentService = new AppointmentService();
            appointmentService.Subscribe(this);
            Countries = locationService.GetAllCountries();
            Cities = LoadCities();
            LanguagesN = LoadLanguages();
            CountriesN = LoadCountries();
            DatePickerDates = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            tourRequests = tourRequestService.GetAllGuestsTourRequests(3);
            Requests = new ObservableCollection<TourRequest>(tourRequestService.GetAllGuestsTourRequests(4));
        }

        private List<string> LoadLanguages()
        {
            List<string> languages = new List<string>();
            StreamReader languageSource = new StreamReader(@"../../../Resources/Data/languages.csv");
            string content = languageSource.ReadToEnd();
            string[] language = content.Split('|');
            foreach (string lang in language)
            {
                languages.Add(lang);
            }
            return languages;
        }

        private List<string> LoadCountries()
        {
            List<string> countries = new List<string>();
            StreamReader countriesSource = new StreamReader(@"../../../Resources/Data/country.csv");
            string content = countriesSource.ReadToEnd();
            string[] country = content.Split('|');
            foreach(string con in country)
            {
                countries.Add(con);
            }
            return countries;
        }

        private List<string> LoadCities()
        {
            List<string> cities = new List<string>();
            StreamReader citiesSource = new StreamReader(@"../../../Resources/Data/city.csv");
            string content = citiesSource.ReadToEnd();
            string[] city = content.Split('|');
            foreach(string cit in city)
            {
                cities.Add(cit);
            }

            return cities;
        }

        private RelayCommand makeNewRequestCommand;
        public ICommand MakeNewRequestCommand
        {
            get
            {
                if (makeNewRequestCommand == null)
                {
                    makeNewRequestCommand = new RelayCommand(param => this.MakeNewRequest(), param => this.CanMakeNewRequest());
                }
                return makeNewRequestCommand;
            }

        }

        private void MakeNewRequest()
        {
            _location = locationService.Create(City, Country);

            //TourRequest request = new TourRequest(_location.Id, Description, Language, GuestNumber, StartDate, EndDate, 4);
            tourRequestService.Create(_location.Id, Description, LanguageN, GuestNumber, StartDate, EndDate, new DateTime(), TourRequest.STATUS.ONHOLD, 4, TourRequest.TYPE.REGULAR);
            tourRequestService.GetAllGuestsTourRequests(3);
        }

        private bool CanMakeNewRequest()
        {
            return true;
        }

        //private string[] LoadCities()
        //{
        //    if(Country == string.Empty)
        //    {
        //        return locationService.GetAllCities();
        //    }
        //    else
        //    {
        //        return locationService.GetAppropriateCities(Country);
        //    }
        //}

        public void Update()
        {
            throw new NotImplementedException();
        }

        private RelayCommand openNewRequestCommand;

        public ICommand OpenNewRequestCommand
        {
            get
            {
                if(openNewRequestCommand == null)
                {
                    openNewRequestCommand = new RelayCommand(param => this.OpenNewRequest(), param => this.CanOpenNewRequest());
                }
                return openNewRequestCommand;
            }
        }

       private bool CanOpenNewRequest()
        {
            return true;
        }

        public void OpenNewRequest()
        {
            Guest2NewRequest newRequest = new Guest2NewRequest(Guest);
            newRequest.Show();
        }

        private RelayCommand openRequestsStatisticCommand;
        public ICommand OpenRequestsStatisticCommand
        {
            get
            {
                if( openRequestsStatisticCommand == null)
                {
                    openRequestsStatisticCommand = new RelayCommand(param => this.OpenRequestsStatistic(), param => this.CanOpenRequestsStatistic());
                }
                return openRequestsStatisticCommand;
            }
        }

        private bool CanOpenRequestsStatistic()
        {
            return true;
        }

        public void OpenRequestsStatistic()
        {
            Guest2RequestsStatistic requestsStatistic = new Guest2RequestsStatistic(Guest);
            requestsStatistic.Show();
        }

        private RelayCommand backButtonCommand;
        public ICommand BackButtonCommand
        {
            get
            {
                if( backButtonCommand == null)
                {
                    backButtonCommand = new RelayCommand(param => this.BackButton(), param => this.CanBackButton());
                }
                return backButtonCommand;
            }
        }

        private bool CanBackButton()
        {
            return true;
        }

        public void BackButton()
        {
            //this.close();
        }
    }
}
