﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CineBase
{
    public class Database
    {
        public static SqlConnection db;
        public static string Username = string.Empty;

        public static bool SetDb(string connectionString)
        {
            try
            {
                if (db != null)
                    db.Close();
                db = new SqlConnection(connectionString);
                db.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void Add(string table, string columns, string values)
        {
            if (db != null)
            {
                string query = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", table, columns, values);
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.ExecuteNonQuery();
            }
        }

        public static void Update(string table, string values, string condition)
        {
            if (db != null)
            {
                string query = string.Format("UPDATE {0} SET {1} WHERE {2}", table, values, condition);
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.ExecuteNonQuery();
            }
        }

        public static int GetLast(string table)
        {
            if (db != null)
            {
                try
                {
                    string query = string.Format("SELECT [Id] FROM [{0}] ORDER BY [Id] DESC", table);
                    SqlCommand cmd = new SqlCommand(query, db);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    return reader.GetFieldValue<int>(0);
                }
                catch
                {
                    return 0;
                }
            }

            return -1;
        }
    }
}
