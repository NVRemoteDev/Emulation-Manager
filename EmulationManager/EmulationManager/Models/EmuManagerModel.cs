using EmulationManager.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmulationManager.Common;

namespace EmulationManager.Models
{
    public class EmuManagerModel : NotifierBase
    {
        public EmuManagerModel()
        { }

        #region properties
        public bool AutoImportRoms
        { 
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings.Get("AutoImportRoms"));
            }
            set
            {
                ConfigurationHelper.SaveConfig("AutoImportRoms", value);
                OnPropertyChanged();
            }
        }

        public bool DownloadGridImages
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings.Get("DownloadGridImages"));
            }
            set
            {
                ConfigurationHelper.SaveConfig("DownloadGridImages", value);
                OnPropertyChanged();
            }
        }

        public string ImageApiUrl
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("ImageApiUrl");
            }
            set
            {
                ConfigurationHelper.SaveConfig("ImageApiUrl", value);
                OnPropertyChanged();
            }
        }

        public string RomDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("RomDirectory");
            }
            set
            {
                ConfigurationHelper.SaveConfig("RomDirectory", value);
                OnPropertyChanged();
            }
        }

        public string EmulatorDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("EmulatorDirectory");
            }
            set
            {
                ConfigurationHelper.SaveConfig("EmulatorDirectory", value);
                OnPropertyChanged();
            }
        }

        public string RomExtensions
        {
            get
            {
                return StringHelper.CleanXmlValues(ConfigurationManager.AppSettings.Get("RomExtensions"));
            }
            set
            {
                ConfigurationHelper.SaveConfig("RomExtensions", value);
                OnPropertyChanged();
            }
        }

        public string EmulatorAssociations
        {
            get
            {
                return StringHelper.CleanXmlValues(ConfigurationManager.AppSettings.Get("EmulatorAssociations"));
            }
            set
            {
                ConfigurationHelper.SaveConfig("EmulatorAssociations", value);
                OnPropertyChanged();
            }
        }

        public string Consoles
        {
            get
            {
                return StringHelper.CleanXmlValues(ConfigurationManager.AppSettings.Get("Consoles"));
            }
            set
            {
                ConfigurationHelper.SaveConfig("Consoles", value);
                OnPropertyChanged();
            }
        }

        public string ConsoleAliases
        {
            get
            {
                return StringHelper.CleanXmlValues(ConfigurationManager.AppSettings.Get("ConsoleAliases"));
            }
            set
            {
                ConfigurationHelper.SaveConfig("ConsoleAliases", value);
                OnPropertyChanged();
            }
        }

        public string EmulatorLaunchParams
        {
            get
            {
                return StringHelper.CleanXmlValues(ConfigurationManager.AppSettings.Get("EmulatorLaunchParams"));
            }
            set
            {
                ConfigurationHelper.SaveConfig("EmulatorLaunchParams", value);
                OnPropertyChanged();
            }
        }

        public string SteamDirectory
        {
            get
            {
                return StringHelper.CleanXmlValues(ConfigurationManager.AppSettings.Get("SteamDirectory"));
            }
            set
            {
                ConfigurationHelper.SaveConfig("SteamDirectory", value);
                OnPropertyChanged();
            }
        }

        private int _romsLoadedCount;
        public string RomsLoadedCount
        {
            get
            {
                return _romsLoadedCount.ToString();
            }
            set
            {
                _romsLoadedCount = int.Parse(value);
                OnPropertyChanged();
            }
        }

        private int _emulatorsLoadedCount;
        public string EmulatorsLoadedCount
        {
            get
            {
                return _emulatorsLoadedCount.ToString();
            }
            set
            {
                _emulatorsLoadedCount = int.Parse(value);
                OnPropertyChanged();
            }
        }

        private int _consolesWithRomsCount;
        public string ConsolesWithRomsCount
        {
            get
            {
                return _consolesWithRomsCount.ToString();
            }
            set
            {
                _consolesWithRomsCount = int.Parse(value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region methods
        #endregion
    }
}
