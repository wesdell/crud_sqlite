using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace crud_sqlite.database
{
    public class Connection
    {
        private string _connectionString;
        private static Connection _connection = null;

        private Connection()
        {
            this._connectionString = "./database.db";
        }

        public SQLiteConnection Connect()
        {
            SQLiteConnection connection = new SQLiteConnection();

            try
            {
                connection.ConnectionString = "Data Source=" + this._connectionString;
            }
            catch (Exception error)
            {
                connection = null;
                throw error;
            }
            return connection;
        }

        public static Connection getConnection()
        {
            if (_connection == null)
            {
                _connection = new Connection();
            }
            return _connection;
        }
    }
}
