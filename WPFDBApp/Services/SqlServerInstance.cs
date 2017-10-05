using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;

namespace WPFDBApp.Services
{
    /// <summary>
    /// A class that helps get local/network servers.
    /// </summary>
    public class SqlServerInstance
    {
        #region Public Properties

        public static List<string> SqlLocalInstances
        {
            get { return GetLocalSqlServerInstanceNames(); }
        }

        public static List<string> SqlNetWorkInstances
        {
            get { return GetNetWorkInstanceNames(); }
        }

        #endregion

        #region Helper Methods

        private static List<string> GetNetWorkInstanceNames()
        {
            List<string> netWorkInstanceNames = new List<string>();
            System.Data.Sql.SqlDataSourceEnumerator instance = System.Data.Sql.SqlDataSourceEnumerator.Instance;
            DataTable dataTable = instance.GetDataSources();
            foreach (DataRow row in dataTable.Rows)
            {
                string instanceName = String.Format(@"{0}\{1}", row["ServerName"].ToString(), row["InstanceName"].ToString());

                if (!netWorkInstanceNames.Contains(instanceName) && !instanceName.Contains(Environment.MachineName))
                {
                    netWorkInstanceNames.Add(instanceName);
                }
            }
            netWorkInstanceNames.Sort();
            return netWorkInstanceNames;
        }

        private static List<string> GetLocalSqlServerInstanceNames()
        {
            List<string> localInstanceNames = new List<string>();
            RegistryView registryView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;
            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView))
            {
                RegistryKey instanceKey = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL", false);
                if (instanceKey != null)
                {
                    foreach (var instanceName in instanceKey.GetValueNames())
                    {
                        string name = String.Format(@"{0}\{1}", Environment.MachineName, instanceName);

                        if (!localInstanceNames.Contains(name))
                            localInstanceNames.Add(name);
                    }
                }
            }
            localInstanceNames.Sort();
            return localInstanceNames;
        }

        #endregion
    }
}
