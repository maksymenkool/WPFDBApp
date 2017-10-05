using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using WPFDBApp.Services;
using WPFDBApp.View;
using WPFDBApp.ViewModel;
using WPFDBApp.ViewModel.UserControls;

namespace WPFDBApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Logger

        private static readonly log4net.ILog LOG = Logger.For(typeof(MainWindow));

        #endregion

        #region Private Fields

        private MainWindowVM _viewModel;

        #endregion

        #region Constructor

        public MainWindow(MainWindowVM viewmodel)
        {
            _viewModel = viewmodel;

            InitializeComponent();

            this.Width = SystemParameters.PrimaryScreenWidth - 10;
            this.Height = SystemParameters.PrimaryScreenHeight - 50;
            double workHeight = SystemParameters.WorkArea.Height;
            double workWidth = SystemParameters.WorkArea.Width;
            this.Top = (workHeight - this.Height) / 2;
            this.Left = (workWidth - this.Width) / 2;

            DataContext = _viewModel;
            _viewModel.ConnectMessage += ConnectMessageHandler;
            _viewModel.AboutMessage += AboutMessageHandler;

            Logger.Setup();
            LOG.Info("Application Starting.");
            Logger.StatusChanged();
            Closing += OnClosing;
        }

        #endregion

        #region Helper Methods

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var stream = File.OpenRead(Convert.ToString("./Resources/sql.xshd")))
            {
                using (var reader = new System.Xml.XmlTextReader(stream))
                {
                    MyAvalonEdit.SyntaxHighlighting =
                        ICSharpCode.AvalonEdit.Highlighting.Xshd.HighlightingLoader.Load(reader,
                        ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance);
                }
            }
        }

        internal void AboutMessageHandler(object sender, EventArgs e)
        {
            var aboutWindow = new AboutWindow();
            aboutWindow.Owner = this;
            aboutWindow.ShowInTaskbar = false;
            aboutWindow.ShowDialog();
        }

        internal void ConnectMessageHandler(object sender, EventArgs e)
        {
            var connectVM = new ConnectWindowVM();
            var connectWindow = new ConnectWindow(connectVM);
            connectVM.ServerConnectionMessage += _viewModel.ServerConnectionGetMessageHandler;
            connectWindow.Owner = this;
            connectWindow.ShowInTaskbar = false;
            LOG.Info("The Connect window is loaded.");
            connectWindow.ShowDialog();
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            LOG.Info("The application was closed.");
            Closing -= OnClosing;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var response = MessageBox.Show("Do you really want to exit?", "Exiting...",
                                           MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                if (_viewModel.CanSaveProject == true)
                {
                    var saveProgect = MessageBox.Show("Do you want to save this progect?", "Saving...",
                                               MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if (saveProgect == MessageBoxResult.Yes)
                    {
                        _viewModel.SaveProjectAs(new object[] { this.TreeViewUC.TreeRootNode, this.TreeViewUC.TreeViewRootItem });
                    }
                }
                System.Threading.Thread.Sleep(750);
                Properties.Settings.Default.IsProjectOpening = false;
                Properties.Settings.Default.ConnectionString = null;
                Properties.Settings.Default.Save();
                Application.Current.Shutdown();
            }
        }

        #endregion
    }
}
