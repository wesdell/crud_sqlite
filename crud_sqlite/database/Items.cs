using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Data.SQLite;
using crud_sqlite.models;

namespace crud_sqlite.database
{
    public class Items
    {
        public DataTable GetItemsByName(String item)
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

        public String CreateUpdateItem(int saveAction, Item item)
        {
            String response;
            String SQLQuery = "";
            SQLiteConnection SQLiteConnection = new SQLiteConnection();

            try
            {
                SQLiteConnection = Connection.getConnection().Connect();

                if (saveAction == 1)
                {
                    SQLQuery = "INSERT INTO item (description, brand, measure_id, category_id) " +
                        "VALUES ('" + item.Description + "', '" + item.Brand + "', '" + item.Measure_Id + "', '" + item.Category_Id + "');";
                }
                else
                {
                    SQLQuery = "";
                }


                SQLiteCommand SQLCommand = new SQLiteCommand(SQLQuery, SQLiteConnection);
                SQLiteConnection.Open();
                response = SQLCommand.ExecuteNonQuery() > 0 ? "Success" : "Failed";
            }
            catch (Exception e)
            {
                response = e.Message;
            }
            finally
            {
                if (SQLiteConnection.State == ConnectionState.Open)
                {
                    SQLiteConnection.Close();
                }
            }

            return response;
        }
    }
}
