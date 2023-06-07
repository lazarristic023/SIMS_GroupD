using Project.Model;
using Project.ViewModel.Guest2ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
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
    /// Interaction logic for VoucherView.xaml
    /// </summary>
    public partial class VoucherView : Window
    {

       

        public VoucherView()
        {
            InitializeComponent();
            var vm = new VoucherViewModel();
            this.DataContext = vm;
        }




        //private void AddRow()
        //{
        //    voucher.Add(new Voucher { Name = "Voucher 1", ExpirationDate = new DateTime(), Description = "Voucher 1 dobijen 200202020" });
        //    voucher.Add(new Voucher { Name = "Voucher 2", ExpirationDate = new DateTime(), Description = "Voucher 2 dobijen 200202020" });
        //    voucher.Add(new Voucher { Name = "Voucher 3", ExpirationDate = new DateTime(), Description = "Voucher 3 dobijen 200202020" });
        //    voucher.Add(new Voucher { Name = "Voucher 4", ExpirationDate = new DateTime(), Description = "Voucher 4 dobijen 200202020" });
        //}

        //private void btSignOut_Click(object sender, RoutedEventArgs e)
        //{
        //    SignInView signInView = new SignInView();
        //    Close();
        //    signInView.Show();
        //}

        //private void Button_Click_Active_Tours(object sender, RoutedEventArgs e)
        //{
        //    ProfileGuest2 profileGuest2 = new ProfileGuest2();
        //    profileGuest2.Top = this.Top;
        //    profileGuest2.Left = this.Left;
        //    this.Close();
        //    profileGuest2.Show();
        //}

        //private void Button_Click_Tour_History(object sender, RoutedEventArgs e)
        //{
        //    TourHistoryView tourHistory = new TourHistoryView();
        //    tourHistory.Top = this.Top;
        //    tourHistory.Left = this.Left;
        //    this.Close();
        //    tourHistory.Show();
        //}

        //private void Button_Click_Back(object sender, RoutedEventArgs e)
        //{
        //    Close();
        //}

        //private void Button_Click_AvailableTours(object sender, RoutedEventArgs e)
        //{
        //    Close();
        //}
    }
}
