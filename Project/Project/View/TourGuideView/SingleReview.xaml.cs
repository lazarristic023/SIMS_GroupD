using Project.Service;
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
    /// Interaction logic for SingleReview.xaml
    /// </summary>
    public partial class SingleReview : Window
    {
        private readonly PresentGuestsService presentGuestsService;
        private readonly TourReviewService _tourReviewService;

        public int Id { get; set; }
        public string guestName { get; set;}
        public string tourName { get; set; }
        public string boardingPoint { get; set; }
        public int knowledgeRating { get;set; }
        public int languageRating { get; set; }
        public int interestingRating { get; set; }
        public double avgRating { get; set; }
        public string text { get; set; }
        public SingleReview(ReviewDisplay selected, TourReviewService tourReviewService)
        {
            InitializeComponent();
            DataContext = this;

            presentGuestsService = new PresentGuestsService();
            _tourReviewService = tourReviewService;

            Id = selected.Id;
            guestName = selected.userName;
            tourName = selected.tourName;
            

            
            boardingPoint = presentGuestsService.GetBoardingPoint(selected.userId, selected.appointmentId);
            

            knowledgeRating = selected.knowledgeRating;
            languageRating = selected.languageRating;
            interestingRating = selected.interestingRating;
            avgRating = selected.avgRating;
            text = selected.review;
             

        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void makeInvalidBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to mark this review as invalid?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //no
            }
            else
            {
                //yes
                _tourReviewService.MarkAsInvalid(Id);
            }
        }
    }
}
