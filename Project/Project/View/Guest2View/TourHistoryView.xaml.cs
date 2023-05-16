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
            review.Add(new Review { Name = "Obilazak Subotice", Location = "Srbija, Subotica", Duration = 2, Guide = "Guide 1"});
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
            VoucherView vouchers = new VoucherView();
            vouchers.Top = this.Top;
            vouchers.Left = this.Left;
            this.Close();
            vouchers.Show();
        }
    }
}
