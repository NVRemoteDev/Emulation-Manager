using MEGAEmulationManager.Commands;
using MEGAEmulationManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MEGAEmulationManager.ViewModels
{
    public class EmuManagerViewModel
    {
        public EmuManagerModel EmuManagerModel { get; set; }

        public ICommand BrowseRomDirectoryCommand { get; private set; }
        public ICommand BrowseEmulatorDirectoryCommand { get; private set; }
        public ICommand LoadRomsIntoGURUCommand { get; private set; }
        public ICommand CleanRomNamesCommand { get; private set; }
        public ICommand CheckRomStreamingCompatibilityCommand { get; private set; }
        public ICommand FixRomStreamingCompatibilityCommand { get; private set; }
        public ICommand CreateSteamShortcutsCommand { get; private set; }
        public ICommand DeleteSteamShortcutsCommand { get; private set; }

        public EmuManagerViewModel()
        {
            EmuManagerModel = new EmuManagerModel();

            //ICommand BrowseRomDirectoryCommand = new Command(this);
            //ICommand BrowseEmulatorDirectoryCommand = new Command(this);
            ICommand LoadRomsIntoGURUCommand = new LoadRomsIntoGURUCommand(this);
            ICommand CleanRomNamesCommand = new CleanRomNamesCommand(this);
            ICommand CheckRomStreamingCompatibilityCommand = new CheckRomStreamingCompatibilityCommand(this);
            ICommand FixRomStreamingCompatibilityCommand = new FixRomStreamingCompatibilityCommand(this);
            ICommand CreateSteamShortcutsCommand = new CreateSteamShortcutsCommand(this);
            ICommand DeleteSteamShortcutsCommand = new DeleteSteamShortcutsCommand(this);
        }

    }
}
