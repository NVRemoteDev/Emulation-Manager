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
using EmulationManager.Common;

namespace EmulationManager.ViewModels
{
    public class EmuManagerViewModel : NotifierBase
    {
        protected Dispatcher dispatcher = Application.Current.Dispatcher;

        public EmuManagerModel EmuManagerModel { get; set; }
        public EmulatorModel[] EmulatorModels { get; set; }
        public RomModel[] RomModels { get; set; }

        #region ICommands and Related Properties        
        public ICommand LoadRomsAndEmulatorsCommand { get; private set; }
        
        public ICommand CleanRomNamesCommand { get; private set; }
        
        public ICommand RevertRomStreamingCompatibilityCommand { get; private set; }
        
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

        #region Properties

        private string _loadingText;
        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                SetProperty(ref _isLoading, value);
                OnPropertyChanged();
            }
        }

        public string LoadingText
        {
            get { return _loadingText; }
            set
            {
                SetProperty(ref _loadingText, value);
                OnPropertyChanged();
            }
        }
        #endregion

        public EmuManagerViewModel()
        {
            EmuManagerModel = new EmuManagerModel();

            LoadRomsAndEmulatorsCommand = new LoadRomsAndEmulatorsCommand(this);
            CleanRomNamesCommand = new CleanRomNamesCommand(this);
            RevertRomStreamingCompatibilityCommand = new RevertRomStreamingCompatibilityCommand(this);
            FixRomStreamingCompatibilityCommand = new FixRomStreamingCompatibilityCommand(this);
            CreateSteamShortcutsCommand = new CreateSteamShortcutsCommand(this);
            DeleteSteamShortcutsCommand = new DeleteSteamShortcutsCommand(this);

            if (EmuManagerModel.AutoImportRoms)
            {
                LoadRomsAndEmulatorsAsync(false);
            }
        }

        public async Task LoadRomsAndEmulatorsAsync(bool showError = true)
        {
            if (CheckRomAndEmulatorDirectories() && CheckCanDoWork())
            {
                // Use a Task.Run() here to get these methods off the UI thread as they're slow and it will lock up responsiveness.
                await Task.Run(() =>
                {
                    IsLoading = true;
                    LoadingText = "Loading emulator information from disk...";

                    EmulatorModels = IOHelper.GetEmulatorInformationFromDisk(EmuManagerModel.EmulatorDirectory);

                    LoadingText = "Loading rom information from disk...";
                    RomModels = IOHelper.GetRomInformationFromDisk(EmuManagerModel.RomDirectory);

                    LoadingText = string.Empty;
                    IsLoading = false;
                });

                if (EmulatorModels == null || RomModels == null)
                {
                    DebugManager.ShowErrorDialog("No roms, or no emulators were able to be loaded. Please check your directories layout.", null);
                    return;
                }

                EmuManagerModel.RomsLoadedCount = RomModels.Length.ToString();
                EmuManagerModel.EmulatorsLoadedCount = EmulatorModels.Length.ToString();
                EmuManagerModel.ConsolesWithRomsCount = RomModels.GroupBy(x => x.Console).ToList().Count.ToString();
            }
            else
            {
                // Since this could conceivably be autoloaded we don't want to show an error unless it's a distinct user interaction that calls this method
                if (showError)
                    DebugManager.ShowErrorDialog("You must load your ROMs and emulators first.", null);
            }
        }

        public void CleanRomNames()
        {
            throw new System.NotImplementedException();
        }

        public async Task RevertRomStreamingCompatibilityAsync()
        {
            if (CheckModelValidity() && CheckCanDoWork())
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                    LoadingText = "Reverting rom streaming compatibility...";

                    RomModels = IOHelper.RevertRomsFromStreaming(RomModels);

                    LoadingText = string.Empty;
                    IsLoading = false;
                });
            }
            else
            {
                DebugManager.ShowErrorDialog("You must load your ROMs and emulators first.", null);
            }
        }

        public async Task FixRomStreamingCompatibilityAsync()
        {
            if (CheckModelValidity() && CheckCanDoWork())
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                    LoadingText = "Fixing rom streaming compatibility...";

                    RomModels = IOHelper.FixRomsForStreaming(RomModels);

                    LoadingText = string.Empty;
                    IsLoading = false;
                });
            }
            else
            {
                DebugManager.ShowErrorDialog("You must load your ROMs and emulators first.", null);
            }
        }

        public async Task CreateSteamShortcutsAsync()
        {
            if (CheckModelValidity() && CheckCanDoWork())
            {
                await LoadRomsAndEmulatorsAsync();
                await Task.Run(() =>
                {
                    IsLoading = true;
                    LoadingText = "Writing Steam shortcuts...";

                    SteamHelper.WriteSteamShortcuts(RomModels, EmulatorModels);

                    LoadingText = string.Empty;
                    IsLoading = false;
                });
            }
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

        public bool CheckModelValidity()
        {
            return EmulatorModels != null && EmulatorModels.Length > 0 && RomModels != null && RomModels.Length > 0;
        }

        public bool CheckCanDoWork()
        {
            if (IsLoading)
            {
                DebugManager.ShowErrorDialog("Please wait for the current operation to finish before proceeding.", null);
                return false;
            }
            return true;
        }
    }
}
