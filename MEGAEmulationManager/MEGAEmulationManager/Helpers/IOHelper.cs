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
        public async static Task<int> EnumerateDirectories(string directory)
        {
            return await Task.Run(() =>
            {
                var directories = Directory.EnumerateDirectories(directory);

                int folderCount = 0;
                foreach (var dir in directories)
                {
                    folderCount++;
                }
                return folderCount;
            });
        }

        /// <summary>
        /// Use to enumerate ROM files from known ROM file types
        /// </summary>
        /// <param name="directory"></param>
        /// <returns>ROM file count</returns>
        public async static Task<int> EnumerateRomFiles(string directory)
        {
            return await Task.Run(() =>
            {
                var directories = Directory.EnumerateFiles(directory);

                int folderCount = 0;
                foreach (var dir in directories)
                {
                    folderCount++;
                }
                return folderCount;
            });
        }
    }
}
