using Project.View;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Command.Guest2Commands.LinkCommands
{
    public class Guest2LinkCommand : CommandBase
    {
        private readonly ViewModelBase viewmodelBase;

        public Guest2LinkCommand(ViewModelBase viewModelBase)
        {
            this.viewmodelBase = viewModelBase;
        }

        public override void Execute(object? parameter)
        {
            Guest2View guest2View = new Guest2View(viewmodelBase.User);
            guest2View.Show();
            viewmodelBase.Window.Close();
        }
    }
}
