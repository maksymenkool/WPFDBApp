using System.Data;
using System.Data.SqlClient;

namespace DBClient
{
    public class TableData
    {
        public static DataTable GetTableName(SqlConnection connection)
        {
            string query = @"SELECT s.name+'.'+t.name AS name, t.create_date, t.modify_date
                         FROM sys.tables AS t
                         INNER JOIN sys.schemas AS s ON t.schema_id = s.schema_id;";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    var tbl = new DataTable();
                    tbl.Load(dr);
                    return tbl;
                }
            }
        }

        public static DataTable GetTableColumns(string tableName, SqlConnection connection)
        {
            string query = @"SELECT c.name AS name, isc.data_type, c.max_length
                , c.scale, c.precision, c.is_identity, ic.seed_value, ic.increment_value
	            , isc.is_nullable, isc.column_default
                FROM information_schema.columns AS isc
                JOIN sys.all_columns AS c ON isc.column_name = c.name
                AND isc.table_name = OBJECT_NAME(c.object_id)
                AND isc.table_schema = OBJECT_SCHEMA_NAME(c.object_id)
                LEFT JOIN sys.identity_columns AS ic ON ic.object_id = c.object_id
                WHERE c.object_id = OBJECT_ID('" + tableName + @"');";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    var tbl = new DataTable();
                    tbl.Load(dr);
                    return tbl;
                }
            }
        }

        public static DataTable GetTablePrimaryKey(string tableName, SqlConnection connection)
        {
            string query = @"SELECT i.name
	                ,is_system_named
	                ,LEFT(list, ISNULL(splitter-1,len(list))) included_columns
                    ,i.type_desc
	                ,i.is_primary_key
                    FROM sys.indexes AS i  
                    cross apply (select NULLIF(charindex('|',indexCols.list),0) splitter , list
                                    from (select cast((
                                    select case when sc.is_included_column = 1 and sc.ColPos = 1 then '|' else '' end +
                                            case when sc.ColPos  > 1 then ', ' else '' end + name 
                                            from (select sc.is_included_column, index_column_id, name
                                                    , ROW_NUMBER() over (partition by sc.is_included_column 
                                                                        order by sc.index_column_id) ColPos
                                                from sys.index_columns  sc
                                                join sys.columns c 
							                    on sc.object_id = c.object_id
                                                and sc.column_id = c.column_id
                                                where sc.index_id = i.index_id 
                                                and sc.object_id = i.object_id ) sc
                   		                    order by sc.is_included_column ,ColPos
                                            for xml path (''), type) as varchar(max)) list)indexCols ) indCol
                LEFT JOIN sys.key_constraints AS kc ON kc.name = i.name AND kc.parent_object_id = i.object_id
                WHERE i.is_primary_key=1 AND  i.type IN (1,2) AND i.object_id = OBJECT_ID('" + tableName + @"');";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    var tbl = new DataTable();
                    tbl.Load(dr);
                    return tbl;
                }
            }
        }

        public static DataTable GetTableForeignKey(string tableName, SqlConnection connection)
        {
            string query = @"SELECT f.name AS name
                       ,f.is_system_named
                       ,COL_NAME(fc.parent_object_id, fc.parent_column_id) AS constraint_column_name
                       ,OBJECT_SCHEMA_NAME(f.referenced_object_id)+'.'+OBJECT_NAME(f.referenced_object_id) AS referenced_object
                       ,COL_NAME(fc.referenced_object_id, fc.referenced_column_id) AS referenced_column_name
                       ,f.is_disabled 
                       ,f.delete_referential_action_desc
                       ,f.update_referential_action_desc
                       FROM sys.foreign_keys AS f
                       INNER JOIN sys.foreign_key_columns AS fc
                       ON f.object_id = fc.constraint_object_id
                       WHERE f.parent_object_id = OBJECT_ID('" + tableName + @"');";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    var tbl = new DataTable();
                    tbl.Load(dr);
                    return tbl;
                }
            }
        }

        public static DataTable GetTableCheckConstraint(string tableName, SqlConnection connection)
        {
            string query = @"SELECT cc.name, cc.definition AS check_value, cc.type_desc AS type_constraint
                         FROM sys.check_constraints AS cc
                         WHERE cc.parent_object_id = OBJECT_ID('" + tableName + @"');";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    var tbl = new DataTable();
                    tbl.Load(dr);
                    return tbl;
                }
            }
        }

        public static DataTable GetTableDefaultConstraint(string tableName, SqlConnection connection)
        {
            string query = @"SELECT dc.name, COL_NAME(dc.parent_object_id, dc.parent_column_id) AS column_name,
						dc.definition AS default_value, dc.type_desc AS type_constraint
						FROM sys.default_constraints AS dc 
						WHERE dc.parent_object_id = OBJECT_ID('" + tableName + @"');";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    var tbl = new DataTable();
                    tbl.Load(dr);
                    return tbl;
                }
            }
        }

        public static DataTable GetTableIndex(string tableName, SqlConnection connection)
        {
            string query = @"SELECT i.name
		                    ,is_system_named
		                    ,LEFT(list, ISNULL(splitter-1,len(list))) included_columns
                            ,i.type_desc
		                    ,i.is_unique 
                            ,i.is_primary_key  
                            FROM sys.indexes AS i  
                            cross apply (select NULLIF(charindex('|',indexCols.list),0) splitter , list
                                            from (select cast((
                                            select case when sc.is_included_column = 1 and sc.ColPos = 1 then '|' else '' end +
                                                    case when sc.ColPos  > 1 then ', ' else '' end + name 
                                                    from (select sc.is_included_column, index_column_id, name
                                                            , ROW_NUMBER() over (partition by sc.is_included_column 
                                                                                order by sc.index_column_id) ColPos
                                                        from sys.index_columns  sc
                                                        join sys.columns c 
							                            on sc.object_id = c.object_id
                                                        and sc.column_id = c.column_id
                                                        where sc.index_id = i.index_id 
                                                        and sc.object_id = i.object_id ) sc
                   		                            order by sc.is_included_column ,ColPos
                                                    for xml path (''), type) as varchar(max)) list)indexCols ) indCol
                        LEFT JOIN sys.key_constraints AS kc ON kc.name = i.name AND kc.parent_object_id = i.object_id
	                    WHERE  i.type IN (1,2) AND i.object_id = OBJECT_ID('" + tableName + @"');";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    var tbl = new DataTable();
                    tbl.Load(dr);
                    return tbl;
                }
            }
        }
    }
}
