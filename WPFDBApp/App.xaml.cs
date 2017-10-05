using System.Windows;
using WPFDBApp.Properties;
using WPFDBApp.ViewModel;

namespace WPFDBApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindowVM mainvm = new MainWindowVM();
            MainWindow window = new MainWindow(mainvm);
            this.MainWindow = window;
            this.MainWindow.Show();

            if (Settings.Default.DoNotShowHelpWindow == false)
            {
                Settings.Default.AboutCheckBoxIsEnabled = true;
                Settings.Default.Save();
                window.AboutMessageHandler(null, System.EventArgs.Empty);
                Settings.Default.AboutCheckBoxIsEnabled = false;
                Settings.Default.Save();
            }
        }
    }
}
