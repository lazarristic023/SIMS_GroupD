using Project.Model;
using Project.ViewModel.Guest1ViewModel;
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

namespace Project.View.Guest1View
{
    /// <summary>
    /// Interaction logic for ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        private ProfileWindowViewModel profileWindowViewModel;
        public ProfileWindow(User u)
        {
            InitializeComponent();
            profileWindowViewModel = new ProfileWindowViewModel(u);
            this.DataContext = profileWindowViewModel;
        }

        private void Label_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MoveReservationWindow moveReservationWindow = new MoveReservationWindow(profileWindowViewModel.User);
            moveReservationWindow.Show();
            Close();
        }
    }
}
