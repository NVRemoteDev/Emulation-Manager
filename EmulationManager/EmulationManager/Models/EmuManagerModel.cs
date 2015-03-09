using EmulationManager.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationManager.Models
{
    public class EmuManagerModel : INotifyPropertyChanged
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
                OnPropertyChanged("AutoImportRoms");
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
                OnPropertyChanged("DownloadGridImages");
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
                OnPropertyChanged("ImageApiUrl");
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
                OnPropertyChanged("RomDirectory");
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
                OnPropertyChanged("EmulatorDirectory");
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
                OnPropertyChanged("RomExtensions");
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
                OnPropertyChanged("EmulatorAssociations");
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
                OnPropertyChanged("Consoles");
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
                OnPropertyChanged("ConsoleAliases");
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
                OnPropertyChanged("RomsLoadedCount");
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
                OnPropertyChanged("EmulatorsLoadedCount");
            }
        }
        #endregion

        #region methods
        #endregion

        #region INotifyPropertyChanged Member
        // Create the OnPropertyChanged method to raise the event 
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion
    }
}
