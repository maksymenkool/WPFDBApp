using System;
using System.ComponentModel;
using System.Windows;
using WPFDBApp.Services;
using WPFDBApp.ViewModel.UserControls;

namespace WPFDBApp.View
{
    public interface IHavePassword
    {
        System.Security.SecureString Password { get; }
    }

    /// <summary>
    /// Interaction logic for ConnectWindow.xaml
    /// </summary>
    public partial class ConnectWindow : Window, IHavePassword
    {
        #region Logger

        private static readonly log4net.ILog LOG = Logger.For(typeof(ConnectWindow));

        #endregion

        #region Private Fields

        private ConnectWindowVM _viewModel;

        #endregion

        #region Constructor

        public ConnectWindow(ConnectWindowVM vm)
        {
            _viewModel = vm;

            InitializeComponent();

            DataContext = _viewModel;

            _viewModel.ServerBrowseMessage += BrowseServersMessageHandler;
            _viewModel.ServerConnectionMessage += ServerConnectionMessageHandler;
            _viewModel.CancelMessage += CancelMessageHandler;

            Closing += OnClosing;
        }

        #endregion

        #region Public Properties

        public System.Security.SecureString Password
        {
            get
            {
                return passwordBox.SecurePassword;
            }
        }

        #endregion

        #region Helper Methods

        private void BrowseServersMessageHandler(object sender, EventArgs e)
        {
            var serverBrowseVM = new ServerBrowseWindowVM();
            var serverBrowseWindow = new ServerBrowseWindow(serverBrowseVM);
            serverBrowseVM.ServerNameSelectedMessage += _viewModel.ServerNameSelectedMessageHandler;
            serverBrowseWindow.Owner = this;
            serverBrowseWindow.ShowInTaskbar = false;
            LOG.Info("The ServerBrowse window is loaded.");
            serverBrowseWindow.ShowDialog();
        }

        internal void ServerConnectionMessageHandler(object sender, SqlStringCreatorEventArgs e)
        {
            this.Close();
        }

        private void CancelMessageHandler(object sender, EventArgs e)
        {
            LOG.Info("The connection was canceled.");
            this.Close();
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            LOG.Info("The Connect window was closed.");
            Logger.StatusChanged();
            Closing -= OnClosing;
        }

        #endregion
    }
}
