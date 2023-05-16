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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project.View
{
    /// <summary>
    /// Interaction logic for TourReviewing.xaml
    /// </summary>
    public partial class TourReviewing : Window
    {
        public TourReviewing()
        {
            InitializeComponent();
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

        private void btMouseEnter(object sender, MouseEventArgs e)
        {
            ChangeButtonBackground(Color.FromRgb(255, 255, 153));
        }

        private void btMouseLeave(object sender, MouseEventArgs e)
        {
            ChangeButtonBackground(Color.FromRgb(79, 12, 177)); 
        }

        private void ChangeButtonBackground(Color color)
        {
            btTourReview.Background = new SolidColorBrush(color);
            btTourReview.Foreground = new SolidColorBrush(Color.FromRgb(255,255,255));
        }

        
    }
   }
