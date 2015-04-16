using EmulationManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationManager.Helpers
{
    public static class SteamHelper
    {
        public static void WriteSteamShortcuts(RomModel[] roms, EmulatorModel[] emulators)
        {
            string shortcutsHeader = "\x0shortcuts\x0";

            string shortcutsBody = "";
            int shortcutNumber = 0;
            foreach (var emulator in emulators)
            {
                var query = from rom in roms
                            where rom.Console == emulator.Console
                            select rom;

                foreach(var rom in query.ToList())
                {
                    shortcutsBody += "\x0" + shortcutNumber.ToString() + "\x0";
                    shortcutsBody += GenerateKeyValuePair("AppName", rom.Name);
                    shortcutsBody += GenerateKeyValuePair("Exe", emulator.FullCommandLineLaunch.Replace("%g", rom.Path));
                    shortcutsBody += GenerateKeyValuePair("StartDir", emulator.StartDirectory);
                    shortcutsBody += GenerateKeyValuePair("icon", "");
                    shortcutsBody += GenerateTags(emulator.Console);
                    shortcutsBody += "\b\b";

                    shortcutNumber++;
                }
            }
            string shortcutsFooter = "\b\b";

            WriteShortcutFile(shortcutsHeader + shortcutsBody + shortcutsFooter);
        }

        private static string GenerateKeyValuePair(string key, string value)
        {
             return "\x01" + key + "\x0" + value + "\x0"; 
        }

        private static string GenerateTags(string console)
        {
            return "\x0" + "tags" + "\x0" + "\x01" + 0.ToString() + "\x0" + console + "\x0";
        }

        private static void WriteShortcutFile(string shortcutText)
        {
            string steamDirectory = new EmuManagerModel().SteamDirectory;

            steamDirectory = steamDirectory + @"\userdata\";
            foreach (var directory in Directory.EnumerateDirectories(steamDirectory))
            {
                File.WriteAllText(directory + @"\config\shortcuts.vdf", shortcutText);
            }
        }
    }
}
