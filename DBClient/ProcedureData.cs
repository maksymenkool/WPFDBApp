using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBClient
{
    public class ProcedureData
    {

        public static DataTable GetProcedureData(SqlConnection connection)
        {
            string query = @"SELECT s.name+'.'+p.name AS name, p.create_date, p.modify_date
					, OBJECT_DEFINITION(p.object_id) AS definition
                    FROM sys.procedures AS p
					INNER JOIN sys.schemas AS s ON p.schema_id = s.schema_id;";
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

        public static DataTable GetParamProcedures(string nameProcedure, SqlConnection connection)
        {
            string query = @"SELECT par.name AS name  
                    ,TYPE_NAME(par.user_type_id) AS parameter_type  
                    ,par.max_length
					,par.is_output
					,default_value
	                FROM sys.parameters AS par
	                WHERE par.object_id = OBJECT_ID('" + nameProcedure + @"');";
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
