using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFDBApp.Model;
using WPFDBApp.Services;
using WPFDBApp.Services.Commands;

namespace WPFDBApp.ViewModel.UserControls
{
    /// <summary>
    /// A class that helps to pass the server name.
    /// </summary>
    public class ServerNameSelectedEventArgs : EventArgs
    {
        public string ServerName { get; }

        public ServerNameSelectedEventArgs(string name)
        {
            ServerName = name;
        }
    }

    public class ServerBrowseWindowVM : BaseVM
    {
        #region Private Fields

        private string _selectedItem;
        private bool _canExecutOkCommand;

        #endregion

        #region Events

        public event EventHandler<ServerNameSelectedEventArgs> ServerNameSelectedMessage;
        public event EventHandler CancelMessage;

        #endregion

        #region Commands

        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }

        #endregion

        #region Constructor

        public ServerBrowseWindowVM()
        {
            OkCommand = new BaseCommand(p => SubmitButton(), p => CanExecuteOkCommand);
            CancelCommand = new BaseCommand(p => CancelButton());
        }

        #endregion

        #region Public Properties

        public string SelectedItem
        {
            get { return _selectedItem; }
            private set
            {
                OnPropertyChanged(ref _selectedItem, value);
            }
        }
        
        #endregion

        #region Commands Helper Methods

        public bool CanExecuteOkCommand
        {
            get { return _canExecutOkCommand; }
            set { OnPropertyChanged(ref _canExecutOkCommand, value); }
        }

        private void AddIsEnabledOkbtv()
        {
            if (_selectedItem != null && _selectedItem != "DataBase Engine" && _selectedItem != "DataBase Engine (loading...)")
                CanExecuteOkCommand = true;
            else
                CanExecuteOkCommand = false;
        }

        private void SubmitButton()
        {
            SelectedItem = _selectedItem;
            ServerNameSelectedMessage(this, new ServerNameSelectedEventArgs(SelectedItem));
        }

        private void CancelButton()
        {
            SelectedItem = Properties.Settings.Default.ServerName;
            ServerNameSelectedMessage(this, new ServerNameSelectedEventArgs(SelectedItem));
            CancelMessage(null, EventArgs.Empty);
        }

        #endregion

        #region Get Server Names Methods

        private static ObservableCollection<ServerInstMenuItemModel> _localServers;
        private static ObservableCollection<ServerInstMenuItemModel> _netWorkServers;

        public void GetSelectedServerName(string item)
        {
            if (item != null)
            {
                _selectedItem = item;
            }
            AddIsEnabledOkbtv();
        }

        public static ObservableCollection<ServerInstMenuItemModel> LocalServers
        {
            get
            {
                _localServers = new ObservableCollection<ServerInstMenuItemModel>{
                    new ServerInstMenuItemModel() {
                        Name = "DataBase Engine (loading...)"
                    }
                };

                Task taskA = Task.Factory.StartNew(() => GetLocalServersItems());

                return _localServers;
            }
        }

        private static void GetLocalServersItems()
        {
            _localServers[0].Items = new ObservableCollection<ServerInstMenuItemModel>();
            try
            {
                var servNames = SqlServerInstance.SqlLocalInstances;
                var items = new ObservableCollection<ServerInstMenuItemModel>();
                foreach (var name in servNames)
                {
                    items.Add(new ServerInstMenuItemModel() { Name = name });
                }
                _localServers[0].Items = items;
                _localServers[0].Name = "DataBase Engine";
            }
            catch { }
        }

        public static ObservableCollection<ServerInstMenuItemModel> NetWorkServers
        {
            get
            {
                _netWorkServers = new ObservableCollection<ServerInstMenuItemModel>{
                    new ServerInstMenuItemModel() {
                        Name = "DataBase Engine (loading...)"
                    }
                };

                Task taskA = Task.Factory.StartNew(() => GetNetWorkServersItems());

                return _netWorkServers;
            }
        }

        private static void GetNetWorkServersItems()
        {
            _netWorkServers[0].Items = new ObservableCollection<ServerInstMenuItemModel>();
            try
            {
                var servNames = SqlServerInstance.SqlNetWorkInstances;
                var items = new ObservableCollection<ServerInstMenuItemModel>();
                foreach (var name in servNames)
                {
                    items.Add(new ServerInstMenuItemModel() { Name = name });
                }
                _netWorkServers[0].Items = items;
                _netWorkServers[0].Name = "DataBase Engine";
            }
            catch { }
        }

        #endregion

        #region Helper Methods

        public override string ToString()
        {
            if (SelectedItem != null)
                return this.SelectedItem;
            return "";
        }

        #endregion
    }
}
