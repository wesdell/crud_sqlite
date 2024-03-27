using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crud_sqlite.database
{
    public class Categories
    {
        public DataTable GetCategories()
        {
            SQLiteDataReader categories;
            DataTable dataTable = new DataTable();
            SQLiteConnection SQLiteConnection = new SQLiteConnection();

            try
            {
                SQLiteConnection = Connection.getConnection().Connect();

                String SQLQuery = "SELECT id, description FROM category;";
                SQLiteCommand SQLCommand = new SQLiteCommand(SQLQuery, SQLiteConnection);
                SQLiteConnection.Open();

                categories = SQLCommand.ExecuteReader();
                dataTable.Load(categories);

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

        public String GetCategoryById(int categoryId)
        {
            String categoryDescription;
            SQLiteConnection SQLiteConnection = new SQLiteConnection();

            try
            {
                SQLiteConnection = Connection.getConnection().Connect();

                String SQLQuery = "SELECT description FROM category WHERE id = @CategoryId";
                SQLiteCommand SQLCommand = new SQLiteCommand(SQLQuery, SQLiteConnection);
                SQLCommand.Parameters.AddWithValue("@CategoryId", categoryId);
                SQLiteConnection.Open();

                categoryDescription = SQLCommand.ExecuteScalar().ToString();
                return categoryDescription;
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
