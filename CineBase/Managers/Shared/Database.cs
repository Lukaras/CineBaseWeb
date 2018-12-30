using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CineBase
{
    public class Database
    {
	public static SqlConnection db;
	
	public static void SetDb(string connectionString)
	{
	    if (db != null)
		db.Close();
	    db = new SqlConnection(connectionString);
	    db.Open();
	}

	public static void Add(string table, string columns, string values) {
	    if (db != null)
	    {
		string query = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", table, columns, values);
		SqlCommand cmd = new SqlCommand(query, db);
		cmd.ExecuteNonQuery();
	    }
	}

	public static int GetLast(string table)
	{
	    if (db != null)
	    {
		string query = string.Format("SELECT [Id] FROM [{0}] ORDER BY [Id] DESC", table);
		SqlCommand cmd = new SqlCommand(query, db);
		SqlDataReader reader = cmd.ExecuteReader();
		reader.Read();
		return reader.GetFieldValue<int>(0);
	    }
	    return -1;
	}
    }
}
