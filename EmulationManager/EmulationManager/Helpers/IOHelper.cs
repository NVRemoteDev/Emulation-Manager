using EmulationManager.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationManager.Helpers
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
        public static int EnumerateEmulators(string rootEmuDirectory)
        {
            string[] emulatorConsoleAssociations = new EmuManagerModel().EmulatorAssociations.Split(';');

            int emulatorCount = 0;
            foreach (string association in emulatorConsoleAssociations)
            {
                try
                {
                    // EX association: PS1:ePSXe.exe
                    string emulator = association.Split(':')[1];

                    string[] files = System.IO.Directory.GetFiles(rootEmuDirectory, emulator,
                        SearchOption.AllDirectories);

                    emulatorCount += files.Length;
                }
                catch (NullReferenceException)
                {
                    // This would mean an improperly formatted emulator association was present if hit
                    continue;
                }

            }

            return emulatorCount;
        }

        /// <summary>
        /// Use to enumerate ROM files from known ROM file types
        /// 
        /// Searches for the roms with extensions listed in the app.config
        /// </summary>
        /// <param name="rootRomDirectory"></param>
        /// <returns>ROM file count</returns>
        public static int EnumerateRomFiles(string rootRomDirectory)
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
        }

        /// <summary>
        /// Builds a EmulatorModel for every rom
        /// </summary>
        public static EmulatorModel[] GetEmulatorInformationFromDisk(string rootEmuDirectory)
        {
            EmulatorModel[] models = null;
            try
            {
                models = new EmulatorModel[EnumerateEmulators(rootEmuDirectory)];
            }
            catch (DirectoryNotFoundException ex)
            {
                DebugManager.ShowErrorDialog("An error occured getting the emulator directory", ex);
                return models;
            }

            string[] emulatorConsoleAssociations = new EmuManagerModel().EmulatorAssociations.Split(';');

            int i = 0;
            foreach (string association in emulatorConsoleAssociations)
            {
                try
                {
                    // EX association: PS1:ePSXe.exe
                    string emulator = association.Split(':')[1];

                    string[] files = System.IO.Directory.GetFiles(rootEmuDirectory, emulator, SearchOption.AllDirectories);
                    foreach (string file in files)
                    {
                        EmulatorModel model = new EmulatorModel();
                        model.BinaryPath = file;
                        model.Console = association.Split(':')[0];

                        models[i] = model;
                        i++;
                    }
                }
                catch (NullReferenceException)
                {
                    // This would mean an improperly formatted emulator association was present if hit
                    continue;
                }
            }

            return models;
        }

        /// <summary>
        /// Builds a RomModel for every rom
        /// </summary>
        public static RomModel[] GetRomInformationFromDisk(string rootRomDirectory)
        {
            string consoleAliases = ConfigurationManager.AppSettings.Get("ConsoleAliases");
            RomModel[] models = null;

            try
            {
                models = new RomModel[EnumerateRomFiles(rootRomDirectory)];
            }
            catch (DirectoryNotFoundException ex)
            {
                DebugManager.ShowErrorDialog("An error occured getting the rom directory", ex);
                return models;
            }

            string[] romExtensions = new EmuManagerModel().RomExtensions.Split(',');
            
            //TODO: We probably want to take a look at the nesting here
            int x = 0;
            try
            {
                foreach (string extension in romExtensions)
                {
                    string[] files = System.IO.Directory.GetFiles(rootRomDirectory, "*." + extension, SearchOption.AllDirectories);
                    for (int i = 0; i < files.Length; i++)
                    {
                        models[x] = PopulateRomModelFromRomPathRomFileName(files[i], consoleAliases);
                        x++;
                    }
                }
            }
            catch (NullReferenceException)
            {
                // This would mean an improperly formatted emulator association was present if hit
            }

            return models;
        }

        /// <summary>
        /// Populates a rom model for a given rom file and the console aliases from the config file
        /// </summary>
        /// <param name="file">Rom File (with full path)</param>
        /// <param name="consoleAliases">Console Aliases (from app.config)</param>
        /// <returns>Rom Model</returns>
        private static RomModel PopulateRomModelFromRomPathRomFileName(string file, string consoleAliases)
        {
            string[] filePathParts = file.Split('\\');

            RomModel model = new RomModel();
            model.Path = file;
            model.Name = filePathParts.Last().Split('.')[0];
            model.StreamingCompatibleName = model.Name.Replace(" ", ConfigurationHelper.GetStreamingCompatiblityReplacementName());

            string consoles = ConfigurationManager.AppSettings.Get("Consoles");
            
            // [length - 1] because the length index would be the file name
            int length = filePathParts.Length - 1;
            while (length >= 0)
            {
                string testFolder = filePathParts[length];
                if (consoles.Contains(testFolder))
                {
                    model.Console = testFolder;
                    break;
                }
                else if (consoleAliases.Contains(testFolder))
                {
                    foreach (string consoleAlias in consoleAliases.Split(';'))
                    {
                        if (consoleAlias.Contains(testFolder))
                        {
                            model.Console = consoleAlias.Split(':')[1];
                            break;
                        }
                    }
                }
                length--;
            }

            return model;
        }

        /// <summary>
        /// Changes the ROM filename to be compatible with streaming over Steam Big Picture
        /// </summary>
        public static void FixRomsForStreaming(RomModel[] roms)
        {
            foreach (var rom in roms)
            {
                if (!string.IsNullOrEmpty(rom.Path) && !string.IsNullOrEmpty(rom.StreamingCompatibleName))
                {
                    File.Move(rom.Path, rom.StreamingCompatibleName);
                }
            }
        }

        /// <summary>
        /// Reverts the FixRomsForStreaming changes
        /// </summary>
        public static void RevertRomsFromStreaming(RomModel[] roms)
        {
            foreach (var rom in roms)
            {
                if (!string.IsNullOrEmpty(rom.Path) && !string.IsNullOrEmpty(rom.StreamingCompatibleName))
                {
                    File.Move(rom.StreamingCompatibleName, rom.Path);
                }
            }
        }
    }
}
