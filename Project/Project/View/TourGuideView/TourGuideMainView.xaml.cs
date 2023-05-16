using Microsoft.Win32;
using Project.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.IO;
using System.Reflection.Metadata;
using Project.Model;
using System.Collections.ObjectModel;
using Project.Observer;
using Project.Service;
using Project.ViewModel.TourGuideViewModel;

namespace Project.View.TourGuideView
{
    /// <summary>
    /// Interaction logic for TourGuideMainView.xaml
    /// </summary>
    public partial class TourGuideMainView : Window, INotifyPropertyChanged, IObserver
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly TourService _tourService;
        private readonly AppointmentService _appointmentService;

        public Tour SelectedTour { get; set; }

        public ObservableCollection<Tour> Tours { get; set; }


        private User _user = new User();
        public User User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        private string _imagesource = string.Empty;
        public string ImageSource
        {
            get => _imagesource;
            set
            {
                if (value != _imagesource)
                {
                    _imagesource = value;
                    OnPropertyChanged();
                }
            }
        }

        public AddSharedViewModel SharedViewModel { get; set; } = new AddSharedViewModel();


        public TourGuideMainView(User user)
        {
            InitializeComponent();
            DataContext =  this;

            User = user;
            

            _appointmentService = new AppointmentService();
            _appointmentService.Subscribe(this);

            _tourService = new TourService();
            _tourService.Subscribe(this);

            ImageSource = "../../Resources/Data/images.csv";

            Tours = new ObservableCollection<Tour>(_tourService.GetAllTourAppointments(User.Id));


        }


        public void Update()
        {
            UpdateTours();
        }


        public void UpdateToursServiceChanged()
        {
            Tours.Clear();

            foreach (var tour in _tourService.GetAll(User.Id))
            {
                Tours.Add(tour);
            }
        }

        public void UpdateTours()
        {
            Tours.Clear();

            foreach(var tour in _tourService.GetAllTourAppointments(User.Id))
            {
                Tours.Add(tour);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            usernameLabel.Content = User.Username;
            cancelTour.IsEnabled = false;

        }


        private void addTourButton_Click(object sender, RoutedEventArgs e)
        {
            SharedViewModel = new AddSharedViewModel();
            AddNewTour addNewTour = new AddNewTour(_tourService,_appointmentService,User,SharedViewModel);
            addNewTour.Owner = this;
            addNewTour.Show();
        }


        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(SelectedTour != null)
            {
                SingleTourOverview singleTour = new SingleTourOverview(SelectedTour);
                singleTour.Owner = this;
                singleTour.Show();
            }
            
        }

        



        private void cancelTour_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {

                var timespan = SelectedTour.TourAppointment.DateAndTimeOfAppointment - DateTime.Now;
                
                if(timespan.TotalHours < 48)
                {
                    MessageBox.Show(this,"You cannot cancel this tour.\nThe tour can be canceled no later than 48 hours before the scheduled start.");
                }
                else
                {
                    if (MessageBox.Show("Are you sure you want to cancel the tour?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        //no
                    }
                    else
                    {
                        //yes
                        _appointmentService.Cancel(SelectedTour.TourAppointment);
                    }

                }

                
            }
            
            
            
        }

        private void myTourDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(SelectedTour == null)
            {
                cancelTour.IsEnabled = false;
            }
            else
            {
                cancelTour.IsEnabled = true;
            }
            
        }

        private void StatisticBtn_Click(object sender, RoutedEventArgs e)
        {
            Statistic statistic = new Statistic(User);
            statistic.Owner = this;
            statistic.Show();
        }

        private void ReviewsBtn_Click(object sender, RoutedEventArgs e)
        {
            Reviews reviews = new Reviews(User.Id);
            reviews.Owner = this;
            reviews.Show();
        }

        private void tourrequestBtn_Click(object sender, RoutedEventArgs e)
        {
            TourRequests tourRequests = new TourRequests(User);
            tourRequests.Owner = this;
            tourRequests.Show();
        }
    }
}
