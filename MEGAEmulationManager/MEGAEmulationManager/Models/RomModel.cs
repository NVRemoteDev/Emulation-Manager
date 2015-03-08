using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEGAEmulationManager.Models
{
    public class RomModel
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public string CleanedName { get; set; }

        public EmulatorModel Emulator { get; set; }
    }
}
