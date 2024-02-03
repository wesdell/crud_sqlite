using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Data.SQLite;

namespace crud_sqlite.database
{
    public class Items
    {
        public DataTable GetItems(String item)
        {
            SQLiteDataReader items;
            DataTable dataTable = new DataTable();
            SQLiteConnection SQLiteConnection = new SQLiteConnection();

            try
            {
                SQLiteConnection = Connection.getConnection().Connect();

                item = "%" + item.Trim() + "%";
                String SQLQuery = "SELECT i.id, i.description, i.brand, i.measure_id, i.category_id, m.description AS measure, c.description AS category " +
                    "FROM item i " +
                    "INNER JOIN measure m " +
                    "ON i.measure_id = m.id " +
                    "INNER JOIN category c " +
                    "ON i.category_id = c.id " +
                    "WHERE i.description LIKE '" + item + "';";
                SQLiteCommand SQLCommand = new SQLiteCommand(SQLQuery, SQLiteConnection);
                SQLiteConnection.Open();

                items = SQLCommand.ExecuteReader();
                dataTable.Load(items);

                return dataTable;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (SQLiteConnection.State == ConnectionState.Open)
                {
                    SQLiteConnection.Close();
                }
            }
        }
    }
}
