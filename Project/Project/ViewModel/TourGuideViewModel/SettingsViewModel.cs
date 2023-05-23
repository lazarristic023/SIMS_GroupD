using Project.Command;
using Project.Model;
using Project.View.TourGuideView;
using ScottPlot.Renderable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Project.ViewModel.TourGuideViewModel
{
    public class SettingsViewModel:ViewModelBase
    {

        private User _currentUser = new User();
        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        public SettingsViewModel(Model.User user)
        {
            CurrentUser = user;
            
        }

        private RelayCommand changePasswordCommand;
        public ICommand ChangePasswordCommand
        {
            get
            {
                if (changePasswordCommand == null)
                {
                    changePasswordCommand = new RelayCommand(param => this.ChangePassword(), param => this.CanChangePassword());
                }
                return changePasswordCommand;
            }
        }



        private bool CanChangePassword()
        {
            return true;
        }

        private void ChangePassword()
        {
            ChangePassword changePasswordView = new ChangePassword(CurrentUser);
            changePasswordView.Show();
        }

        private bool CanQuit()
        {
            return true;
        }

        private RelayCommand quitCommand;
        public ICommand QuitCommand
        {
            get
            {
                if (quitCommand == null)
                {
                    quitCommand = new RelayCommand(param => this.Quit(), param => this.CanQuit());
                }
                return quitCommand;
            }
        }
        private void Quit()
        {
            DialogResult dialogResult = MessageBox.Show("Sure", "Some Title", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //do something
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }

        }


    }
}
