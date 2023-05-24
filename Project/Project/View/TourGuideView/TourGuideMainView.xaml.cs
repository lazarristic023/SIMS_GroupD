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


        private User _currentUser = new User();
        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
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
        public QuitSharedViewModel QuitShared { get; set; } = new QuitSharedViewModel();


        public TourGuideMainView(User user)
        {
            InitializeComponent();
            DataContext =  this;

            CurrentUser = user;
            

            _appointmentService = new AppointmentService();
            _appointmentService.Subscribe(this);

            _tourService = new TourService();
            _tourService.Subscribe(this);

            ImageSource = "../../Resources/Data/images.csv";

            Tours = new ObservableCollection<Tour>(_tourService.GetAllTourAppointments(CurrentUser.Id));




        }


        public void Update()
        {
            UpdateTours();
        }


        public void UpdateToursServiceChanged()
        {
            Tours.Clear();

            foreach (var tour in _tourService.GetAll(CurrentUser.Id))
            {
                Tours.Add(tour);
            }
        }

        public void UpdateTours()
        {
            Tours.Clear();

            foreach(var tour in _tourService.GetAllTourAppointments(CurrentUser.Id))
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
            usernameLabel.Content = CurrentUser.Username;
            cancelTour.IsEnabled = false;

        }


        private void addTourButton_Click(object sender, RoutedEventArgs e)
        {
            SharedViewModel = new AddSharedViewModel();
            AddNewTour addNewTour = new AddNewTour(_tourService,_appointmentService,CurrentUser,SharedViewModel);
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
                        _appointmentService.Cancel(SelectedTour.TourAppointment,CurrentUser,SelectedTour.Name);
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
            Statistic statistic = new Statistic(CurrentUser);
            statistic.Owner = this;
            statistic.Show();
        }

        private void ReviewsBtn_Click(object sender, RoutedEventArgs e)
        {
            Reviews reviews = new Reviews(CurrentUser.Id);
            reviews.Owner = this;
            reviews.Show();
        }

        private void tourrequestBtn_Click(object sender, RoutedEventArgs e)
        {
            TourRequests tourRequests = new TourRequests(CurrentUser);
            tourRequests.Owner = this;
            tourRequests.Show();
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings(CurrentUser, QuitShared);
            settings.Owner = this;
            settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            settings.Closed += new EventHandler(Settings_Closed);
            settings.Show();

        }

        private void Settings_Closed(object sender, EventArgs e)
        {
            if(QuitShared.IsQuit == true)
            {
                System.Windows.Forms.Application.Restart();
                System.Windows.Application.Current.Shutdown();
            }
            
        }
    }
}
