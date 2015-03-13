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
            string shortcutsHeader = "\00shortcuts\00";

            string shortcutsBody = "";
            int shortcutNumber = 0;
            foreach (var emulator in emulators)
            {
                var query = from rom in roms
                            where rom.Console == emulator.Console
                            select rom;

                foreach(var rom in query.ToList())
                {
                    shortcutsBody += "\x0" + shortcutNumber.ToString();
                    shortcutsBody += GenerateKeyValuePair("AppName", rom.Name);
                    shortcutsBody += GenerateKeyValuePair("Exe", emulator.FullCommandLineLaunch.Replace("%g", rom.Path));
                    shortcutsBody += GenerateKeyValuePair("StartDir", emulator.StartDirectory);
                    shortcutsBody += GenerateKeyValuePair("icon", emulator.StartDirectory);
                    shortcutsBody += GenerateTags();
                    shortcutsBody += "\x01" + 0.ToString() + "\x0" + emulator.Console + "\x0\x0";
                    shortcutsBody += "\b\b";

                    shortcutNumber++;
                }
            }
            string shortcutsFooter = "\b\b\n";

            WriteShortcutFile(shortcutsHeader + shortcutsBody + shortcutsFooter);
        }

        private static string GenerateKeyValuePair(string key, string value)
        {
             return "\x01" + key + "\x0" + value + "\x0"; 
        }

        private static string GenerateTags()
        {
            return "\x0" + "tags" + "\x0" + "\x01" + 0.ToString() + "\x0" + "Powered by GURU Emulation Manager" + "\x0";
        }

        private static void WriteShortcutFile(string shortcutText)
        {
            File.WriteAllText(@"C:\Program Files (x86)\Steam\userdata\31025313\config\shortcuts.vdf", shortcutText);
        }
    }
}
