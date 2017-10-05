using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TreeStruct;
using WPFDBApp.Services;
using WPFDBApp.Services.TreeServices;
using WPFDBApp.ViewModel;
using WPFDBApp.ViewModel.UserControls;

namespace WPFDBApp.View.UserControls
{
    public delegate void TreeViewItemsHandler(string serverName);
    public delegate void OpenExistProjectHandler(TreeNode<Element> rootNode);

    /// <summary>
    /// Interaction logic for TreeViewUserControl.xaml
    /// </summary>
    public partial class TreeViewUserControl : UserControl
    {
        #region Logger

        private static readonly log4net.ILog LOG = Logger.For(typeof(TreeViewUserControl));

        #endregion
        
        #region DependencyProperty

        public static readonly DependencyProperty TreeRootNodeProperty =
        DependencyProperty.Register("TreeRootNode", typeof(TreeNode<Element>), typeof(TreeViewUserControl));

        public TreeNode<Element> TreeRootNode
        {
            get { return (TreeNode<Element>)GetValue(TreeRootNodeProperty); }
            set { SetValue(TreeRootNodeProperty, value); }
        }

        public static readonly DependencyProperty TreeViewRootItemProperty =
        DependencyProperty.Register("TreeViewRootItem", typeof(ObservableCollection<TreeViewItemVM>), typeof(TreeViewUserControl));

        public ObservableCollection<TreeViewItemVM> TreeViewRootItem
        {
            get { return (ObservableCollection<TreeViewItemVM>)GetValue(TreeViewRootItemProperty); }
            set { SetValue(TreeViewRootItemProperty, value); }
        }

        public static readonly DependencyProperty SQLScriptProperty =
        DependencyProperty.Register("SQLScript", typeof(string), typeof(TreeViewUserControl), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string SQLScript
        {
            get { return (string)GetValue(SQLScriptProperty); }
            set { SetValue(SQLScriptProperty, value); }
        }

        public static readonly DependencyProperty TreeSelectedItemProperty =
        DependencyProperty.Register("TreeSelectedItem", typeof(TreeViewItemVM), typeof(TreeViewUserControl));

        public TreeViewItemVM TreeSelectedItem
        {
            get { return (TreeViewItemVM)GetValue(TreeSelectedItemProperty); }
            set { SetValue(TreeSelectedItemProperty, value); }
        }

        #endregion

        #region Private Fields

        private TreeNode<Element> _serverRoot = null;
        private TreeViewVM _treeViewModel;

        #endregion
        
        #region Constructor

        public TreeViewUserControl()
        {
            LOG.Info("Loading server metadata in the Tree View.");
            Logger.StatusChanged("Loading...");
            InitializeComponent();
            ConnectWindowVM.OnServerConnectCreating += TreeViewItemsCreated;
            MainWindowVM.OnProjectOpen += TreeViewItemsCreatedFromExistProject;
        }

        #endregion

        #region Helper Methods

        private void WriteIsProjectOpening(bool value)
        {
            Properties.Settings.Default.IsProjectOpening = value;
            Properties.Settings.Default.Save();
        }

        private void TreeViewItemsCreatedFromExistProject(TreeNode<Element> rootNode)
        {
            WriteIsProjectOpening(true);
            _serverRoot = rootNode;
            _treeViewModel = new TreeViewVM(_serverRoot);
            this.DataContext = _treeViewModel;
            LOG.Info("The metadata is loaded.");
            Logger.StatusChanged();
            WriteIsProjectOpening(false);
        }

        private void TreeViewItemsCreated(string serverName)
        {
            if (_serverRoot != null && _serverRoot.Data.Attributes["name"] == serverName)
                return;
            Element rootData = new Element();
            rootData.Name = DBConstants.SERVER;
            rootData.Attributes.Add("name", serverName);
            _serverRoot = new TreeNode<Element>(rootData);
            _treeViewModel = new TreeViewVM(_serverRoot);
            this.DataContext = _treeViewModel;
            LOG.Info("The metadata is loaded.");
            Logger.StatusChanged();
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            if (_treeViewModel != null)
                _treeViewModel.SearchCommand.Execute(null);
        }

        #endregion
    }
}
