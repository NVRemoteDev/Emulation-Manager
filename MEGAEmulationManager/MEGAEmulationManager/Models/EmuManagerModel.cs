using MEGAEmulationManager.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEGAEmulationManager.Models
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

        public string RomsLoadedCount
        {
            get
            {
                if (string.IsNullOrEmpty(RomsLoadedCount))
                {
                    return "0";
                }
                return RomsLoadedCount;
            }
            set
            {
                RomsLoadedCount = value;
                OnPropertyChanged("RomsLoadedCount");
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
