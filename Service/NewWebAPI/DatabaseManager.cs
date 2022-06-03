using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using NewWebAPI.Models;

namespace NewWebAPI
{
    public class DatabaseManager
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

        public static List<Participant> LoadParticipantsFromDB()
        {
            List<Participant> participants = new List<Participant>();

            using (var conn = DatabaseManager.CreatConnection())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $" USE campformDB SELECT * FROM SignUpData";

                    var rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        int id = rdr.GetInt32(0);
                        string firstName = rdr.GetString(1);
                        string lastName = rdr.GetString(2);
                        bool isFirstTime = rdr.GetBoolean(3);

                        participants.Add(new Participant()
                        {
                            Id = id,
                            FirstName = firstName,
                            LastName = lastName,
                            IsFirstTime = isFirstTime
                        });
                    }
                }
            }
            return participants;
        }

        public static string InsertData(Participant participant)
        {
            using (var conn = CreatConnection())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $@"USE campformDB INSERT INTO SignUpData (FirstName, LastName, IsFirstTime) Values(@FirstName, @LastName, @IsFirstTime)";

                    cmd.Parameters.AddWithValue("@FirstName", participant.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", participant.LastName);
                    cmd.Parameters.AddWithValue("@IsFirstTime", participant.IsFirstTime);

                    cmd.ExecuteNonQuery();
                }
            }
            return "Participant Added Successfully";
        }

        public static string DeleteAllData()
        {
            using (var conn = CreatConnection())
            {
                using (var cmd = conn.CreateCommand())
                {

                    cmd.CommandText = $"USE campformDB DELETE FROM SignUpData";
                    cmd.ExecuteNonQuery();
                }
            }
            return "Participants Deleted Successfully";
        }

        public static string DeleteById(int participantId)
        {
            var checkDatabase = LoadParticipantsFromDB().Where(p => p.Id == participantId).ToList();

            if (checkDatabase.Count() == 0)
            {
                return "Id Not Found";
            }
            using (var conn = CreatConnection())
            {
                using (var cmd = conn.CreateCommand())
                {

                    cmd.CommandText = $"USE campformDB DELETE FROM SignUpData WHERE Id =" + participantId + "";
                    cmd.ExecuteNonQuery();
                }
            }
            return "Participant Deleted Successfully";
        }

        public static string UpdateData(int participantId, Participant participant)
        {
            var checkDatabase = LoadParticipantsFromDB().Where(P => P.Id == participantId).ToList();

            if (checkDatabase.Count() == 0)
            {
                return "Id Not Found";
            }
            using (var conn = CreatConnection())
            {
                using (var cmd = conn.CreateCommand())
                {

                    cmd.CommandText = $"USE campformDB UPDATE SignUpData SET FirstName = @FirstName, LastName = @LastName, IsFirstTime = @IsFirstTime  WHERE Id = {participantId}";
                    cmd.Parameters.AddWithValue("@FirstName", participant.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", participant.LastName);
                    cmd.Parameters.AddWithValue("@IsFirstTime", participant.IsFirstTime);

                    cmd.ExecuteNonQuery();
                }
            }
            return "Participant Updated Successfully";
        }

    }
}
