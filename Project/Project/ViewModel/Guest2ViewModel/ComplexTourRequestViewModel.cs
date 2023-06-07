using Project.Command;
using Project.Model;
using Project.Observer;
using Project.Service;
using Project.View;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project.ViewModel.Guest2ViewModel
{
    public class ComplexTourRequestViewModel : CloseableViewModel, IObserver
    {
        private ObservableCollection<ComplexTourRequestModel> _complexRequests;

        public ObservableCollection<ComplexTourRequestModel> ComplexRequests
        {
            get
            {
                return _complexRequests;
            }

            set
            {
                _complexRequests = value;
                OnPropertyChanged(nameof(ComplexRequests));
            }
        }

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

        private string[] _cities;
        public string[] Cities
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

        private ObservableCollection<string> _anotherTours = new ObservableCollection<string>();
        public ObservableCollection<string> AnotherTours
        {
            get
            {
                return _anotherTours;
            }
            set
            {
                _anotherTours = value;
                OnPropertyChanged(nameof(AnotherTours));
            }
        }

        private readonly LocationService locationService;
        private readonly TourRequestService tourRequestService;
        private readonly AppointmentService appointmentService;



        public ComplexTourRequestViewModel()
        {
            ComplexRequests = new ObservableCollection<ComplexTourRequestModel>();
            PopulateDataGrid();

            locationService = new LocationService();
            tourRequestService = new TourRequestService();
            appointmentService = new AppointmentService();
            appointmentService.Subscribe(this);
            //Countries = locationService.GetAllCountries();
            LanguagesN = LoadLanguages();
            CountriesN= LoadCountries();

            DatePickerDates = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        }

        private List<string> LoadCountries()
        {
            List<string> countries = new List<string>();
            StreamReader countrySource = new StreamReader(@"../../../Resources/Data/country.csv");
            string content = countrySource.ReadToEnd();
            string[] country = content.Split('|');
            foreach(string con in country)
            {
                countries.Add(con);
            }
            return countries;
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

        

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void PopulateDataGrid()
        {
            ComplexRequests = new ObservableCollection<ComplexTourRequestModel>();
            ComplexRequests.Add(new ComplexTourRequestModel { Name = "Obilazak Novog Sada", GuestId = 3, Language = "Srpski", Duration = 2 });
            ComplexRequests.Add(new ComplexTourRequestModel { Name = "Longdon Tour", GuestId = 3, Language = "English", Duration = 3 });
            ComplexRequests.Add(new ComplexTourRequestModel { Name = "Obilazak Subotice", GuestId = 3, Language = "Srpski", Duration = 3 });

        }

        private RelayCommand addTourCommand;
        public ICommand AddTourCommand
        {
            get
            {
                if (addTourCommand == null)
                {
                    addTourCommand = new RelayCommand(param => this.AddTour(), param => this.CanAddTour());
                }
                return addTourCommand;
            }
        }

        private bool CanAddTour()
        {
            return true;
        }

        public void AddTour()
        {
            if(CountryN != string.Empty)
            {
                string Tour = CountryN.ToString() +"  |  " + StartDate.ToString() + "  |  " + Description;
                AnotherTours.Add(Tour);
                
            }
            Description = "";
            CountryN = string.Empty;
            StartDate = DateTime.MinValue;
            EndDate = DateTime.MinValue;
            GuestNumber = 0;
            LanguageN = string.Empty;


        }

        private RelayCommand makeComplexRequestCommand;
        public ICommand MakeComplexRequestCommand
        {
            get
            {
                if (makeComplexRequestCommand == null)
                {
                    makeComplexRequestCommand = new RelayCommand(param => this.AddComplexTour(), param => this.CanAddComplexTour());
                }
                return makeComplexRequestCommand;
            }
        }

        public bool CanAddComplexTour()
        {
            return true;
        }

        public void AddComplexTour()
        {
            AnotherTours.Clear();
            ComplexRequests.Add(new ComplexTourRequestModel { Name = "Obilazak", GuestId = 3, Language = "Arabic", Duration = 3 });
        }

        private RelayCommand viewComplexDetailsCommand;

        public ICommand ViewComplexDetailsCommand
        {
            get
            {
                if(viewComplexDetailsCommand == null)
                {
                    viewComplexDetailsCommand = new RelayCommand(param => this.ViewComplexDetails(), param => this.CanViewComplexDetails());
                }
                return viewComplexDetailsCommand;
            }
        }

        public bool CanViewComplexDetails()
        {
            return true;
        }

        public void ViewComplexDetails()
        {
            ComplexTourDetails2 tourDetails = new ComplexTourDetails2();
            tourDetails.Show();
        }
    }
}
