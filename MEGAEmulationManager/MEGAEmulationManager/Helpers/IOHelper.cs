using MEGAEmulationManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEGAEmulationManager.Helpers
{
    public static class IOHelper
    {
        /// <summary>
        /// Use to enumerate Emulator files from known Emulator exes
        /// 
        /// Searches for the emulator binary listed in the app.config
        /// </summary>
        /// <param name="directory"></param>
        /// <returns>Emulator count</returns>
        public async static Task<int> EnumerateEmulators(string rootEmuDirectory)
        {
            return await Task.Run(() =>
            {
                string[] emulatorConsoleAssociations = new EmuManagerModel().EmulatorAssociations.Split(';');

                int emulatorCount = 0;
                foreach (string association in emulatorConsoleAssociations)
                {
                    try
                    {
                        // EX association: PS1:ePSXe.exe
                        string emulator = association.Split(':')[1];

                        string[] files = System.IO.Directory.GetFiles(rootEmuDirectory, emulator, SearchOption.AllDirectories);

                        emulatorCount += files.Length;
                    }
                    catch (NullReferenceException)
                    {
                        // This would mean an improperly formatted emulator association was present if hit
                        continue;
                    }
                }

                return emulatorCount;
            });
        }

        /// <summary>
        /// Use to enumerate ROM files from known ROM file types
        /// 
        /// Searches for the roms with extensions listed in the app.config
        /// </summary>
        /// <param name="rootRomDirectory"></param>
        /// <returns>ROM file count</returns>
        public async static Task<int> EnumerateRomFiles(string rootRomDirectory)
        {
            return await Task.Run(() =>
            {
                var romExtensionsCSV = new EmuManagerModel().RomExtensions;

                string[] romExtensions = romExtensionsCSV.Split(',');

                int romCount = 0;
                foreach (string extension in romExtensions)
                {
                    string[] files = System.IO.Directory.GetFiles(rootRomDirectory, "*." + extension, SearchOption.AllDirectories);

                    romCount += files.Length;
                }
                
                return romCount;
            });
        }
    }
}
