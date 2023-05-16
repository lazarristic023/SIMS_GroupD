using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Command.Guest2Commands
{
    public class Guest2Command : CommandBase
    {
        private readonly Guest2ViewModel _viewModel;

        public Guest2Command(Guest2ViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }
    }
}
