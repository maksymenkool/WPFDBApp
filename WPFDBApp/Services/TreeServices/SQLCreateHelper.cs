using System.Text;
using System.Threading.Tasks;
using TreeStruct;

namespace WPFDBApp.Services.TreeServices
{
    /// <summary>
    /// A class that helps create sql script
    /// </summary>
    public class SQLCreateHelper
    {
        #region Create Sql Script Async

        internal static async Task<StringBuilder> CreateSqlScriptAsync(TreeNode<Element> node)
        {
            StringBuilder query = new StringBuilder();
            StringBuilder queryFK = new StringBuilder();

            foreach (var item in node)
            {
                switch (item.Data.Name)
                {
                    case "Database":
                        await TreeStructHelper.LoadChildAsync(item.Data.Name, item);
                        foreach (var it in item.Children)
                            await TreeStructHelper.LoadChildAsync(it.Data.Name, it);
                        query.Append("CREATE DATABASE ").Append(item.Data.Attributes["name"]).Append("\nGO\nUSE ").Append(item.Data.Attributes["name"]).Append("\nGO\n");
                        break;
                    case "Table":
                        await TreeStructHelper.LoadChildAsync(item.Data.Name, item);
                        foreach (var it in item.Children)
                            await TreeStructHelper.LoadChildAsync(it.Data.Name, it);
                        CreateTable(item, query, queryFK);
                        break;
                    case "View":
                    case "Procedure":
                        await TreeStructHelper.LoadChildAsync(item.Data.Name, item);
                        if(!string.IsNullOrEmpty(item.Data.Attributes["definition"]))
                            query.Append(item.Data.Attributes["definition"].Trim()).Append("\nGO\n");
                        break;
                    default:
                        break;
                }
            }
            if (queryFK.Length > 0)
                query.Append(queryFK.ToString());

            return query;
        }

        #endregion

        #region Helper Methods

        private static void CreateTable(TreeNode<Element> item, StringBuilder query, StringBuilder queryFK)
        {
            query.Append("CREATE TABLE ").Append(item.Data.Attributes["name"]).Append("\n(\n");
            int countCol = 0;
            bool flag = false;
            foreach (var tabItem in item)
            {
                if (tabItem.Data.Name == "Columns")
                {
                    continue;
                }
                if (tabItem.Data.Name == "Column")
                {
                    CreateColumn(tabItem, query);
                    if (countCol < item.Children[0].ChildrenCount())
                        query.Append(",\n");
                    countCol++;
                }
                else if (tabItem.Data.Name != "Table")
                {
                    if (!flag)
                    {
                        query.Remove(query.Length - 2, 1);
                        query.Append(")\nGO\n");
                        flag = true;
                    }
                    if ("PrimaryKey" == tabItem.Data.Name)
                        CreatePrimaryKey(tabItem, query, item.Data.Attributes["name"]);
                    if ("CheckConstraint" == tabItem.Data.Name)
                        CreateConstraint(tabItem, query, item.Data.Attributes["name"]);
                    if ("Index" == tabItem.Data.Name)
                        CreateIndex(tabItem, query, item.Data.Attributes["name"]);
                    if ("ForeignKey" == tabItem.Data.Name)
                        CreateForeignKey(tabItem, queryFK, item.Data.Attributes["name"]);
                }
            }
            if (!flag)
            {
                query.Remove(query.Length - 2, 1);
                query.Append(")\nGO\n");
            }
        }

        private static void CreatePrimaryKey(TreeNode<Element> tabItem, StringBuilder query, string tableName)
        {
            query.Append("ALTER TABLE ").Append(tableName).Append("\nADD");
            if (tabItem.Data.Attributes["is_system_named"] == "False")
            {
                query.Append(" CONSTRAINT ").Append(tabItem.Data.Attributes["name"]);
            }
            query.Append(" PRIMARY KEY ").Append(tabItem.Data.Attributes["type_desc"]).
            Append(" (").Append(tabItem.Data.Attributes["included_columns"]).Append(")\nGO\n");
        }

        private static void CreateForeignKey(TreeNode<Element> tabItem, StringBuilder query, string tableName)
        {
            query.Append("ALTER TABLE ").Append(tableName).Append("\nADD ");
            if (tabItem.Data.Attributes["is_system_named"] == "False")
                query.Append("CONSTRAINT ").Append(tabItem.Data.Attributes["name"]);
            query.Append("FOREIGN KEY (").
                Append(tabItem.Data.Attributes["constraint_column_name"]).Append(")\nREFERENCES ").Append(tabItem.Data.Attributes["referenced_object"]).
                Append(" (").Append(tabItem.Data.Attributes["referenced_column_name"]).Append(")");
            if (tabItem.Data.Attributes["delete_referential_action_desc"] != "NO_ACTION")
                query.Append(" ").Append(tabItem.Data.Attributes["delete_referential_action_desc"]).Append(" ");
            if (tabItem.Data.Attributes["update_referential_action_desc"] != "NO_ACTION")
                query.Append(" ").Append(tabItem.Data.Attributes["update_referential_action_desc"]).Append(" ");
            query.Append("\nGO\n");
        }

        private static void CreateConstraint(TreeNode<Element> tabItem, StringBuilder query, string tableName)
        {
            query.Append("ALTER TABLE ").Append(tableName).Append(" WITH CHECK ADD CHECK").
                Append(tabItem.Data.Attributes["check_value"]).Append("\nGO\n");
        }

        private static void CreateIndex(TreeNode<Element> tabItem, StringBuilder query, string tableName)
        {
            if (tabItem.Data.Attributes["is_primary_key"] == "True") return;
            query.Append("CREATE ");
            if (tabItem.Data.Attributes["is_unique"] == "True")
                query.Append("UNIQUE ");
            query.Append(tabItem.Data.Attributes["type_desc"]).Append(" INDEX ").
                Append(tabItem.Data.Attributes["name"]).Append(" ON ").Append(tableName).
                Append("(").Append(tabItem.Data.Attributes["included_columns"]).Append(")\nGO\n");
        }

        private static void CreateColumn(TreeNode<Element> node, StringBuilder query)
        {
            string value;
            query.Append("\t").Append(node.Data.Attributes["name"]).Append("\t").Append(node.Data.Attributes["data_type"]);
            if (node.Data.Attributes["data_type"] == "datetime2" || node.Data.Attributes["data_type"] == "datetimeoffset" || node.Data.Attributes["data_type"] == "time")
                query.Append("(").Append(node.Data.Attributes["scale"]).Append(")");
            else if (node.Data.Attributes["data_type"] == "decimal" || node.Data.Attributes["data_type"] == "numeric")
                query.Append("(").Append(node.Data.Attributes["precision"]).Append(",").Append(node.Data.Attributes["scale"]).Append(")");
            else if (node.Data.Attributes["data_type"] == "float")
                query.Append("(").Append(node.Data.Attributes["precision"]).Append(")");
            else if (node.Data.Attributes["data_type"] == "uniqueidentifier")
            { }
            else if (node.Data.Attributes.TryGetValue("max_length", out value))
            {
                if (node.Data.Attributes["max_length"] == "-1")
                    query.Append("(MAX)");
                else if (node.Data.Attributes["max_length"] == "2147483647")
                    query.Append("");
                else
                    query.Append("(").Append(value).Append(")");
            }
            if (node.Data.Attributes["is_identity"] == "True")
                query.Append(" IDENTITY(").Append(node.Data.Attributes["seed_value"]).Append(",").Append(node.Data.Attributes["increment_value"]).Append(")");
            if (node.Data.Attributes["is_nullable"] == "NO")
                query.Append("\tNOT NULL");
            else if (node.Data.Attributes["is_nullable"] == "YES")
                query.Append("\tNULL");
            if (node.Data.Attributes.TryGetValue("column_default", out value))
                query.Append("\n\t\tDEFAULT\t").Append(value);
        }

        #endregion
    }
}
