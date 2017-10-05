using System;
using System.ComponentModel;
using System.Windows;
using WPFDBApp.Model;
using WPFDBApp.Services;
using WPFDBApp.ViewModel.UserControls;

namespace WPFDBApp.View
{
    /// <summary>
    /// Interaction logic for ServerBrowseWindow.xaml
    /// </summary>
    public partial class ServerBrowseWindow : Window
    {
        #region Logger

        private static readonly log4net.ILog LOG = Logger.For(typeof(ServerBrowseWindow));

        #endregion

        #region Private Fields

        private ServerBrowseWindowVM _viewModel;

        #endregion

        #region Constructor

        public ServerBrowseWindow(ServerBrowseWindowVM vm)
        {
            _viewModel = vm;

            InitializeComponent();

            _viewModel.ServerNameSelectedMessage += ServerNameSelectedMessageHandler;
            _viewModel.CancelMessage += CancelMessageHandler;

            Closing += OnClosing;

            DataContext = _viewModel;
        }

        #endregion

        #region Helper Methods

        private void TreeView_Selected(object sender, RoutedPropertyChangedEventArgs<Object> e)
        {
            ServerInstMenuItemModel selectedItem = e.NewValue as ServerInstMenuItemModel;
            _viewModel.GetSelectedServerName(selectedItem.Name);
        }

        private void ServerNameSelectedMessageHandler(object sender, ServerNameSelectedEventArgs e)
        {
            LOG.Info("The Server name was selected");
            this.Close();
        }

        private void CancelMessageHandler(object sender, EventArgs e)
        {
            LOG.Info("Select server name was canceled");
            this.Close();
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            LOG.Info("The ServerBrowse window was closed.");
            Closing -= OnClosing;
        }

        #endregion
    }
}
