using Project.Model;
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

namespace Project.View
{
    /// <summary>
    /// Interaction logic for TourHistoryView.xaml
    /// </summary>
    public partial class TourHistoryView : Window
    {
        User user;
        private ObservableCollection<Review> review;
        public TourHistoryView()
        {
            InitializeComponent();
            review = new ObservableCollection<Review>();
            myDataGrid.ItemsSource = review;
            AddRows();
        }

        private void AddRows()
        {
            review.Add(new Review { Name = "Obilazak Subotice", Location = "Srbija, Subotica", Duration = 2, Guide = "Guide 1" });
            review.Add(new Review { Name = "Obilazak Beograda", Location = "Srbija, Beograd", Duration = 4, Guide = "Guide 2" });
            review.Add(new Review { Name = "Obilazak Novog Sada", Location = "Srbija, Novi Sad", Duration = 2, Guide = "Guide 3" });
            review.Add(new Review { Name = "Obilazak Zrenjanina", Location = "Srbija, Zrenjanin", Duration = 2, Guide = "Guide 1" });
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btSignOut_Click(object sender, RoutedEventArgs e)
        {
            SignInView signInView = new SignInView();
            Close();
            signInView.Show();
        }

        private void tbReviewTour_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TourReviewing tourReviewing = new TourReviewing();
            tourReviewing.Top = this.Top;
            tourReviewing.Left = this.Left;
            this.Close();
            tourReviewing.Show();
        }

        private void Button_Click_Active_Tours(object sender, RoutedEventArgs e)
        {
            ProfileGuest2 profileGuest2 = new ProfileGuest2();
            profileGuest2.Top = this.Top;
            profileGuest2.Left = this.Left;
            this.Close();
            profileGuest2.Show();
        }

        private void Button_Click_Vouchers(object sender, RoutedEventArgs e)
        {
            VoucherView voucherView = new VoucherView();
            voucherView.Top = this.Top;
            voucherView.Left = this.Left;
            this.Close();
            voucherView.Show();
        }

        private void Button_Click_AvailableTours(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_Tour_History(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Complex_Click(object sender, RoutedEventArgs e)
        {
            ComplexTourRequest request = new ComplexTourRequest();
            request.Top = this.Top;
            request.Left = this.Left;
            this.Close();
            request.Show();
        }

        private void Button_Request_Click(object sender, RoutedEventArgs e)
        {
            Guest2TourRequests request = new Guest2TourRequests(user);
            request.Top = this.Top;
            request.Left = this.Left;
            this.Close();
            request.Show();
        }

        private void Button_ActiveTour_Click(object sender, RoutedEventArgs e)
        {
            ProfileGuest2 profile = new ProfileGuest2();
            profile.Top = this.Top;
            profile.Left = this.Left;
            this.Close();
            profile.Show();
        }
    }
}
