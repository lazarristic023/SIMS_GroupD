using Project.Controller;
using Project.Model;
using Project.Observer;
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

namespace Project.View.TourGuideView
{
    /// <summary>
    /// Interaction logic for TourTracking.xaml
    /// </summary>
    public partial class TourTracking : Window, IObserver
    {
        private readonly TourGuideController _tourGuideController;
        private readonly TourPointController _tourPointController;
        private readonly TourPointsListController _tourPointsListController;

        public int tourId { get; set; }

        public ObservableCollection<TourPoint> tourPoints { get; set; }


        private int numberOfNextClicks = 0;

        public TourPoint selectedPoint { get; set; }



        public TourTracking(int sendedId)
        {
            InitializeComponent();
            DataContext = this;

            _tourGuideController = new TourGuideController();
            _tourPointController = new TourPointController();
            _tourPointsListController = new TourPointsListController();

            tourId = sendedId;

            tourPoints = new ObservableCollection<TourPoint>(_tourPointsListController.GetPointsByTourId(tourId));

            pointsListBox.SelectedIndex = 0;





            //tourPoints[0].Action = true;

            //AddRadioButtons();



            

               
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tourName.Content = _tourGuideController.GetById(tourId).Name;
            selectedPoint = (TourPoint)pointsListBox.SelectedItem;
            selectedPoint = ChangeActivity(selectedPoint);


            
        }

        //public void AddRadioButtons()
        //{
        //    int counter = 0;
        //    int radioButtonId = 1;

        //    foreach (TourPoint point in tourPoints)
        //    {
        //        var radioButton = CreateRadioButton(point, radioButtonId);
        //        radioButtonsStackPanel.Children.Add(radioButton);
        //        radioButtonId++;
        //        counter++;
        //    }

        //    numberOfRadioButtons = radioButtonId;
        //}

        //private RadioButton CreateRadioButton(TourPoint tourPoint, int id)
        //{
        //    RadioButton radioButton = new RadioButton();
        //    radioButton.Content = tourPoint.PointName;
        //    radioButton.IsChecked = tourPoint.Action;
        //    radioButton.Name = "rb" + id;
        //    radioButton.GroupName = "activeTour";

        //    return radioButton;

        //}

        //public List<TourPoint> GetTourPoints(int id)
        //{
        //    List<TourPoint> points = new List<TourPoint>();

        //    TourPointsList tourPointsList = _tourPointsListController.GetByTourId(id);

        //    foreach(int tourPointId in tourPointsList.PointsId)
        //    {
        //        points.Add(_tourPointController.GetById(tourPointId));
        //    }

        //    return points;
        //}

        private void endTour_Click(object sender, RoutedEventArgs e)
        {
            selectedPoint.Action = false;
            EndTheAppointment();
        }

        private void EndTheAppointment()
        {
            MessageBox.Show("The tour is over");
            Close();
        }

        private void nextPoint_Click(object sender, RoutedEventArgs e)
        {
            selectedPoint = ChangeActivity(selectedPoint);

            pointsListBox.SelectedIndex++;
            numberOfNextClicks++;
            int lastIndex = pointsListBox.Items.Count - 1;

            if(numberOfNextClicks > lastIndex)
            {
                EndTheAppointment();
            }

            selectedPoint = (TourPoint)pointsListBox.SelectedItem;
            selectedPoint = ChangeActivity(selectedPoint);
       
        }

        public TourPoint ChangeActivity(TourPoint point)
        {
            if(point.Action == true)
            {
                point.Action = false;
            }
            else
            {
                point.Action = true;
            }

            return point;

        }
        


        public void Update()
        {
            UpdatePoints();
        }

        public void UpdatePoints()
        {
            tourPoints.Clear();

            foreach (var point in _tourPointsListController.GetPointsByTourId(tourId))
            {
                tourPoints.Add(point);
            }
        }

        
    }
}
