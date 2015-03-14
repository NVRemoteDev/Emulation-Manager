using EmulationManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EmulationManager.Commands
{
    public class CreateSteamShortcutsCommand : ICommand
    {
        private EmuManagerViewModel _viewModel;
        public CreateSteamShortcutsCommand(EmuManagerViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        #region ICommand Members
        public bool CanExecute(object parameter)
        {
            return _viewModel.CanCreateSteamShortcuts;
        }

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public async void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                await _viewModel.CreateSteamShortcutsAsync();
            }
        }
        #endregion
    }
}
