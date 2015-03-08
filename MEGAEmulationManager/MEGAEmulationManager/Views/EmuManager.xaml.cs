using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ookii.Dialogs.Wpf;

namespace MEGAEmulationManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseRomDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new VistaFolderBrowserDialog();
            if (dialog.ShowDialog() == true)
            {
                RomDirectoryTextBox.Text = dialog.SelectedPath;
            }
        }

        private void BrowseEmulatorDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new VistaFolderBrowserDialog();
            if (dialog.ShowDialog() == true)
            {
                EmulatorDirectoryTextBox.Text = dialog.SelectedPath;
            }
        }

        private void RootRomsHelp_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // The whitespace is important here.
            string helpText = @"The root roms directory should be just above all of your categorized rom folders. 

Example:
--C:\Roms
--C:\Roms\N64
--C:\Roms\Playstation 
--C:\Roms\Gamecube

Enter 'C:\Roms' (trailing slash optional)";
            
            string caption = "Root Roms Directory Help";

            ShowHelpDialog(helpText, caption);
        }

        private void RootEmulatorsHelp_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // The whitespace is important here.
            string helpText = @"The root emulator directory should be just above all of your categorized emulator folders. 

Example:
--C:\Emulators
--C:\Emulators\N64
--C:\Emulators\Playstation 
--C:\Emulators\Gamecube

Enter 'C:\Emulators' (trailing slash optional)";

            string caption = "Root Emulators Directory Help";

            ShowHelpDialog(helpText, caption);
        }

        public MessageBoxResult ShowHelpDialog(string helpText, string caption)
        {
            return MessageBox.Show(helpText, caption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
        }

        private void ShowHideAdvancedSettingsTextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (AdvancedSettingsValues.Visibility == Visibility.Collapsed)
            {
                AdvancedSettingsValues.Visibility = Visibility.Visible;
            }
            else
            {
                AdvancedSettingsValues.Visibility = Visibility.Collapsed;
            }
        }
    }
}
