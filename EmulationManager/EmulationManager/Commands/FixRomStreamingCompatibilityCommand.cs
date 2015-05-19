using EmulationManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmulationManager.Commands
{
    public class FixRomStreamingCompatibilityCommand : ICommand
    {
        private EmuManagerViewModel _viewModel;
        public FixRomStreamingCompatibilityCommand(EmuManagerViewModel viewModel)
        {
            _viewModel = viewModel;
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
                _viewModel.FixRomStreamingCompatibilityAsync();
            }
        }
        #endregion
    }
}
