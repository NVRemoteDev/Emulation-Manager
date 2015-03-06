using MEGAEmulationManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MEGAEmulationManager.Commands
{
    public class CreateSteamShortcutsCommand : ICommand
    {
        private EmuManagerViewModel _viewmodel;
        public CreateSteamShortcutsCommand(EmuManagerViewModel viewmodel)
        {
            _viewmodel = viewmodel;
        }
    }
}
