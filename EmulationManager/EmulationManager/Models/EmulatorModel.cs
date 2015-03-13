using EmulationManager.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
        /// Emulator directory
        /// </summary>
        public string StartDirectory
        {
            get
            {
                if (!string.IsNullOrEmpty(BinaryPath))
                {
                    return Path.GetDirectoryName(BinaryPath);
                }
                return "";
            }
        }

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
                    string[] launchParams = StringHelper.CleanXmlValues(ConfigurationManager.AppSettings.Get("EmulatorLaunchParams")).Split(';');
                    for (int i = 0; i < launchParams.Length; i++)
                    {
                        if (launchParams[i].Contains(Console))
                        {
                            return launchParams[i].Split(':')[1];
                        }
                    }
                }
                return "";
            }
        }


        public string FullCommandLineLaunch
        {
            get
            {
                string launchParams = "";
                launchParams = LaunchParams.Replace("%g", "\"%g\"");
                return "\"" + BinaryPath + "\"" + " " + launchParams;
            }
        }
    }
}
