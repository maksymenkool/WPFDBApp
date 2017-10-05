using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBClient
{
    public class DBData
    {
        public static DataTable GetDataBasesName(SqlConnection connection)
        {
            string query = @"SELECT name, create_date FROM sys.databases;";
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
