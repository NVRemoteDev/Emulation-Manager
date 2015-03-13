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
                    shortcutsBody += "\00" + shortcutNumber.ToString() + "\01AppName\00" + rom.Name + "\00";
                    shortcutsBody += "\01Exe\00" + emulator.FullCommandLineLaunch.Replace("%g", rom.Path) + "\00";
                    shortcutsBody += "\01StartDir\00" + emulator.StartDirectory + "\00";
                    shortcutsBody += "\01icon\00\00";
                    shortcutsBody += "\00tags\00\01" + 0.ToString() + "\00" + "Powered by GURU Emulation Manager";
                    shortcutsBody += "\01" + 0.ToString() + "\00" + emulator.Console + "\00\00";
                    shortcutsBody += "\b\b";

                    shortcutNumber++;
                }
            }
            string shortcutsFooter = "\b\b";

            WriteShortcutFile(shortcutsHeader + shortcutsBody + shortcutsFooter);
        }

        private static void WriteShortcutFile(string shortcutText)
        {
            File.WriteAllText(@"C:\Program Files (x86)\Steam\userdata\31025313\config\shortcuts.vdf", shortcutText);
        }
    }
}
