using MEGAEmulationManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MEGAEmulationManager.Commands
{
    public class FixRomStreamingCompatibilityCommand : ICommand
    {
        private EmuManagerViewModel _viewModel;
        public FixRomStreamingCompatibilityCommand(EmuManagerViewModel viewmodel)
        {
            _viewModel = viewmodel;
        }

        #region ICommand Members
        public bool CanExecute(object parameter)
        {
            return _viewModel.CanFixRomStreamingCompatibility;
        }

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                _viewModel.FixRomStreamingCompatibility();
            }
        }
        #endregion
    }
}
