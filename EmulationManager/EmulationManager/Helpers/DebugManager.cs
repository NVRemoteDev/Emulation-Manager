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
        public static void ShowErrorDialog(string errorMessage, Exception exception)
        {
            string dialogErrorText = "";
            if (exception == null)
            {
                dialogErrorText = errorMessage;
            }
            else
            {
                dialogErrorText = errorMessage + Environment.NewLine + exception.Message;
            }

            MessageBox.Show(dialogErrorText, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
        }
    }
}
