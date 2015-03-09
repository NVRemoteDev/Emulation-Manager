using EmulationManager.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationManager.Models
{
    public class EmulatorModel
    {
        /// <summary>
        /// Name of the emulator
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Path to the emulator executable
        /// </summary>
        public string BinaryPath { get; set; }

        /// <summary>
        /// Console this emulator is for (alias)
        /// </summary>
        public string Console { get; set; }

        /// <summary>
        /// Emulator launch options (include game)
        /// 
        /// Returns %r which should be should be substituted for the rom binary full path upon launch
        /// </summary>
        public string LaunchParams
        {
            get
            {
                if (!string.IsNullOrEmpty(BinaryPath))
                {
                    string options = StringHelper.CleanXmlValues(ConfigurationManager.AppSettings.Get("EmulatorLaunchParams"));
                    string[] optionsArray = options.Split(';');

                    string launchOption = "";
                    foreach (var option in optionsArray)
                    {
                        if (option.Contains(Name))
                        {
                            launchOption = option.Split(':')[1];
                        }
                    }

                    return launchOption;
                }
                return "";
            }
            set
            {
                ConfigurationHelper.SaveConfig("ConsoleAliases", value);
            }
        }

        public string FullCommandLineLaunch
        {
            get
            {
                return BinaryPath + " " + LaunchParams;
            }
        }
    }
}
