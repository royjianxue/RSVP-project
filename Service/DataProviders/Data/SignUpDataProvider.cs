using Microsoft.Data.SqlClient;
using Common.Contracts.Model;

namespace DataProviders.Data
{
    public class SignUpDataProvider : ISignUpDataProvider
    {
        private readonly string connectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;";

        public SqlConnection CreatConnection()
        {
            var conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }
        public void CreateDatabase()
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
        public void CreateTable()
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

        public List<Participant> LoadParticipantsFromDB()
        {
            List<Participant> participants = new List<Participant>();

            using (var conn = CreatConnection())
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

        public void InsertData(Participant participant)
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
        }

        public void DeleteAllData()
        {
            using (var conn = CreatConnection())
            {
                using (var cmd = conn.CreateCommand())
                {

                    cmd.CommandText = $"USE campformDB DELETE FROM SignUpData";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteById(int participantId)
        {
            using (var conn = CreatConnection())
            {
                using (var cmd = conn.CreateCommand())
                {

                    cmd.CommandText = $"USE campformDB DELETE FROM SignUpData WHERE Id =" + participantId + "";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateData(int participantId, Participant participant)
        {
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
        }

    }
}
