using MEGAEmulationManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MEGAEmulationManager.Commands
{
    public class LoadRomsIntoGURUCommand : ICommand
    {
        private EmuManagerViewModel _viewModel;
        public LoadRomsIntoGURUCommand(EmuManagerViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        #region ICommand Members
        public bool CanExecute(object parameter)
        {
            return _viewModel.CanLoadRomsIntoGURU;
        }

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                _viewModel.LoadRomsIntoGURU();
            }
        }
        #endregion
    }
}
