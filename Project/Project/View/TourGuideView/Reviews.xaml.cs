using Project.Model;
using Project.Observer;
using Project.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Project.View.TourGuideView
{
    /// <summary>
    /// Interaction logic for Reviews.xaml
    /// </summary>
    public partial class Reviews : Window, IObserver, INotifyPropertyChanged
    {

        private readonly TourReviewService _tourReviewService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<ReviewDisplay> TourReviews { get; set; }

        public ReviewDisplay SelectedReview { get; set; }
        public Reviews()
        {
            InitializeComponent();
            DataContext = this;

            _tourReviewService = new TourReviewService();
            _tourReviewService.Subscribe(this);
            

            TourReviews = new ObservableCollection<ReviewDisplay>(_tourReviewService.GetReviewForDisplay());

            

        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update()
        {
            TourReviews.Clear();
            foreach(var t in _tourReviewService.GetReviewForDisplay())
            {
                TourReviews.Add(t);
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SingleReview singleReview = new SingleReview(SelectedReview,_tourReviewService);
            singleReview.Owner = this;
            singleReview.Show();
        }
    }
}
