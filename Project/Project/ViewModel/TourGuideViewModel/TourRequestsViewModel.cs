using Project.Command;
using Project.Model;
using Project.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.ViewModel.TourGuideViewModel
{
    public class TourRequestsViewModel:ViewModelBase
    {
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
				Cities = LoadCities();
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

		private List<string> _languages;
		public List<string> Languages
		{
			get
			{
				return _languages;
			}
			set
			{
				_languages = value;
				OnPropertyChanged(nameof(Languages));
			}
		}

		private string _language = string.Empty;
		public string Language
		{
			get
			{
				return _language;
			}
			set
			{
				_language = value;
				OnPropertyChanged(nameof(Language));
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

		private DateTime _endaDate;
		public DateTime EndDate
		{
			get
			{
				return _endaDate;
			}
			set
			{
				_endaDate = value;
				OnPropertyChanged(nameof(EndDate));
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

		private readonly LocationService locationService;

        List<TourRequest> tourRequests = new List<TourRequest>();

        public TourRequestsViewModel()
        {
			locationService = new LocationService();

			Countries = locationService.GetAllCountries();
			Cities = LoadCities();
			Languages = LoadLanguages();

			DateTime date1 = new DateTime(20 / 05 / 2022);
			DateTime date2 = new DateTime(25 / 05 / 2022);
			DateTime date3 = new DateTime(28 / 05 / 2022);
			DateTime date4 = new DateTime(31 / 05 / 2022);
			DateTime date5 = DateTime.MinValue;

			
			tourRequests.Add(new TourRequest(1, "Opis 1", "Serbian", 5, date1, date2, date5, TourRequest.STATUS.ONHOLD));
			tourRequests.Add(new TourRequest(13, "Opis 2", "Croatian", 10, date3, date4, date5, TourRequest.STATUS.ONHOLD));

			Requests = new ObservableCollection<TourRequest>(tourRequests);
			
        }

		private List<TourRequest> ApplyedFilter(List<TourRequest> torReq)
		{
            List<TourRequest> req = new List<TourRequest>();

			foreach(TourRequest t in torReq)
			{
				if(t.Language == Language)
				{
					req.Add(t);
				}
			}

			return req;
		}

		private List<string> LoadLanguages()
		{
			List<string> languages = new List<string>();
            StreamReader languageSource = new StreamReader(@"../../../Resources/Data/languages.csv");
            string content = languageSource.ReadToEnd();
            string[] language = content.Split('|');
            foreach (string element in language)
            {
				languages.Add(element);
            }

			return languages;

        }

		private string[] LoadCities()
		{
			if(Country == string.Empty)
			{
				return locationService.GetAllCities();
			}
			else
			{
				return locationService.GetAppropriateCities(Country);
			}
		}

        private RelayCommand clearFilterCommand;
        public ICommand ClearFilterCommand
        {
            get
            {
                if (clearFilterCommand == null)
                {
                    clearFilterCommand = new RelayCommand(param => this.ClearFilter(), param => this.CanClearFilter());
                }
                return clearFilterCommand;
            }
        }

		private bool CanClearFilter()
		{
			return true;
		}

		private void ClearFilter()
		{
			Country = string.Empty;
			City = string.Empty;
			Language = string.Empty;
			GuestNumber = 0;
			Requests.Clear();
            foreach (var el in tourRequests)
            {
                Requests.Add(el);
            }

        }

        private RelayCommand applyFilterCommand;
        public ICommand ApplyFilterCommand
        {
            get
            {
                if (applyFilterCommand == null)
                {
                    applyFilterCommand = new RelayCommand(param => this.ApplyFilter(), param => this.CanApplyFilter());
                }
                return applyFilterCommand;
            }
        }

		private bool CanApplyFilter()
		{
			return true;
		}

		private void ApplyFilter()
		{
			Requests.Clear();
            foreach (var el in tourRequests)
            {
                Requests.Add(el);
            }

            List<TourRequest> tr = new List<TourRequest>();
			foreach(var element in Requests)
			{
				tr.Add(element);
			}
			Requests.Clear();
			foreach(var el in ApplyedFilter(tr))
			{
				Requests.Add(el);
			}
		}
    }
}
