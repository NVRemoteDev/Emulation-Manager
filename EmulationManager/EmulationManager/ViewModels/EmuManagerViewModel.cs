using EmulationManager.Commands;
using EmulationManager.Helpers;
using EmulationManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace EmulationManager.ViewModels
{
    public class EmuManagerViewModel
    {
        protected Dispatcher dispatcher = Application.Current.Dispatcher;

        public EmuManagerModel EmuManagerModel { get; set; }
        public EmulatorModel[] EmulatorModels { get; set; }
        public RomModel[] RomModels { get; set; }

        #region ICommands and Related Properties        
        public ICommand LoadRomsAndEmulatorsCommand { get; private set; }
        
        public ICommand CleanRomNamesCommand { get; private set; }
        
        public ICommand CheckRomStreamingCompatibilityCommand { get; private set; }
        
        public ICommand FixRomStreamingCompatibilityCommand { get; private set; }
        
        public ICommand CreateSteamShortcutsCommand { get; private set; }
        
        public ICommand DeleteSteamShortcutsCommand { get; private set; }

        //TODO: Many of these need more actual checks
        public bool CanLoadRomsAndEmulators
        { 
            get 
            {
                return CheckRomAndEmulatorDirectories();
            } 
        }
        public bool CanCleanRomNames 
        { 
            get 
            {
                return CheckRomAndEmulatorDirectories();
            } 
        }
        public bool CanCheckRomStreamingCompatibility 
        {
            get
            {
                return CheckRomAndEmulatorDirectories();
            } 
        }
        public bool CanFixRomStreamingCompatibility 
        {
            get
            {
                return CheckRomAndEmulatorDirectories();
            }  
        }
        public bool CanCreateSteamShortcuts 
        {
            get
            {
                return CheckRomAndEmulatorDirectories();
            } 
        }
        public bool CanDeleteSteamShortcuts 
        {
            get
            {
                return CheckRomAndEmulatorDirectories();
            } 
        }
        #endregion

        public EmuManagerViewModel()
        {
            EmuManagerModel = new EmuManagerModel();

            LoadRomsAndEmulatorsCommand = new LoadRomsAndEmulatorsCommand(this);
            CleanRomNamesCommand = new CleanRomNamesCommand(this);
            CheckRomStreamingCompatibilityCommand = new CheckRomStreamingCompatibilityCommand(this);
            FixRomStreamingCompatibilityCommand = new FixRomStreamingCompatibilityCommand(this);
            CreateSteamShortcutsCommand = new CreateSteamShortcutsCommand(this);
            DeleteSteamShortcutsCommand = new DeleteSteamShortcutsCommand(this);
        }

        public async Task LoadRomsAndEmulatorsAsync()
        {
            // The methods below lock up the UI thread. Take it this off the UI thread.
            await Task.Run(() =>
            {
                EmulatorModels = IOHelper.GetEmulatorInformationFromDisk(EmuManagerModel.EmulatorDirectory);
                RomModels = IOHelper.GetRomInformationFromDisk(EmuManagerModel.RomDirectory);
            });

            EmuManagerModel.RomsLoadedCount = RomModels.Length.ToString();
            EmuManagerModel.EmulatorsLoadedCount = EmulatorModels.Length.ToString();
            EmuManagerModel.ConsolesWithRomsCount = RomModels.GroupBy(x => x.Console).ToList().Count.ToString();
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

        public bool CheckRomAndEmulatorDirectories()
        {
            if (string.IsNullOrEmpty(EmuManagerModel.RomDirectory) || string.IsNullOrEmpty(EmuManagerModel.EmulatorDirectory))
            {
                return false;
            }
            return true; 
        }
    }
}
