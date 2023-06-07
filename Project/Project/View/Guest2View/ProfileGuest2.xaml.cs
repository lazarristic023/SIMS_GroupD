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

namespace Project.View
{
    /// <summary>
    /// Interaction logic for ProfileGuest2.xaml
    /// </summary>
    public partial class ProfileGuest2 : Window, IObserver
    {
        User user;
        public ProfileGuest2(User u)
        {
            InitializeComponent();
            DataContext = this;
        }

        private ObservableCollection<Review> review;

        public ProfileGuest2()
        {
            InitializeComponent();
            review = new ObservableCollection<Review>();
            MyDataGrid.ItemsSource = review;
            AddRows();
        }

        private void AddRows()
        {
            review.Add(new Review { Name = "Obilazak Subotice", Location = "Srbija, Subotica", Duration = 2, Guide = "Guide 1" });
            review.Add(new Review { Name = "Obilazak Beograda", Location = "Srbija, Beograd", Duration = 4, Guide = "Guide 2" });
            review.Add(new Review { Name = "Obilazak Novog Sada", Location = "Srbija, Novi Sad", Duration = 2, Guide = "Guide 3" });
            review.Add(new Review { Name = "Obilazak Zrenjanina", Location = "Srbija, Zrenjanin", Duration = 2, Guide = "Guide 1" });
        }

        private void btSignOut_Click(object sender, RoutedEventArgs e)
        {
            SignInView signInView = new SignInView();
            Close();
            signInView.Show();
        }

        private void Button_Click_AvailableTours(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_Vouchers(object sender, RoutedEventArgs e)
        {
            VoucherView voucherView = new VoucherView();
            voucherView.Top = this.Top;
            voucherView.Left = this.Left;
            this.Close();
            voucherView.Show();
        }

        private void Button_Click_Tour_History(object sender, RoutedEventArgs e)
        {
            TourHistoryView tourHistory = new TourHistoryView();
            tourHistory.Top = this.Top;
            tourHistory.Left = this.Left;
            this.Close();
            tourHistory.Show();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        private void Button_Click_Tour_Requests(object sender, RoutedEventArgs e)
        {
            Guest2TourRequests tourRequests = new Guest2TourRequests(user);
            tourRequests.Top = this.Top;
            tourRequests.Left = this.Left;
            this.Close();
            tourRequests.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyReservedTourInfo myReservedTourInfo = new MyReservedTourInfo();
            myReservedTourInfo.Top = this.Top;
            myReservedTourInfo.Left = this.Left;
            myReservedTourInfo.Show();
        }
    }
}
