using Project.Controller;
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
    /// Interaction logic for OwnerView.xaml
    /// </summary>
    public partial class OwnerView : Window
    {
        private User user;
        
        private OwnerController controller;

        public ObservableCollection<Accommodation> Accommodations { get; set; }



        public OwnerView(User u)
        {
            InitializeComponent();
            DataContext = this;
            controller = new OwnerController(u);

            Accommodations = new ObservableCollection<Accommodation>(controller.Owner.Accommodations);          //controller.Owner.Accommodations;
            
        }

        private void btSignOut_Click(object sender, RoutedEventArgs e)
        {
            SignInView signInView = new SignInView();
            Close();
            signInView.Show();
        }
    }
}
