using System;
using System.IO;
using System.Security;
using System.Xml;
using TreeStruct;

namespace WPFDBApp.Services.TreeServices
{
    /// <summary>
    /// A class that helps serialize/deserialize data.
    /// </summary>
    public class XMLConverteHelper
    {
        #region Serialize to Xml

        internal static void SerializeTreeNodeToXml(TreeNode<Element> node, string fileName)
        {
            if (node == null)
            {
                throw new ArgumentNullException(
                " Cannot serialize null value!");
            }
            if (!fileName.EndsWith(".xml"))
            {
                throw new ArgumentException(
                    " Wrong file name format!");
            }
            if (File.Exists(fileName))
            {
                FileInfo fInfo = new FileInfo(fileName);
                fInfo.IsReadOnly = false;
                File.Delete(fileName);
            }
            Stream fs = new FileStream(fileName, FileMode.Create);
            using (XmlTextWriter writer = new XmlTextWriter(fs, null))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;
                writer.WriteStartDocument();
                node.Data.Attributes["xmlns"] = "MetaData";
                writer.WriteStartElement(node.Data.Name);
                if (node.Data.Attributes != null)
                    foreach (var attr in node.Data.Attributes)
                    {
                        writer.WriteAttributeString(attr.Key, attr.Value);
                    }
                writer.WriteString(node.Data.Text);
                RecursiveWriter(writer, node);
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            }
        }

        private static void RecursiveWriter(XmlTextWriter writer, TreeNode<Element> tree)
        {
            foreach (var element in tree.Children)
            {
                writer.WriteStartElement(element.Data.Name);
                if (element.Data.Attributes != null)
                {
                    foreach (var attr in element.Data.Attributes)
                    {
                        writer.WriteAttributeString(attr.Key, attr.Value);
                    }
                }
                RecursiveWriter(writer, element);
                writer.WriteString(element.Data.Text);
                writer.WriteEndElement();
            }
        }

        internal static void SerializeSysToXml(string fileName, string userName, string serverName, string connectionString)
        {
            if (File.Exists(fileName))
            {
                FileInfo fInfo = new FileInfo(fileName);
                fInfo.IsReadOnly = false;
                File.Delete(fileName);
            }
            Stream fs = new FileStream(fileName, FileMode.Create);
            using (XmlTextWriter writer = new XmlTextWriter(fs, null))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;
                writer.WriteStartDocument();
                writer.WriteStartElement("SYS_FILE");
                writer.WriteAttributeString("ConnectionString", connectionString);
                writer.WriteAttributeString("ServerName", serverName);
                writer.WriteAttributeString("UserName", userName);
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            }
        }

        #endregion

        #region Deserialize From Xml

        internal static void DeserializeFromXml(string fileName, out TreeNode<Element> tree)
        {
            tree = null;
            try
            {
                FileStream file = new FileStream(fileName, FileMode.OpenOrCreate);
                BufferedStream stream = new BufferedStream(file);

                XmlTextReader reader = null;
                using (reader = new XmlTextReader(stream))
                {
                    reader.WhitespaceHandling = WhitespaceHandling.None;
                    RecursiveRead(reader, ref tree);
                }
            }
            catch (SecurityException e)
            {
                throw new SecurityException("Cannot read file!" + e.Message);
            }
            catch (Exception e)
            {
                throw new FileLoadException("Cannot open this project.\nError: " + e.Message + "\nPlease choose another project, and try again.\n");
            }
        }

        private static void RecursiveRead(XmlTextReader reader, ref TreeNode<Element> tree)
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.EndElement:
                        if (tree.HasParent) tree = tree.Parent;
                        break;
                    case XmlNodeType.Element:
                        Element elemdata = new Element();
                        elemdata.Name = reader.Name;
                        for (int i = 0; i < reader.AttributeCount; i++)
                        {
                            reader.MoveToAttribute(i);
                            elemdata.Attributes[reader.Name] = reader.Value;
                        }
                        reader.MoveToContent();
                        if (tree == null) tree = new TreeNode<Element>(elemdata);
                        else
                        {
                            tree = tree.AddChild(new TreeNode<Element>(elemdata));
                        }
                        if (reader.IsEmptyElement)
                            if (tree.HasParent) tree = tree.Parent;
                        break;
                    case XmlNodeType.Text:
                        tree.Data.Text = reader.Value;
                        break;
                    default:
                        break;
                }
            }
        }

        internal static void DeserializeSysFileFromXml(string fileName)
        {
            try
            {
                XmlTextReader reader = null;
                using (reader = new XmlTextReader(fileName))
                {
                    reader.WhitespaceHandling = WhitespaceHandling.None;
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                if (reader.Name == "SYS_FILE")
                                {
                                    for (int i = 0; i < reader.AttributeCount; i++)
                                    {
                                        reader.MoveToAttribute(i);
                                        /*if (reader.Name == "ConnectionString")
                                            Properties.Settings.Default.ConnectionString = reader.Value;*/
                                        if (reader.Name == "ServerName")
                                            Properties.Settings.Default.ServerName = reader.Value;
                                        if (reader.Name == "UserName")
                                            Properties.Settings.Default.UserName = reader.Value;
                                    }
                                    Properties.Settings.Default.Save();
                                }
                                reader.MoveToContent();
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (SecurityException e)
            {
                throw new SecurityException("Cannot read file!" + e.Message);
            }
            catch (Exception e)
            {
                throw new FileLoadException("Cannot open this project.\nError: " + e.Message + "\nPlease choose another project, and try again.\n");
            }
        }

        #endregion
    }
}
