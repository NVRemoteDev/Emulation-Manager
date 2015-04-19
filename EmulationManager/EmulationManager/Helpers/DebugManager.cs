using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmulationManager.Helpers
{
    public static class DebugManager
    {
        public static MessageBoxResult ShowErrorDialog(string errorMessage, Exception exception)
        {
            return MessageBox.Show(errorMessage + Environment.NewLine + exception.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
        }
    }
}
