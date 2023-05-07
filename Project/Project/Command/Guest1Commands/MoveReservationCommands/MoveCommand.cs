using Project.View.Guest1View;
using Project.ViewModel.Guest1ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project.Command.Guest1Commands.MoveReservationCommands
{
    public class MoveCommand : CommandBase
    {
        MoveReservationViewModel _viewModel;

        public MoveCommand(MoveReservationViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            if (_viewModel.SelectedReservation == null)
            {
                MessageBox.Show("Choose a reservation first!", "Reservation not chosen", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_viewModel.SelectedReservation.StartDate <= DateTime.Now.Date)
            {
                MessageBox.Show("You can not move reservation that has already started", "Reservation already started", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MakeMoveRequestView makeMoveRequestView = new MakeMoveRequestView(_viewModel.SelectedReservation, _viewModel.User);
            makeMoveRequestView.Show();
        }
    }
}
