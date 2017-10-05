using Ionic.Zip;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Xml.Schema;
using TreeStruct;
using WPFDBApp.Services;
using WPFDBApp.Services.Commands;
using WPFDBApp.Services.TreeServices;
using WPFDBApp.View.UserControls;
using WPFDBApp.ViewModel.UserControls;

namespace WPFDBApp.ViewModel
{
    public delegate void StatusHandler(string status, StatusBackgroundColor color);


    public class MainWindowVM : BaseVM
    {
        #region Logger

        private static readonly log4net.ILog LOG = Logger.For(typeof(MainWindowVM));

        #endregion

        #region Private fields

        private string _connectionString;
        private bool _canSaveProject;

        private TreeViewVM _treeVM;
        private TreeNode<Element> _rootNode;

        private string _status = "Ready";
        private string _statusColor = "RoyalBlue";

        #endregion

        #region Commands

        public ICommand ShowConnectWindowCommand { get; }
        public ICommand DisconnectServerCommand { get; }
        public ICommand SaveAsCommand { get; }
        public ICommand OpenCommand { get; }
        public ICommand SaveProjectAsCommand { get; }
        public ICommand ExitCommand { get; }
        public ICommand HelpCommand { get; }

        #region Edit Commands

        public ICommand CopyCommand { get; }
        public ICommand PasteCommand { get; }
        public ICommand CutCommand { get; }
        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }

        #endregion //Edit Commands

        #endregion

        #region Events

        public event EventHandler AboutMessage;
        public event EventHandler ConnectMessage;
        public static event OpenExistProjectHandler OnProjectOpen;

        #endregion

        #region Constructor

        public MainWindowVM()
        {
            Logger.OnStatusChanged += StatusChanged;

            ShowConnectWindowCommand = new BaseCommand(p => ShowConnectWindow(), p => CanGetConnect);
            DisconnectServerCommand = new BaseCommand(p => DisconnectServer(), p => !CanGetConnect);
            SaveAsCommand = new BaseCommand(p => SaveFileAs(p));
            OpenCommand = new BaseCommand(p => OpenProgect(), p => CanGetConnect);
            SaveProjectAsCommand = new BaseCommand(p => SaveProjectAs(p), p => CanSaveProject);

            CopyCommand = ApplicationCommands.Copy;
            PasteCommand = ApplicationCommands.Paste;
            CutCommand = ApplicationCommands.Cut;
            UndoCommand = ApplicationCommands.Undo;
            RedoCommand = ApplicationCommands.Redo;

            ExitCommand = new BaseCommand(p => CloseApp());
            HelpCommand = new BaseCommand(p => DisplayAbout());
        }

        #endregion

        #region Public Properties

        public bool CanSaveProject
        {
            get
            {
                if (Properties.Settings.Default.IsScriptCreating == true)
                    return false;
                return _canSaveProject;
            }
        }

        public TreeViewVM TreeVM
        {
            get { return _treeVM; }
            set
            {
                _treeVM = value;
                OnPropertyChanged(nameof(TreeVM));
            }
        }

        public string Status
        {
            get { return _status; }
            set { OnPropertyChanged(ref _status, value); }
        }

        public string StatusColor
        {
            get { return _statusColor; }
            set { OnPropertyChanged(ref _statusColor, value); }
        }

        public string ConnectionServerString
        {
            get { return _connectionString; }
            set
            {
                if (_connectionString == value)
                    return;

                _connectionString = value;

                OnPropertyChanged(nameof(ConnectionServerString));
            }
        }

        internal bool CanGetConnect
        {
            get { return null == ConnectionServerString; }
        }

        #endregion

        #region Status Change

        private void StatusChanged(string status, StatusBackgroundColor color)
        {
            this.Status = status;
            this.StatusColor = Enum.GetName((color.GetType()), color);
        }

        #endregion

        #region Commands Helper Methods

        private void DisplayAbout()
        {
            AboutMessage(null, EventArgs.Empty);
        }

        internal void SaveFileAs(object data)
        {
            LOG.Info("The user tries to save data in file.");
            Logger.StatusChanged("Saving data to a file...");
            string doc = data as string;
            if (string.IsNullOrEmpty(doc))
            {
                Logger.StatusChanged("No data to save.", StatusBackgroundColor.Orange);
                MessageBox.Show("Data is empty. No data to save.", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                Logger.StatusChanged();
                LOG.Info("Data is empty. No data to save.");
                return;
            }
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "SQL File(*.sql)|*.sql|Text Documents(*.txt)|*.txt";
            saveFileDialog.ValidateNames = true;
            if (saveFileDialog.ShowDialog() == true)
            {
                LOG.Info("The data are saved to a file.");
                File.WriteAllText(saveFileDialog.FileName, doc);
            }
            else
            {
                Logger.StatusChanged("Saving data is canceled.", StatusBackgroundColor.Orange);
                LOG.Info("Saving data is canceled.");
            }
            Logger.StatusChanged();
        }

        private void ShowConnectWindow()
        {
            Logger.StatusChanged("Connecting to the server...");
            ConnectMessage(null, EventArgs.Empty);
            if (CanGetConnect == false)
                _canSaveProject = true;
        }

        private void DisconnectServer()
        {
            Logger.StatusChanged("Disconnecting...");
            LOG.Info("The user tries disconnect from the server.");
            if (MessageBox.Show("Do you want to disconnect from server?", "Disconnecting...", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                LOG.Info("Server is disconnected.");
                ConnectionServerString = null;
                Properties.Settings.Default.ConnectionString = null;
                Properties.Settings.Default.Save();
                Logger.StatusChanged();
                return;
            }
            Logger.StatusChanged();
            LOG.Info("Disconnect is canceled.");
        }

        internal void OpenProgect()
        {
            LOG.Info("Opening existing project.");
            Logger.StatusChanged("Opening existing project...");
            OpenFileDialog oFD = new OpenFileDialog() { Filter = "Zip files(*.zip)|*.zip", ValidateNames = true, Multiselect = false };

            if (oFD.ShowDialog() == true)
            {
                Logger.StatusChanged("Loading...");
                string fileForValidation = "ForValidationXmlFile.xml";
                string extractPath = @"ProjectData";
                try
                {
                    string zipPath = oFD.FileName;
                    DeleteDirectory(extractPath);
                    using (ZipFile zip1 = ZipFile.Read(zipPath))
                    {
                        foreach (ZipEntry e in zip1)
                        {
                            e.Extract(extractPath, ExtractExistingFileAction.OverwriteSilently);
                        }
                    }
                    bool nodefileexist = false;
                    bool sysfileexist = false;
                    string[] files = Directory.GetFiles(extractPath, "*.xml");
                    for (int i = 0; i < files.Length; i++)
                    {
                        if (files[i].EndsWith("_Node.xml"))
                        {
                            if (File.Exists(fileForValidation))
                                File.Delete(fileForValidation);
                            File.Copy(files[i], fileForValidation);
                            ValidateXMLFile(fileForValidation);
                            File.Delete(fileForValidation);

                            XMLConverteHelper.DeserializeFromXml(files[i], out _rootNode);
                            nodefileexist = true;
                        }
                        if (files[i].EndsWith("_sys.xml"))
                        {
                            XMLConverteHelper.DeserializeSysFileFromXml(files[i]);
                            sysfileexist = true;
                        }
                    }
                    if (nodefileexist != true || sysfileexist != true)
                        throw new FileLoadException("Cannot open this project.\nPlease choose another project, and try again.\n");
                    LOG.Info("The project is open.");
                }
                catch (Exception ex)
                {
                    Logger.StatusChanged("Cannot open project!", StatusBackgroundColor.Orange);
                    LOG.Warn("The project opening is failed.\n" + ex.StackTrace);
                    System.Threading.Thread.Sleep(200);
                    MessageBox.Show("The project opening is failed.\n" + ex.Message, "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                finally
                {
                    if (File.Exists(fileForValidation))
                        File.Delete(fileForValidation);
                    DeleteDirectory(extractPath);
                }
                Logger.StatusChanged("Loading...");
                OnProjectOpen(_rootNode);
                Logger.StatusChanged();
                _canSaveProject = true;
                return;
            }
            else
            {
                Logger.StatusChanged();
                LOG.Info("The project opening is canceled.");
            }
        }

        private void DeleteDirectory(string extractPath)
        {
            if (Directory.Exists(extractPath))
            {
                string[] files_arr = Directory.GetFiles(extractPath);
                foreach (var fileName in files_arr)
                {
                    SetFileReadAccess(fileName, false);
                    File.Delete(fileName);
                }
                Directory.Delete(extractPath, true);
            }
        }

        internal void SaveProjectAs(object parameter) // object[] parameter = {object treeNode, object treeView}
        {
            LOG.Info("The user tries to save project.");
            Logger.StatusChanged("Saving project...");
            var values = parameter as object[];
            TreeNode<Element> treeNode = values[0] as TreeNode<Element>;
            ObservableCollection<TreeViewItemVM> treeView = values[1] as ObservableCollection<TreeViewItemVM>;
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "NewProject_" + treeView[0].Element.Data.Name + ".zip";
            dlg.Filter = "Zip Files (*.zip)|*.zip";
            dlg.ValidateNames = true;
            if (dlg.ShowDialog() == true)
            {

                string nodefilename = treeView[0].Element.Data.Name + "_Node.xml";
                if (File.Exists(nodefilename))
                {
                    File.Delete(nodefilename);
                }
                XMLConverteHelper.SerializeTreeNodeToXml(treeNode, nodefilename);
                string sysfilename = treeView[0].Element.Data.Name + "_sys.xml";
                if (File.Exists(sysfilename))
                {
                    File.Delete(sysfilename);
                }
                XMLConverteHelper.SerializeSysToXml(
                    sysfilename,
                    Properties.Settings.Default.UserName,
                    Properties.Settings.Default.ServerName,
                    Properties.Settings.Default.ConnectionString
                    );

                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(nodefilename);
                    zip.AddFile(sysfilename);
                    zip.Save(dlg.FileName);
                }

                File.Delete(nodefilename);
                File.Delete(sysfilename);
                LOG.Info("The project is saved.");
                Logger.StatusChanged();
            }
            else
            {
                LOG.Info("Saving project is canceled.");
                Logger.StatusChanged();
            }
        }

        private void ValidateXMLFile(string nodefilename)
        {
            using (XmlTextReader tr = new XmlTextReader(nodefilename))
            {
                using (XmlValidatingReader vr = new XmlValidatingReader(tr))
                {
                    vr.Schemas.Add("MetaData", "XMLFileSchemaForValidation.xsd");
                    vr.ValidationType = ValidationType.Schema;
                    vr.ValidationEventHandler += new ValidationEventHandler(ValidationHandler);

                    while (vr.Read()) ;
                };
            };
        }

        public static void ValidationHandler(object sender, ValidationEventArgs args)
        {
            throw new ArgumentException("Project Validation Error!");
        }

        private void SetFileReadAccess(string FileName, bool SetReadOnly)
        {
            FileInfo fInfo = new FileInfo(FileName);

            // Set the IsReadOnly property.
            fInfo.IsReadOnly = SetReadOnly;
        }

        internal void CloseApp()
        {
            Application.Current.MainWindow.Close();
        }

        internal void ServerConnectionGetMessageHandler(object sender, SqlStringCreatorEventArgs e)
        {
            _connectionString = e.ServerConnectionString;

            Properties.Settings.Default.ConnectionString = _connectionString;
            Properties.Settings.Default.Save();

            OnPropertyChanged(nameof(ConnectionServerString));
        }

        #endregion
    }
}
