using System.Collections.ObjectModel;
using WPFDBApp.ViewModel;

namespace WPFDBApp.Model
{
    public class ServerInstMenuItemModel : BaseVM
    {
        #region Private Fields

        private ObservableCollection<ServerInstMenuItemModel> _items;
        private string _name;

        #endregion

        #region Public Properties

        public string Name
        {
            get { return _name; }
            set
            {
                OnPropertyChanged(ref _name, value);
            }
        }

        public ObservableCollection<ServerInstMenuItemModel> Items
        {
            get { return _items; }
            set
            {
                OnPropertyChanged(ref _items, value);
            }
        }

        #endregion
    }
}
