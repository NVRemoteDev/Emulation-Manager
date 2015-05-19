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
            CheckRomStreamingCompatibilityCommand = new CheckRomStreamingCompatibilityCommand(this);
            FixRomStreamingCompatibilityCommand = new FixRomStreamingCompatibilityCommand(this);
            CreateSteamShortcutsCommand = new CreateSteamShortcutsCommand(this);
            DeleteSteamShortcutsCommand = new DeleteSteamShortcutsCommand(this);
        }

        public async Task LoadRomsAndEmulatorsAsync()
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
                // If either model is null, we probably have already shown an error to the user,
                // since something screwed up. But we may want to explain that nothing has happened in terms
                // of loading the emulators and roms.
                // TODO: Show error alert.
                return;
            }

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

        public async Task FixRomStreamingCompatibilityAsync()
        {
            if (CheckModelValidity())
            {
                await Task.Run(() =>
                {
                    IsLoading = true;
                    LoadingText = "Fixing rom streaming compatability...";

                    RomHelper.FixRomsForStreaming(RomModels);

                    LoadingText = string.Empty;
                    IsLoading = false;
                });
            }
        }

        public async Task CreateSteamShortcutsAsync()
        {
            if (CheckModelValidity())
            {
                await LoadRomsAndEmulatorsAsync();
                await Task.Run(() =>
                {
                    IsLoading = true;
                    LoadingText = "Writing Steam Shortcuts";

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
    }
}
