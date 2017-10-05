using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TreeStruct;
using WPFDBApp.Services;
using WPFDBApp.Services.Commands;
using WPFDBApp.Services.TreeServices;

namespace WPFDBApp.ViewModel.UserControls
{
    public delegate void TreeviewItemSelectedHandler(TreeViewItemVM item);
    public delegate void SqlScriptCreatedHandler(string script);

    public class TreeViewItemVM : BaseVM
    {
        #region Logger

        private static readonly log4net.ILog LOG = Logger.For(typeof(TreeViewItemVM));

        #endregion

        #region Constants

        const string IS_SELECTED = "is_selected";
        const string IS_EXPANDED = "is_expanded";
        const string SQL_SCRIPT = "sql_script";

        #endregion

        #region Events

        public static event TreeviewItemSelectedHandler OnItemSelected;
        public static event SqlScriptCreatedHandler OnSqlScriptCreated;

        #endregion

        #region Private Fields

        private static readonly HashSet<TreeItemType> _nonExpandedTypes = new HashSet<TreeItemType>()
        {
            TreeItemType.Dummy, TreeItemType.PrimaryKey, TreeItemType.ForeignKey,
            TreeItemType.Column, TreeItemType.DefaultConstraint, TreeItemType.CheckConstraint,
            TreeItemType.Parameter, TreeItemType.Index, TreeItemType.View
        };

        private static readonly HashSet<TreeItemType> _nonScriptCreatedItems = new HashSet<TreeItemType>()
        {
            TreeItemType.Server, TreeItemType.DataBases, TreeItemType.Folder, TreeItemType.Dummy,
            TreeItemType.Columns, TreeItemType.Keys, TreeItemType.Indexes, TreeItemType.Constraints,
            TreeItemType.PrimaryKey, TreeItemType.ForeignKey, TreeItemType.Column, TreeItemType.Index,
            TreeItemType.DefaultConstraint, TreeItemType.CheckConstraint, TreeItemType.Parameter
        };

        private static readonly Dictionary<string, TreeItemType> _dbTypes = new Dictionary<string, TreeItemType>
        {
            { DBConstants.DATABASES, TreeItemType.DataBases }, { DBConstants.DATABASE, TreeItemType.DataBase},
            { DBConstants.TABLE, TreeItemType.Table }, { DBConstants.VIEW, TreeItemType.View },
            { DBConstants.PROCEDURE, TreeItemType.Procedure }, { DBConstants.PARAMETER, TreeItemType.Parameter },
            { DBConstants.COLUMNS, TreeItemType.Columns }, { DBConstants.COLUMN, TreeItemType.Column },
            { DBConstants.KEYS, TreeItemType.Keys }, { DBConstants.FOREIGN_KEY, TreeItemType.ForeignKey },
            { DBConstants.PRIMARY_KEY, TreeItemType.PrimaryKey }, { DBConstants.DEFAULT_CONSTRAINT, TreeItemType.DefaultConstraint },
            { DBConstants.CHECK_CONSTRAINT, TreeItemType.CheckConstraint }, { DBConstants.INDEXES, TreeItemType.Indexes },
            { DBConstants.INDEX, TreeItemType.Index }
        };

        private ObservableCollection<TreeViewItemVM> _children;
        private TreeViewItemVM _parent;
        private TreeNode<Element> _element;
        private bool _isSelected;
        private bool _isExpanded;
        private bool _isBusy;
        private bool _notConnect;
        private string _sqlScript;

        #endregion

        #region Commands

        public ICommand CreateSQLCommand { get; }
        public ICommand RefreshCommand { get; }

        #endregion

        #region Constructor

        public TreeViewItemVM(TreeNode<Element> elem, TreeItemType type, TreeViewItemVM parent)
        {
            _element = elem;
            _parent = parent;
            this.Type = type;
            this.ClearChildren();
            GetIsExpandedIsSelected();

            CreateSQLCommand = new BaseCommand(async p => await CreateSQLScriptAsync(), p => CanCreateSQLScript);
            RefreshCommand = new BaseCommand(async p => await RefreshItemAsync());
        }

        public TreeViewItemVM(TreeNode<Element> elem, TreeItemType type)
            : this(elem, type, null)
        { }

        #endregion

        #region Public Properties

        public TreeViewItemVM Parent
        {
            get { return _parent; }
        }

        public ObservableCollection<TreeViewItemVM> Children
        {
            get { return _children; }
            set { OnPropertyChanged(ref _children, value); }
        }

        public TreeItemType Type { get; set; }

        public string SqlScript
        {
            get { return _sqlScript; }
            set
            {
                _sqlScript = value;
            }
        }

        public TreeNode<Element> Element
        {
            get { return _element; }
            set { _element = value; }
        }

        public string Name
        {
            get { return this.ToString(); }
        }

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                Element.Data.Attributes[IS_SELECTED] = value ? "true" : "false";
                if (value != _isSelected)
                {
                    _isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
                if (_isSelected == true)
                    OnItemSelected(this);
            }
        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    if (value == true)
                    {
                        this.Children.Clear();
                        var a = ExpandAsync();
                        a.Wait();
                    }

                    _isExpanded = value;

                    if (_notConnect == true)
                    {
                        this.Children.Add(new TreeViewItemVM(new TreeNode<TreeStruct.Element>
                            (new TreeStruct.Element() { Name = "Dummy", Attributes = new Dictionary<string, string>() { ["Dummy"] = "Dummy" } }), TreeItemType.Dummy, this));

                        _isExpanded = false;
                    }

                    Element.Data.Attributes[IS_EXPANDED] = _isExpanded ? "true" : "false";
                    OnPropertyChanged(nameof(IsExpanded));
                }
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { OnPropertyChanged(ref _isBusy, value); }
        }

        #endregion

        #region Helper Methods

        private void GetIsExpandedIsSelected()
        {
            string value1;
            if (Element.Data.Attributes.TryGetValue(IS_EXPANDED, out value1))
            {
                if (value1 == "true")
                    this.IsExpanded = true;
            }
            string value2;
            if (Element.Data.Attributes.TryGetValue(IS_SELECTED, out value2))
            {
                if (value2 == "true")
                    this.IsSelected = true;
            }
        }

        private void ClearChildren()
        {
            _children = new ObservableCollection<TreeViewItemVM>();
            // We cannot add children this items: 
            if (_nonExpandedTypes.Contains(this.Type))
                return;
            else
                this.Children.Add(new TreeViewItemVM(new TreeNode<TreeStruct.Element>
                    (new TreeStruct.Element() { Name = "Dummy", Attributes = new Dictionary<string, string>() { ["Dummy"] = "Dummy" } }), TreeItemType.Dummy, this));
        }

        private Task ExpandAsync()
        {
            return Task.Factory.StartNew(async() => {
                this.IsSelected = true;
                // We cannot expand this items: 
                if (_nonExpandedTypes.Contains(this.Type))
                    return;
                
                IsBusy = true;
                if (!string.IsNullOrEmpty(Properties.Settings.Default.ConnectionString))
                    _notConnect = false;

                if (Element.Children.Count == 0 && Properties.Settings.Default.IsProjectOpening == false && !Element.Data.Attributes.ContainsKey("is_empty"))
                {
                    try
                    {
                        await TreeStructHelper.LoadChildAsync(Element.Data.Name, this.Element);
                    }
                    catch (Exception e)
                    {
                        Logger.StatusChanged("Cannot expand!", StatusBackgroundColor.Orange);
                        _notConnect = true;
                        MessageBox.Show(e.Message, "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        Logger.StatusChanged();
                    }
                }

                // Find all children
                var chindren = Element.Children;
                if (chindren.Count > 0)
                {
                    if (this.Type == TreeItemType.Folder)
                        this.Children = new ObservableCollection<TreeViewItemVM>(chindren.Select(node => new TreeViewItemVM(node, GetType(node.Data.Name), this)).OrderBy(tree => tree.Element.Data.Attributes["name"]));
                    else
                        this.Children = new ObservableCollection<TreeViewItemVM>(chindren.Select(node => new TreeViewItemVM(node, GetType(node.Data.Name), this)));
                }
                if (chindren.Count > 0 && this.Children[0].Element.Data.Name == "Dummy" && _notConnect == false)
                    this.Children.Clear();
                IsBusy = false;
            });
        }

        private TreeItemType GetType(string name)
        {
            TreeItemType item = TreeItemType.Folder;
            TreeItemType value;
            if (_dbTypes.TryGetValue(name, out value))
                item = value;
            return item;
        }

        public override string ToString()
        {
            string value = "";
            if (this.Element.Data.Attributes.TryGetValue("name", out value))
                return value;
            return value;
        }

        #endregion

        #region NameContainsText

        public bool NameContainsText(string text)
        {
            if (String.IsNullOrEmpty(text) || String.IsNullOrEmpty(this.Name))
                return false;

            return this.Name.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1;
        }

        #endregion

        #region Command Methods Helper

        private bool CanCreateSQLScript
        {
            get
            {
                if (Properties.Settings.Default.IsScriptCreating == true)
                    return false;
                if (_nonScriptCreatedItems.Contains(this.Type))
                    return false;
                return true;
            }
        }

        private async Task CreateSQLScriptAsync()
        {
            LOG.Info(String.Format("Attempt to create SQL script for \"{0}\" item.", this.Element.Data.Attributes["name"]));
            Logger.StatusChanged(String.Format("Attempt to create SQL script for \"{0}\" item.", this.Element.Data.Attributes["name"]), StatusBackgroundColor.Orange);
            this.IsSelected = true;
            try
            {
                Properties.Settings.Default.IsScriptCreating = true;
                Properties.Settings.Default.Save();
                if (!String.IsNullOrEmpty(this.SqlScript))
                {
                    OnSqlScriptCreated(this.SqlScript);
                    return;
                }
                StringBuilder query = null;
                await Task.Run(async () =>
                {
                    Logger.StatusChanged(String.Format("Creating SQL Script for:    {0}  \"{1}\" ", this.Element.Data.Name, this.Element.Data.Attributes["name"]));
                    query = await SQLCreateHelper.CreateSqlScriptAsync(this.Element);
                    this.SqlScript = query.ToString();
                    OnSqlScriptCreated(this.SqlScript);
                    LOG.Info("SQL script was created.");
                });
            }
            catch (ArgumentException e)
            {
                LOG.Warn("Cannot script create!");
                Logger.StatusChanged("Cannot script create!", StatusBackgroundColor.Orange);
                MessageBox.Show(e.Message, "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                LOG.Warn("Cannot script create!");
                Logger.StatusChanged("Cannot script create!", StatusBackgroundColor.Orange);
                MessageBox.Show(ex.Message, "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                Properties.Settings.Default.IsScriptCreating = false;
                Properties.Settings.Default.Save();
                Logger.StatusChanged();
            }
        }

        private async Task RefreshItemAsync()
        {
            this.IsSelected = true;
            this.SqlScript = String.Empty;
            try
            {
                await TreeStructHelper.LoadChildAsync(this.Element.Data.Name, this.Element);
                var chindren = Element.Children;
                this.Children = new ObservableCollection<TreeViewItemVM>(chindren.Select(node => new TreeViewItemVM(node, GetType(node.Data.Name))));
            }
            catch (ArgumentException e)
            {
                Logger.StatusChanged("Cannot refresh this item!", StatusBackgroundColor.Orange);
                MessageBox.Show(e.Message, "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                Logger.StatusChanged();
            }
            catch (Exception ex)
            {
                Logger.StatusChanged("Cannot refresh this item!", StatusBackgroundColor.Orange);
                MessageBox.Show(ex.Message, "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                Logger.StatusChanged();
            }
        }

        #endregion

    }
}
