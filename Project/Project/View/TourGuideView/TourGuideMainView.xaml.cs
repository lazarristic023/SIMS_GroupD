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

        List<DateTime> dates = new List<DateTime>();
        List<string> images = new List<string>();
        List<int> pointsIds = new List<int>();
        


        public ObservableCollection<Tour> Tours { get; set; }
        public ObservableCollection<TourPointsList> Points { get; set; }

        User User { get; set; }

        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                if(value != _id)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _country;
        public string Country
        {
            get => _country;
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _city;
        public string City
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _name;
        public string NameOfTour
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();

                }
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _language;
        public string LanguageOfTour
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _maxGuests;
        public int MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _startTime;
        public string StartTime
        {
            get => _startTime;
            set
            {
                if (value != _startTime)
                {
                    _startTime = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _duration;
        public int Duration
        {
            get => _duration;
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _coverImageUrl;
        public string CoverImageUrl
        {
            get => _coverImageUrl;
            set
            {
                if (value != _coverImageUrl)
                {
                    _coverImageUrl = value;
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
            _imageController = new ImageController();
            _tourPointController = new TourPointController();
            _tourPointsListController = new TourPointsListController();
            _locationController = new LocationController();


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



        private void AddPictureButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(openFileDialog.FileName);
                bitmap.DecodePixelHeight = 200;
                bitmap.EndInit();

                images.Add(openFileDialog.FileName);
                imageList.Items.Add(bitmap);
            }

        }

        private void AddPoints_Click(object sender, RoutedEventArgs e)
        {
            string anotherPoint = pointInput.Text;



            if(anotherPoint != "")
            {
                int anotherTourPointId = _tourPointController.Create(anotherPoint, false);
                pointsIds.Add(anotherTourPointId);   
            }

            
            pointInput.Clear();
            pointsList.Items.Add(anotherPoint);

        }

        private void pointInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                e.Handled = true;
                AddPoints_Click(sender,e);
            }

        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            int locationId = -1;

            if (cityComboBox.SelectedItem.Equals != null && countryComboBox.SelectedItem != null)
            {
                locationId = _locationController.Create(cityComboBox.SelectedItem.ToString(), countryComboBox.SelectedItem.ToString());
            }

            int tourId = _tourGuideController.Create(locationId, NameOfTour, Description, LanguageOfTour, MaxGuests, Duration);

           
            _tourAppointmentsController.Create(tourId,dates);
            dates.Clear();

            if (startPointTextBox.Text != "" && endPointTextBox.Text != "")
            {
                int startPointId = _tourPointController.Create(startPointTextBox.Text, false);
                int endPointId = _tourPointController.Create(endPointTextBox.Text, false);
                pointsIds.Insert(0, startPointId);
                pointsIds.Add(endPointId);
            }

            _tourPointsListController.Create(tourId,pointsIds);

            foreach (string image in images)
            {
                _imageController.Create(image,tourId,Model.PictureType.TOUR);
            }
            images.Clear();

            


            MessageBox.Show("Tour is added");

            tourNameTextbox.Clear();
        }

        private void AddDate_Click(object sender, RoutedEventArgs e)
        {
            
            string[] timeSplit = time.Text.Split(':');
            //if(date.SelectedDate == null)
            //{
            //    throw new ArgumentException("No date has been selected");
            //}

            DateTime datetime = new DateTime(date.SelectedDate.Value.Year, date.SelectedDate.Value.Month, date.SelectedDate.Value.Day, int.Parse(timeSplit[0]), int.Parse(timeSplit[1]),0);

            time.Clear();
            
            dates.Add(datetime);

            string dateAndTime = datetime.ToString();
            dateTimeList.Items.Add(dateAndTime);    


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            usernameLabel.Content = User.Username;

            StreamReader countrySource = new StreamReader(@"../../../Resources/Data/country.csv");
            string content = countrySource.ReadToEnd();
            string[] country = content.Split('|');
            foreach(string element in country)
            {
                countryComboBox.Items.Add(element);
            }

            AddPoints.IsEnabled = false;


        }

        private void countryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StreamReader citySource = new StreamReader(@"../../../Resources/Data/city.csv");

            cityComboBox.Items.Clear();

            string line;


            while ((line = citySource.ReadLine()) != null)
            {

                string[] couple = line.Split('|');
                if (couple[0] == countryComboBox.SelectedItem.ToString())
                {
                    string[] city = couple[1].Split(';');
                    foreach (string word in city)
                    {
                        cityComboBox.Items.Add(word);
                    }
                }
            }
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //dateTimeList.Items.Clear();
            //dates.Clear();
            //images.Clear();
            //imageList.Items.Clear();
        }

        private void startPointTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(startPointTextBox.Text != "" && endPointTextBox.Text != "")
            {
                AddPoints.IsEnabled = true;
            }
            else
            {
                AddPoints.IsEnabled = false;
            }
        }

        private void addTourButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewTour addNewTour = new AddNewTour(_tourGuideController,_tourAppointmentsController,_imageController,_tourPointController,_tourPointsListController,_locationController);
            addNewTour.Show();
        }
    }
}
