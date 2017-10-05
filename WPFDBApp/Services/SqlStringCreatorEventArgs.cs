using System;

namespace WPFDBApp.Services
{
    public class SqlStringCreatorEventArgs : EventArgs
    {
        public string ServerConnectionString { get; }

        public SqlStringCreatorEventArgs(string connectString)
        {
            ServerConnectionString = connectString;
        }
    }
}
