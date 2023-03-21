using Project.Controller;
using Project.Model;
using System;
using System.Collections.Generic;
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
    public partial class TourTracking : Window
    {
        private readonly TourGuideController _tourGuideController;

        TourPointsList tourPointsList { get; set; }
        public TourTracking(int tourId)
        {
            InitializeComponent();
            DataContext = this;

            tourPointsList = new TourPointsList();
            tourPointsList.TourId = tourId;

            _tourGuideController = new TourGuideController();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tourName.Content = _tourGuideController.GetById(tourPointsList.TourId).Name;
        }
    }
}
