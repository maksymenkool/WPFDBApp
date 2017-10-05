using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFDBApp.Services;
using WPFDBApp.Services.Commands;
using WPFDBApp.View;
using WPFDBApp.View.UserControls;

namespace WPFDBApp.ViewModel.UserControls
{
    public class ConnectWindowVM : BaseVM
    {
        #region Logger

        private static readonly log4net.ILog LOG = Logger.For(typeof(ConnectWindowVM));

        #endregion

        #region Constants

        const string BROWSE_SERVER_NAME = " Browse for more... ";

        #endregion

        #region Private Fields

        private SqlConnection connection;
        private string connectionString;
        private bool _cancelConnect;
        private bool _lockConnectButton;

        private string _selectedServer;
        private string _editedServerName;
        private bool _isAuth;
        private string _userName;
        private string _userPassword;

        private ObservableCollection<string> _observableServers;

        #endregion

        #region Events

        public static event TreeViewItemsHandler OnServerConnectCreating;

        public event EventHandler<SqlStringCreatorEventArgs> ServerConnectionMessage;

        public event EventHandler ServerBrowseMessage;
        public event EventHandler CancelMessage;

        #endregion

        #region Commands

        public ICommand CancelCommand { get; }
        public ICommand ServerConnectCommand { get; }

        #endregion

        #region Settings action

        private void WriteServerNameSetting(string servername, string username)
        {
            Properties.Settings.Default.ServerName = servername;
            Properties.Settings.Default.UserName = username;
            Properties.Settings.Default.Save();
        }

        private void ReadServerNameSetting()
        {
            _selectedServer = Properties.Settings.Default.ServerName;
            _editedServerName = Properties.Settings.Default.ServerName;
            _userName = Properties.Settings.Default.UserName;
        }
        #endregion

        #region Constructor

        public ConnectWindowVM()
        {
            ReadServerNameSetting();
            var servers = new List<string> { SelectedServer, BROWSE_SERVER_NAME };

            _observableServers = new ObservableCollection<string>(servers);
            CancelCommand = new BaseCommand(p => CancelButton());
            ServerConnectCommand = new BaseCommand(async p => await ServerConnectAsync(p), p => CanConnect);
        }

        #endregion

        #region Public Properties

        public ICollection<string> Servers
        {
            get { return _observableServers; }
        }

        public string EditedServerName
        {
            get { return _editedServerName; }
            set
            {
                _editedServerName = value;
                SelectedServer = _editedServerName;
                OnPropertyChanged(nameof(SelectedServer));
            }
        }

        public string SelectedServer
        {
            get { return _selectedServer; }
            set
            {
                if (_selectedServer == value)
                    return;
                _selectedServer = value;
                if (_selectedServer == BROWSE_SERVER_NAME)
                {
                    ServerBrowseMessage(null, EventArgs.Empty);
                }
                OnPropertyChanged(nameof(SelectedServer));
            }
        }

        public bool IsAuth
        {
            get { return _isAuth; }
            set
            {
                if (value != _isAuth)
                {
                    OnPropertyChanged(ref _isAuth, value);
                }
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                OnPropertyChanged(ref _userName, value);
            }
        }

        public string UserPassword
        {
            get { return _userPassword; }
            set
            {
                OnPropertyChanged(ref _userPassword, value);
            }
        }

        public bool CanConnect
        {
            get
            {
                var canConnect = true;
                if (_lockConnectButton == true)
                {
                    canConnect = false;
                }
                else if (IsAuth == true)
                {
                    if (!String.IsNullOrEmpty(_userName) || !String.IsNullOrEmpty(_userPassword))
                        canConnect = true;
                    else
                        canConnect = false;
                }
                return canConnect;
            }
        }

        #endregion

        #region Commands Helper Methods

        public void ServerNameSelectedMessageHandler(object sender, ServerNameSelectedEventArgs e)
        {
            _observableServers[0] = e.ServerName;
            _selectedServer = e.ServerName;

            OnPropertyChanged(nameof(SelectedServer));
            OnPropertyChanged(nameof(Servers));
        }

        private async Task ServerConnectAsync(object parameter)
        {
            GetUserPassword(parameter);
            if (true == SelectConnectFormData())
            {
                try
                {
                    LOG.Info(String.Format("Attempt to connect to the server: {0}.", SelectedServer));
                    Logger.StatusChanged(String.Format("Attempt to connect to the server: {0}.", SelectedServer), StatusBackgroundColor.Orange);
                    _cancelConnect = false;
                    _lockConnectButton = true;
                    await GetOpenDataConnectionAsync(IsAuth);
                    Logger.StatusChanged(String.Format("Connection to server: {0} succeeded.", SelectedServer));
                    LOG.Info(String.Format("Connection to server: {0} succeeded.", SelectedServer));
                    MessageBox.Show("Connection succeeded.", "Connection...", MessageBoxButton.OK, MessageBoxImage.Information);
                    ServerConnectionMessage(this, new SqlStringCreatorEventArgs(connectionString));
                    WriteServerNameSetting(SelectedServer, UserName);
                    OnServerConnectCreating(SelectedServer);
                    Logger.StatusChanged();
                }
                catch (Exception ex)
                {
                    if (_cancelConnect == false)
                    {
                        Logger.StatusChanged(String.Format("Connection is failed: \n{0}.", ex.Message), StatusBackgroundColor.Orange);
                        MessageBox.Show(String.Format("Connection is failed: \n{0}", ex.Message), "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        LOG.Warn(String.Format("Connection is failed: \n{0}.", ex.Message, ex.StackTrace));
                    }
                }
                finally
                {
                    _lockConnectButton = false;
                    Logger.StatusChanged();
                }
            }
        }

        private void CancelButton()
        {
            _cancelConnect = true;
            ServerConnectionMessage(this, new SqlStringCreatorEventArgs(null));

            CancelMessage(null, EventArgs.Empty);
        }

        private bool SelectConnectFormData()
        {
            if (!String.IsNullOrEmpty(SelectedServer))
            {
                if (false == IsAuth)
                    return true;
                else if (true == IsAuth && !String.IsNullOrEmpty(UserName) && !String.IsNullOrEmpty(UserPassword))
                    return true;
            }
            MessageBox.Show("You did not specify all the data!\nCheck the entered data", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        private void GetUserPassword(object parameter)
        {
            var passwordContainer = parameter as IHavePassword;
            if (passwordContainer != null)
            {
                var secureString = passwordContainer.Password;
                UserPassword = ConvertToUnsecureString(secureString);
            }
        }

        private string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        #endregion

        #region Get Connection

        private async Task GetOpenDataConnectionAsync(bool auth)
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = (SelectedServer.IndexOf(":") > -1) ? SelectedServer.Replace(":", ",") : SelectedServer;
            if (true == auth)
            {
                builder.IntegratedSecurity = false;
                builder.UserID = UserName;
                builder.Password = UserPassword;
            }
            else
            {
                builder.IntegratedSecurity = true;
            }
            connectionString = builder.ToString();

            using (connection = new SqlConnection())
            {

                connection.ConnectionString = connectionString;
                await connection.OpenAsync();
            }
        }

        #endregion
    }
}
