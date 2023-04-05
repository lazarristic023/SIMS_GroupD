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

namespace Project.View.TourGuideView
{
    /// <summary>
    /// Interaction logic for TourGuideMainView.xaml
    /// </summary>
    public partial class TourGuideMainView : Window, INotifyPropertyChanged, IObserver
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private readonly TourGuideController _tourGuideController;
        private readonly TourAppointmentsController _tourAppointmentsController;
        private readonly ImageController _imageController;
        private readonly TourPointController _tourPointController;
        private readonly TourPointsListController _tourPointsListController;
        private readonly LocationController _locationController;
        private readonly AppointmentController _appointmentController;

        public Tour SelectedTour { get; set; }

        


        public ObservableCollection<Tour> Tours { get; set; }
        public ObservableCollection<TourPointsList> Points { get; set; }

        User User { get; set; }

        

        private string _imagesource;
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

        public TourGuideMainView(User user)
        {
            InitializeComponent();
            DataContext =  this;

            User = user;
             
            _tourGuideController = new TourGuideController();
            _tourGuideController.Subscribe(this);

            _tourAppointmentsController = new TourAppointmentsController();
            _tourAppointmentsController.Subscribe(this);

            _imageController = new ImageController();
            _imageController.Subscribe(this);

            _tourPointController = new TourPointController();
            _tourPointController.Subscribe(this);

            _tourPointsListController = new TourPointsListController();
            _tourPointsListController.Subscribe(this);

            _locationController = new LocationController();
            _locationController.Subscribe(this);

            _appointmentController = new AppointmentController();
            _appointmentController.Subscribe(this);

            ImageSource = "../../Resources/Data/images.csv";


            Tours = new ObservableCollection<Tour>(_tourGuideController.GetAllTours());

        }


        public void Update()
        {
            UpdateTours();
        }

        public void UpdateTours()
        {
            Tours.Clear();

            foreach (var tour in _tourGuideController.GetAllTours())
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

        }


        private void addTourButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewTour addNewTour = new AddNewTour(_tourGuideController, _imageController,_tourPointController,_tourPointsListController,_locationController, _appointmentController);
            addNewTour.Show();
        }


        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(SelectedTour != null)
            {
                SingleTourOverview singleTour = new SingleTourOverview(SelectedTour);
                singleTour.Show();
            }
            
        }
    }
}
