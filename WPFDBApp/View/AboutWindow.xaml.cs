using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace WPFDBApp.View
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();

            Closing += OnClosing;
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            Closing -= OnClosing;
        }

        private void DoNotShowCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (DoNotShowCheckBox.IsChecked == true)
            {
                Properties.Settings.Default.DoNotShowHelpWindow = true;
                Properties.Settings.Default.Save();
            }
        }

        private void DoNotShowCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.DoNotShowHelpWindow = false;
            Properties.Settings.Default.Save();
        }
    }
}
