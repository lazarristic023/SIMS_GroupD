using ScottPlot.Statistics;
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
    /// Interaction logic for TourReviewing.xaml
    /// </summary>
    public partial class TourReviewing : Window
    {

        public ObservableCollection<string> AnotherImages;
        public string images;
        public TourReviewing()
        {
            InitializeComponent();
            AnotherImages = new ObservableCollection<string>();
            images = string.Empty;
        }

        private void Button_Click_AvailableTours(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_Tour_History(object sender, RoutedEventArgs e)
        {
            TourHistoryView tourHistoryView = new TourHistoryView();
            tourHistoryView.Top = this.Top;
            tourHistoryView.Left = this.Left;
            this.Close();
            tourHistoryView.Show();
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

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            TourHistoryView tourHistoryView = new TourHistoryView();
            tourHistoryView.Top = this.Top;
            tourHistoryView.Left = this.Left;
            this.Close();
            tourHistoryView.Show();
        }

        private void btSignOut_Click(object sender, RoutedEventArgs e)
        {
            SignInView signInView = new SignInView();
            Close();
            signInView.Show();
        }

        private void btMouseEnter(object sender, RoutedEventArgs e)
        {
            ChangeButtonBackground(Color.FromRgb(255, 255, 153));
        }

        private void btMouseLeave(object sender, RoutedEventArgs e)
        {
            ChangeButtonBackground(Color.FromRgb(79, 12, 177));
        }

        private void ChangeButtonBackground(Color color)
        {
            btTourReview.Background = new SolidColorBrush(color);
            btTourReview.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void Button_Add_Image_Click(object sender, RoutedEventArgs e)
        {
            //if (images != string.Empty)
            //{
            //    AnotherImages.Add(images);
            //}
            string inputText = txtInput.Text;
            if (!string.IsNullOrEmpty(inputText))
            {
                lstItems.Items.Add(inputText);
                txtInput.Text = string.Empty;
            }
        }
    }
}
