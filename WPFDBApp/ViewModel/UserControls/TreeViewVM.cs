using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using TreeStruct;
using WPFDBApp.Services.Commands;
using WPFDBApp.Services.TreeServices;

namespace WPFDBApp.ViewModel.UserControls
{
    public class TreeViewVM : BaseVM
    {
        #region Private Fields

        private TreeViewItemVM _rootItem;
        private TreeNode<Element> _rootNode;

        private ObservableCollection<TreeViewItemVM> _items;
        private TreeViewItemVM _selectedItem;
        private string _sqlScript;
        private string _searchItem = string.Empty;
        private IEnumerator<TreeViewItemVM> _itemsEnumerator;
        readonly ICommand _searchCommand;

        #endregion

        #region Commands

        public ICommand SearchCommand
        {
            get { return _searchCommand; }
        }


        #endregion

        #region Constructor

        public TreeViewVM(TreeNode<Element> rootNode)
        {
            TreeViewItemVM.OnItemSelected += TreeViewItemSelected;
            TreeViewItemVM.OnSqlScriptCreated += SqlScriptCreated;
            
            _searchCommand = new SearchTreeItemCommand(this);

            this.RootNode = rootNode;
            this._rootItem = new TreeViewItemVM(rootNode, TreeItemType.Server);
            this.Items = new ObservableCollection<TreeViewItemVM>(new List<TreeViewItemVM> { _rootItem });
        }

        #endregion

        #region Public Properties

        public ObservableCollection<TreeViewItemVM> Items
        {
            get { return _items; }
            set
            {
                OnPropertyChanged(ref _items, value);
            }
        }

        public TreeViewItemVM SelectedItem_
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem_));
            }
        }

        public TreeNode<Element> RootNode
        {
            get { return _rootNode; }
            set
            {
                _rootNode = value;
                OnPropertyChanged(nameof(RootNode));
            }
        }

        public string SqlScript
        {
            get { return _sqlScript; }
            set
            {
                _sqlScript = value;
                OnPropertyChanged(nameof(SqlScript));
            }
        }

        public string SearchItem
        {
            get { return _searchItem; }
            set
            {
                if (ReferenceEquals(_searchItem, value))
                    return;

                _searchItem = value;
                OnPropertyChanged(nameof(SearchItem));
                _itemsEnumerator = null;
            }
        }

        #endregion

        #region Helper Methods

        private void SqlScriptCreated(string script)
        {
            SqlScript = script;
        }

        private void TreeViewItemSelected(TreeViewItemVM item)
        {
            SelectedItem_ = item;
        }

        #endregion


        #region Commands Helper Methods

        #region Search Logic

        private class SearchTreeItemCommand : ICommand
        {
            readonly TreeViewVM _tree;

            public SearchTreeItemCommand(TreeViewVM tree)
            {
                _tree = tree;
            }

            public bool CanExecute(object parameter)
            {
                //return true;
                return string.IsNullOrEmpty(_tree.SearchItem);
            }

            event EventHandler ICommand.CanExecuteChanged
            {
                add { }
                remove { }
            }

            public void Execute(object parameter)
            {
                _tree.Search();
            }
        }

        private bool CanSearch
        {
            get { return !string.IsNullOrEmpty(SearchItem); }
        }

        void Search()
        {
            if (_itemsEnumerator == null || !_itemsEnumerator.MoveNext())
                this.VerifyMatchingItemEnumerator();

            var item = _itemsEnumerator.Current;

            if (item == null)
                return;

            if (item.Parent != null)
                item.Parent.IsExpanded = true;

            item.IsSelected = true;
        }

        void VerifyMatchingItemEnumerator()
        {
            var matches = this.FindItem(SearchItem, _rootItem);
            _itemsEnumerator = matches.GetEnumerator();

            if (!_itemsEnumerator.MoveNext())
            {
                MessageBox.Show("No matching items were found.", "Try Again", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        IEnumerable<TreeViewItemVM> FindItem(string searchText, TreeViewItemVM item)
        {
            if (item.NameContainsText(searchText))
                yield return item;

            foreach (var child in item.Children)
                foreach (var match in FindItem(searchText, child))
                    yield return match;

        }

        #endregion //Search Logic

        #endregion

    }
}
