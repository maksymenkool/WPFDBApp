using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TreeStruct;

namespace WPFDBApp.Services.TreeServices
{
    /// <summary>
    /// A class that helps load data from a database to a tree structure.
    /// </summary>
    public class TreeStructHelper
    {
        #region Private Fields

        private static readonly Dictionary<string, Action<SqlConnection, TreeNode<Element>>> _loaderMap = new Dictionary<string, Action<SqlConnection, TreeNode<Element>>>
        {
            { DBConstants.SERVER, AddDBsItem }, { DBConstants.DATABASES, GetDataBases }, { DBConstants.DATABASE, AddItemToDB },
            { DBConstants.TABLES, GetTables }, { DBConstants.TABLE, AddItemToTable }, { DBConstants.VIEWS, GetViews },
            { DBConstants.PROCEDURES, GetProcedures }, { DBConstants.PROCEDURE, GetProcedureParam }, { DBConstants.COLUMNS, GetTableColumnsData },
            { DBConstants.KEYS, GetTableKeysData }, { DBConstants.CONSTRAINTS, GetTableConstraintsData }, { DBConstants.INDEXES, GetTableIndexesData }
        };

        private static readonly HashSet<string> _dbItems = new HashSet<string>
        {
            DBConstants.TABLES, DBConstants.VIEWS, DBConstants.PROCEDURES
        };

        private static readonly HashSet<string> _tableItems = new HashSet<string>
        {
            DBConstants.COLUMNS, DBConstants.KEYS,
            DBConstants.CONSTRAINTS, DBConstants.INDEXES, DBConstants.PROCEDURE,
        };

        #endregion

        #region LoadChildAsync

        public static async Task LoadChildAsync(string nameType, TreeNode<Element> root)
        {
            var connectionString = Properties.Settings.Default.ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("You are disconnected from server.\nPlease to connect to server, and try again.\n");
            }
            root.Children.Clear();
            if (_dbItems.Contains(nameType))
                connectionString = connectionString + ";Initial Catalog=" + root.Parent.Data.Attributes["name"];
            if (_tableItems.Contains(nameType))
                connectionString = connectionString + ";Initial Catalog=" + GetDBName(root);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                if (_loaderMap.ContainsKey(nameType))
                    _loaderMap[nameType].Invoke(connection, root);
            }
        }

        #endregion

        #region Helper Methods

        private static void AddDBsItem(SqlConnection connection, TreeNode<Element> root)
        {
            Element dbData = new Element();
            dbData.Name = DBConstants.DATABASES;
            dbData.Attributes.Add("name", DBConstants.DATABASES);
            root.AddChild(new TreeNode<Element>(dbData));

        }

        private static string GetDBName(TreeNode<Element> node)
        {
            TreeNode<Element> parent = node.Parent;
            string dbName = "";
            bool flag = false;
            while (!flag)
            {
                if (parent.Data.Name == "Database")
                {
                    dbName = parent.Data.Attributes["name"];
                    flag = true;
                    break;
                }
                else
                    parent = parent.Parent;
            }
            return dbName;
        }

        private static void GetDataBases(SqlConnection connection, TreeNode<Element> root)
        {
            DataTable dbNames = new DataTable();
            dbNames = DBClient.DBData.GetDataBasesName(connection);
            if (dbNames.Rows.Count == 0)
            {
                root.Data.Attributes["is_empty"] = "true";
                return;
            }
            if (dbNames.Rows.Count > 0)
            {
                foreach (DataRow row in dbNames.Rows)
                {
                    Element dbData = new Element();
                    dbData.Name = DBConstants.DATABASE;
                    foreach (DataColumn col in dbNames.Columns)
                    {
                        string key = col.ColumnName;
                        string val = row[col].ToString();
                        dbData.Attributes.Add(key, val);
                    }
                    root.AddChild(new TreeNode<Element>(dbData));
                }
            }
        }

        private static void AddItemToDB(SqlConnection connection, TreeNode<Element> dataBase)
        {
            Element dataBaseTables = new Element();
            dataBaseTables.Name = DBConstants.TABLES;
            dataBaseTables.Attributes.Add("name", DBConstants.TABLES);
            dataBase.AddChild(new TreeNode<Element>(dataBaseTables));

            Element dataBaseProcedures = new Element();
            dataBaseProcedures.Name = DBConstants.PROCEDURES;
            dataBaseProcedures.Attributes.Add("name", DBConstants.PROCEDURES);
            dataBase.AddChild(new TreeNode<Element>(dataBaseProcedures));

            Element dataBaseViews = new Element();
            dataBaseViews.Name = DBConstants.VIEWS;
            dataBaseViews.Attributes.Add("name", DBConstants.VIEWS);
            dataBase.AddChild(new TreeNode<Element>(dataBaseViews));

        }

        private static void GetTables(SqlConnection connection, TreeNode<Element> dataBase)
        {
            DataTable tableNames = new DataTable();
            tableNames = DBClient.TableData.GetTableName(connection);
            if (tableNames.Rows.Count == 0)
            {
                dataBase.Data.Attributes["is_empty"] = "true";
                return;
            }
            if (tableNames.Rows.Count > 0)
            {
                foreach (DataRow row in tableNames.Rows)
                {
                    Element tabData = new Element();
                    tabData.Name = DBConstants.TABLE;

                    foreach (DataColumn col in tableNames.Columns)
                    {
                        string key = col.ColumnName;
                        string val = row[col].ToString();
                        tabData.Attributes.Add(key, val);
                    }
                    dataBase.AddChild(new TreeNode<Element>(tabData));
                }
            }
        }

        private static void AddItemToTable(SqlConnection connection, TreeNode<Element> table)
        {
            Element tablecolumns = new Element();
            tablecolumns.Name = DBConstants.COLUMNS;
            tablecolumns.Attributes.Add("name", DBConstants.COLUMNS);
            table.AddChild(new TreeNode<Element>(tablecolumns));

            Element tablekeys = new Element();
            tablekeys.Name = DBConstants.KEYS;
            tablekeys.Attributes.Add("name", DBConstants.KEYS);
            table.AddChild(new TreeNode<Element>(tablekeys));

            Element tableconstraints = new Element();
            tableconstraints.Name = DBConstants.CONSTRAINTS;
            tableconstraints.Attributes.Add("name", DBConstants.CONSTRAINTS);
            table.AddChild(new TreeNode<Element>(tableconstraints));

            Element tableindexes = new Element();
            tableindexes.Name = DBConstants.INDEXES;
            tableindexes.Attributes.Add("name", DBConstants.INDEXES);
            table.AddChild(new TreeNode<Element>(tableindexes));

        }

        private static void GetProcedures(SqlConnection connection, TreeNode<Element> dataBase)
        {
            DataTable procedureTbl = new DataTable();
            procedureTbl = DBClient.ProcedureData.GetProcedureData(connection);
            if (procedureTbl.Rows.Count == 0)
            {
                dataBase.Data.Attributes["is_empty"] = "true";
                return;
            }
            if (procedureTbl.Rows.Count > 0)
            {
                foreach (DataRow row in procedureTbl.Rows)
                {
                    Element procData = new Element();
                    procData.Name = DBConstants.PROCEDURE;
                    foreach (DataColumn col in procedureTbl.Columns)
                    {
                        string key = col.ColumnName;
                        string val = (row[col].ToString());
                        procData.Attributes.Add(key, val);
                    }
                    dataBase.AddChild(new TreeNode<Element>(procData));
                }
            }
        }

        private static void GetProcedureParam(SqlConnection connection, TreeNode<Element> procedure)
        {
            DataTable paramTbl = new DataTable();
            paramTbl = DBClient.ProcedureData.GetParamProcedures(procedure.Data.Attributes["name"].ToString(), connection);
            if (paramTbl.Rows.Count == 0)
            {
                procedure.Data.Attributes["is_empty"] = "true";
                return;
            }

            AddItem(paramTbl, DBConstants.PARAMETER, procedure);
        }

        private static void GetViews(SqlConnection connection, TreeNode<Element> dataBase)
        {
            DataTable viewTbl = new DataTable();
            viewTbl = DBClient.ViewData.GetViewData(connection);
            if (viewTbl.Rows.Count == 0)
            {
                dataBase.Data.Attributes["is_empty"] = "true";
                return;
            }
            if (viewTbl.Rows.Count > 0)
            {
                foreach (DataRow row in viewTbl.Rows)
                {
                    Element viewData = new Element();
                    viewData.Name = DBConstants.VIEW;
                    foreach (DataColumn col in viewTbl.Columns)
                    {
                        string key = col.ColumnName;
                        string val = (row[col].ToString());
                        viewData.Attributes.Add(key, val);
                    }
                    dataBase.AddChild(new TreeNode<Element>(viewData));
                }
            }
        }

        private static void GetTableColumnsData(SqlConnection connection, TreeNode<Element> tableNode)
        {
            var tableName = tableNode.Parent.Data.Attributes["name"];
            DataTable tableMD = new DataTable();
            tableMD = DBClient.TableData.GetTableColumns(tableName, connection);
            if (tableMD.Rows.Count == 0)
            {
                tableNode.Data.Attributes["is_empty"] = "true";
                return;
            }

            AddItem(tableMD, DBConstants.COLUMN, tableNode);
        }

        private static void GetTableKeysData(SqlConnection connection, TreeNode<Element> tableNode)
        {
            var tableName = tableNode.Parent.Data.Attributes["name"];

            DataTable tableMDPK = new DataTable();
            tableMDPK = DBClient.TableData.GetTablePrimaryKey(tableName, connection);
            if (tableMDPK.Rows.Count == 0)
                tableNode.Data.Attributes["is_empty"] = "true";
            if (tableMDPK.Rows.Count > 0)
                AddItem(tableMDPK, DBConstants.PRIMARY_KEY, tableNode);

            DataTable tableMDFK = new DataTable();
            tableMDFK = DBClient.TableData.GetTableForeignKey(tableName, connection);
            if (tableMDFK.Rows.Count == 0)
                tableNode.Data.Attributes["is_empty"] = "true";
            if (tableMDFK.Rows.Count > 0)
            {
                if(tableNode.Data.Attributes.ContainsKey("is_empty"))
                    tableNode.Data.Attributes.Remove("is_empty");
                AddFKey(tableMDFK, DBConstants.FOREIGN_KEY, tableNode);
            }
                
        }

        private static void GetTableConstraintsData(SqlConnection connection, TreeNode<Element> tableNode)
        {
            var tableName = tableNode.Parent.Data.Attributes["name"];

            DataTable tableMDCC = new DataTable();
            tableMDCC = DBClient.TableData.GetTableCheckConstraint(tableName, connection);
            if (tableMDCC.Rows.Count == 0)
                tableNode.Data.Attributes["is_empty"] = "true";
            if (tableMDCC.Rows.Count > 0)
                AddItem(tableMDCC, DBConstants.CHECK_CONSTRAINT, tableNode);

            DataTable tableMDDC = new DataTable();
            tableMDDC = DBClient.TableData.GetTableDefaultConstraint(tableName, connection);
            if (tableMDDC.Rows.Count == 0)
                tableNode.Data.Attributes["is_empty"] = "true";
            if (tableMDDC.Rows.Count > 0)
            {
                if (tableNode.Data.Attributes.ContainsKey("is_empty"))
                    tableNode.Data.Attributes.Remove("is_empty");
                AddItem(tableMDDC, DBConstants.DEFAULT_CONSTRAINT, tableNode);
            }
        }

        private static void GetTableIndexesData(SqlConnection connection, TreeNode<Element> tableNode)
        {
            var tableName = tableNode.Parent.Data.Attributes["name"];

            DataTable tableMD = new DataTable();
            tableMD = DBClient.TableData.GetTableIndex(tableName, connection);
            if (tableMD.Rows.Count == 0)
                tableNode.Data.Attributes["is_empty"] = "true";
            if (tableMD.Rows.Count > 0)
                AddItem(tableMD, DBConstants.INDEX, tableNode);
        }

        private static void AddFKey(DataTable tableMD, string nameItem, TreeNode<Element> itemNode)
        {
            string k_name = "";

            foreach (DataRow rowTab in tableMD.Rows)
            {
                Element columnData = new Element();
                columnData.Name = nameItem;
                foreach (DataColumn colTab in tableMD.Columns)
                {
                    if (!String.IsNullOrEmpty(rowTab[colTab].ToString()))
                    {
                        string keyCol = colTab.ColumnName;
                        string valCol = rowTab[colTab].ToString();
                        if (keyCol == "name")
                            k_name = valCol;
                        columnData.Attributes.Add(keyCol, valCol);
                    }
                }
                var itemexist = itemNode.FindNodeTraversalTree(itemNode, 1, new Element() { Name = nameItem, Attributes = new System.Collections.Generic.Dictionary<string, string>() { ["name"] = k_name } });
                if (itemexist != null)
                {
                    if (itemexist.Data.Attributes["constraint_column_name"] != columnData.Attributes["constraint_column_name"])
                        itemexist.Data.Attributes["constraint_column_name"] = itemexist.Data.Attributes["constraint_column_name"] + ", " + columnData.Attributes["constraint_column_name"];

                    if (itemexist.Data.Attributes["referenced_column_name"] != columnData.Attributes["referenced_column_name"])
                        itemexist.Data.Attributes["referenced_column_name"] = itemexist.Data.Attributes["referenced_column_name"] + ", " + columnData.Attributes["referenced_column_name"];
                }
                else
                    itemNode.AddChild(new TreeNode<Element>(columnData));
            }
        }

        private static void AddItem(DataTable tableMD, string nameItem, TreeNode<Element> itemNode)
        {
            foreach (DataRow rowTab in tableMD.Rows)
            {
                Element columnData = new Element();
                columnData.Name = nameItem;
                foreach (DataColumn colTab in tableMD.Columns)
                {
                    if (!String.IsNullOrEmpty(rowTab[colTab].ToString()))
                    {
                        string keyCol = colTab.ColumnName;
                        string valCol = rowTab[colTab].ToString();
                        columnData.Attributes.Add(keyCol, valCol);
                    }
                }
                itemNode.AddChild(new TreeNode<Element>(columnData));
            }
        }

        #endregion
    }
}
