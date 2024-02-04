﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crud_sqlite.database
{
    public class Measures
    {
        public DataTable GetMeasures()
        {
            SQLiteDataReader measures;
            DataTable dataTable = new DataTable();
            SQLiteConnection SQLiteConnection = new SQLiteConnection();

            try
            {
                SQLiteConnection = Connection.getConnection().Connect();

                String SQLQuery = "SELECT id, description FROM measure;";
                SQLiteCommand SQLCommand = new SQLiteCommand(SQLQuery, SQLiteConnection);
                SQLiteConnection.Open();

                measures = SQLCommand.ExecuteReader();
                dataTable.Load(measures);

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
