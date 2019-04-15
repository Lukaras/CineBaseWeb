using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DbBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            using (StreamReader reader = new StreamReader("connectionString.txt"))
            {
                string connectionString = reader.ReadLine();
                SqlConnection db = new SqlConnection(connectionString);
                db.Open();
                string query = string.Format("CREATE TABLE [Comment] ([Id] int PRIMARY KEY, [UserId] int NOT NULL, [MovieId] int NOT NULL, [Comment] nvarchar(MAX));");
                SqlCommand cmd = new SqlCommand(query, db);
                cmd.ExecuteNonQuery();
                query = string.Format("CREATE TABLE [Creators] ([Id] int PRIMARY KEY, [MovieId] int NOT NULL, [PersonId] int NOT NULL, [Type] int NOT NULL);");
                cmd = new SqlCommand(query, db);
                cmd.ExecuteNonQuery();
                query = string.Format("CREATE TABLE [List] ([Id] int PRIMARY KEY, [ListType] int NOT NULL, [Content] nvarchar(MAX));");
                cmd = new SqlCommand(query, db);
                cmd.ExecuteNonQuery();
                query = string.Format("CREATE TABLE [Movie] ([Id] int PRIMARY KEY, [Title] nvarchar(MAX) NOT NULL, [OriginalTitle] nvarchar(MAX), [Description] nvarchar(MAX), [Genre] int NOT NULL);");
                cmd = new SqlCommand(query, db);
                cmd.ExecuteNonQuery();
                query = string.Format("CREATE TABLE [Person] ([Id] int PRIMARY KEY, [Firstname] nvarchar(MAX) NOT NULL, [Lastname] nvarchar(MAX) NOT NULL, [Birthdate] date, [Deathdate] date, [Birthplace] nvarchar(MAX), [Bio] nvarchar(MAX));");
                cmd = new SqlCommand(query, db);
                cmd.ExecuteNonQuery();
                query = string.Format("CREATE TABLE [Rating] ([Id] int PRIMARY KEY, [MovieId] int NOT NULL, [UserId] int NOT NULL, [Rating] int NOT NULL);");
                cmd = new SqlCommand(query, db);
                cmd.ExecuteNonQuery();
                query = string.Format("CREATE TABLE [User] ([Id] int PRIMARY KEY, [Username] nvarchar(MAX) NOT NULL, [Password] nvarchar(MAX) NOT NULL, [PasswordSalt] nvarchar(MAX) NOT NULL, [Created] datetime, [Question] int, [Answer] nvarchar(MAX), [Type] int NOT NULL);");
                cmd = new SqlCommand(query, db);
                cmd.ExecuteNonQuery();
                string enc = System.Text.Encoding.ASCII.GetString(SHA256.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes("adminABCD")));
                query = string.Format("INSERT INTO [User] VALUES (1, 'admin', '{0}', 'ABCD', null, 0, null, 2)", enc);
                cmd = new SqlCommand(query, db);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
