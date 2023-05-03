using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModel.Guest1ViewModel
{
    public class SearchAccommodationsViewModel :ViewModelBase
    {
		private int _name;
		public int Name
		{
			get 
			{ 
				return _name;
			}
			set 
			{ 
				_name = value;
				OnPropertyChanged(nameof(_name));
			}
		}

        private string _country;
        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
                OnPropertyChanged(nameof(_country));
            }
        }

		private string _city;

		public string City
		{
			get { return _city; }
			set 
			{ 
				_city = value;
				OnPropertyChanged(nameof(_city));
			}
		}

        private int _guests;
        public int Guests
        {
            get
            {
                return _guests;
            }
            set
            {
                _guests = value;
                OnPropertyChanged(nameof(_guests));
            }
        }

        private int _days;
        public int Days
        {
            get
            {
                return _days;
            }
            set
            {
                _days = value;
                OnPropertyChanged(nameof(_days));
            }
        }

        private AccommodationType _type;
        public AccommodationType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                OnPropertyChanged(nameof(_type));
            }
        }


    }
}
