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

        #region ICommands and Related Properties
        public ICommand BrowseRomDirectoryCommand { get; private set; }
        public bool CanBrowseRomDirectory { get { return true; } }

        public ICommand BrowseEmulatorDirectoryCommand { get; private set; }
        public bool CanBrowseEmulatorDirectory { get { return true; }}
        
        public ICommand LoadRomsIntoGURUCommand { get; private set; }
        public bool CanLoadRomsIntoGURU { get { return true; } }
        
        public ICommand CleanRomNamesCommand { get; private set; }
        public bool CanCleanRomNames { get { return true; } }
        
        public ICommand CheckRomStreamingCompatibilityCommand { get; private set; }
        public bool CanCheckRomStreamingCompatibility { get { return true; } }
        
        public ICommand FixRomStreamingCompatibilityCommand { get; private set; }
        public bool CanFixRomStreamingCompatibility { get { return true; } }
        
        public ICommand CreateSteamShortcutsCommand { get; private set; }
        public bool CanCreateSteamShortcuts { get { return true; } }
        
        public ICommand DeleteSteamShortcutsCommand { get; private set; }
        public bool CanDeleteSteamShortcuts { get { return true; } }
        #endregion

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

        public void BrowseRomDirectory()
        {
            throw new System.NotImplementedException();
        }

        public void BrowseEmulatorDirectory()
        {
            throw new System.NotImplementedException();
        }

        public void LoadRomsIntoGURU()
        {
            throw new System.NotImplementedException();
        }

        public void CleanRomNames()
        {
            throw new System.NotImplementedException();
        }

        public void CheckRomStreamingCompatibility()
        {
            throw new System.NotImplementedException();
        }

        public void FixRomStreamingCompatibility()
        {
            throw new System.NotImplementedException();
        }

        public void CreateSteamShortcuts()
        {
            throw new System.NotImplementedException();
        }

        public void DeleteSteamShortcuts()
        {
            throw new System.NotImplementedException();
        }
    }
}
