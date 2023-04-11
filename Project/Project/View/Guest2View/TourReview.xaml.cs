using Project.Controller;
using Project.Model;
using Project.Repository;
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

namespace Project.View
{
    /// <summary>
    /// Interaction logic for TourReview.xaml
    /// </summary>
    public partial class TourReview : Window
    {
        public Guest2Controller Controller { get; set; }

        public Appointment ChosenAppointment { get; set; }


        private readonly TourReviewService TourReviewService;

        public TourReview(Guest2Controller guest2Controller, Appointment appointment)
        {
            InitializeComponent();
            DataContext = this;
            Controller = guest2Controller;
            ChosenAppointment = appointment;
            TourReviewService = new TourReviewService();
        }

        private void btSubmit_Click(object sender, RoutedEventArgs e)
        {
            TourReviewService.Create(ChosenAppointment.Id, Controller.Guest.User.Id, GetGuideKnowledgeRating(), GetGuideLanguageRating(), GetInterestingRating(), tbReviewText.Text);
            this.Close();
        }

        private int GetGuideKnowledgeRating()
        {
            int knowledge;
            if (string.IsNullOrEmpty(tbGuideKnowledge.Text))
            {
                MessageBox.Show("Guide Knowledge is empty");
                return 0;
            }
            knowledge = Convert.ToInt32(tbGuideKnowledge.Text);
            return knowledge;
        }

        private int GetGuideLanguageRating()
        {
            int languageRating;
            if(string.IsNullOrEmpty(tbGuideLanguage.Text))
            {
                MessageBox.Show("Guide language is empty");
                return 0;
            }

            languageRating = Convert.ToInt32(tbGuideLanguage.Text);
            return languageRating;
        }

        private int GetInterestingRating()
        {
            int interestingRating;
            if(string.IsNullOrEmpty(tbInterestingRating.Text))
            {
                MessageBox.Show("Guide language is empty");
                return 0;
            }

            interestingRating = Convert.ToInt32(tbInterestingRating.Text);
            return interestingRating;
        }
    }
}
