using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBClient
{
    public class ViewData
    {
        public static DataTable GetViewData(SqlConnection connection)
        {
            string query = @"SELECT s.name+'.'+v.name AS name
                , v.create_date, v.modify_date
                , OBJECT_DEFINITION(v.object_id) AS definition
                FROM sys.views AS v INNER JOIN sys.schemas AS s ON v.schema_id = s.schema_id;";
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
