using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmulationManager.Models
{
    public class RomModel
    {
        /// <summary>
        /// Rom Name (Original formatting)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Full path to rom binary
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Full path to rom binary (fixed for streaming)
        /// </summary>
        public string StreamingCompatiblePath { get; set; }

        /// <summary>
        /// Grid image url (ie consolegrid.com steam image)
        /// </summary>
        public string GridImageUrl { get; set; }

        /// <summary>
        /// What console this rom is for
        /// </summary>
        public string Console { get; set; }

        /// <summary>
        /// Whether to use the streaming compatible path or not
        /// 
        /// True if the roms have been renamed
        /// </summary>
        public bool UseStreamingCompatiblePath { get; set; }
    }
}
