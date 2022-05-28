using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;


namespace NewWebAPI
{
    public static class DatabaseManager
    {

       

        static string connectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;"; 
        public static SqlConnection CreatConnection()
        {
            var conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }
        public static void CreateDatabase()
        {
            using (var conn = CreatConnection())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'campformDB')
                                        BEGIN
                                        CREATE DATABASE campformDB;
                                        END;";
                    cmd.ExecuteNonQuery();
                }
            }
            CreateTable();
        }
        public static void CreateTable()
        {
          
            using (var conn = CreatConnection())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"USE campformDB IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SignUpData')
                        CREATE TABLE SignUpData (
	                      Id int IDENTITY(1,1) NOT NULL,
	                      FirstName varchar(100) NOT NULL,
                          LastName varchar(100) NOT NULL ,
                          IsFirstTime BIT,
	                      PRIMARY KEY (Id)
                );";

                    cmd.ExecuteNonQuery();

                }
            }
        }

    }
}
