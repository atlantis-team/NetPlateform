using MySql.Data;
using MySql.Data.MySqlClient;
using System;

namespace Database
{
    public class DBConnection
    {
        private DBConnection()
        {
        }

        private string databaseName = "calculatedmetrics";
        public string DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }

        private string password = string.Empty;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private MySqlConnection connection = null;
        public MySqlConnection Connection
        {
            get { return connection; }
        }

        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }

        public bool IsConnect()
        {
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(databaseName))
                    return false;
                string connstring = string.Format("Server=localhost; database={0}; UID=root; password={1}", databaseName, password);
                connection = new MySqlConnection(connstring);
                connection.Open();
            }

            return true;
        }

        public void Close()
        {
            connection.Close();
        }

        public MySqlDataReader ExecuteQuery(string query)
        {
            var cmd = new MySqlCommand(query, Connection);
            var reader = cmd.ExecuteReader();
            return reader;
        }
    }
}