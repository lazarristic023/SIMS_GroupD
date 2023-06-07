using Project.Command;
using Project.Model;
using Project.Observer;
using Project.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.ViewModel.Guest2ViewModel
{
    public class ComplexTourDetailsViewModel : CloseableViewModel, IObserver
    {
        private ObservableCollection<Tourr> _tourrs;

        public ObservableCollection<Tourr> Tourrs
        {
            get
            {
                return _tourrs;
            }

            set
            {
                _tourrs = value;
                OnPropertyChanged(nameof(Tourrs));
            }
        }

        public ComplexTourDetailsViewModel()
        {
            Tourrs = new ObservableCollection<Tourr>();
            PopulateDataGrid();
        }

        public void PopulateDataGrid()
        {
            Tourrs = new ObservableCollection<Tourr>();
            Tourrs.Add(new Tourr { Name = "Obilazak Petrovaradina", Language = "Srpski",StartDate= new DateTime(), Duration = 2, Status = "onhold" });
            Tourrs.Add(new Tourr { Name = "Obilazak 2", Language = "English", StartDate = new DateTime(), Duration = 3, Status = "onhold" });

        }

        public void Update()
        {
            throw new NotImplementedException();
        }


        private RelayCommand signOutCommand;

        public ICommand SignOutCommand
        {
            get
            {
                if (signOutCommand == null)
                {
                    signOutCommand = new RelayCommand(param => this.SignOut(), param => this.CanSignOut());
                }
                return signOutCommand;
            }
        }

        public bool CanSignOut()
        {
            return true;
        }

        public void SignOut()
        {
            SignInView signInView = new SignInView();
            this.Window.Close();
            signInView.Show();
            
        }
        
    }
}
