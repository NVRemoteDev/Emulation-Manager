using EmulationManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmulationManager.Commands
{
    public class RevertRomStreamingCompatibilityCommand : ICommand
    {
        private EmuManagerViewModel _viewModel;
        public RevertRomStreamingCompatibilityCommand(EmuManagerViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        #region ICommand Members
        public bool CanExecute(object parameter)
        {
            return _viewModel.CanCheckRomStreamingCompatibility;
        }

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                _viewModel.RevertRomStreamingCompatibilityAsync();
            }
        }
        #endregion
    }
}
