using MEGAEmulationManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEGAEmulationManager.ViewModels
{
    public class EmuManagerViewModel
    {
        public EmuManagerModel EmuManagerModel { get; set; }

        public EmuManagerViewModel()
        {
            EmuManagerModel = new EmuManagerModel();
        }

    }
}
