using Microsoft.AspNetCore.Mvc;
using NewWebAPI.Models;
using System.Data;

namespace NewWebAPI.Controllers
{
    [ApiController]
    [Route("api/SignUp")]
    public class SignUpController : ControllerBase
    {
        [HttpGet]
        public List<Participant> GetALLParticipants()
        {
            return LoadParticipantsFromDB();
        }

        [HttpGet]
        [Route("GetByID/{participantid}")]
        public List<Participant>GetParticipantByID(int participantId)
        {
            return LoadParticipantsFromDB().Where(p => p.Id == participantId).ToList();
        }

        [HttpPost]
        [Route("Post")]
        public string PostParticipants(Participant participant)
        {

            using (var conn = DatabaseManager.CreatConnection())
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

        [HttpDelete]
        [Route("DeleteAll")]
        public string DeleteAll()
        {
            using (var conn = DatabaseManager.CreatConnection())
            {
                using (var cmd = conn.CreateCommand())
                {

                    cmd.CommandText = $"USE campformDB DELETE FROM SignUpData";
                    cmd.ExecuteNonQuery();
                }
            }
            return "Participants Deleted Successfully";
        }


        [HttpDelete]
        [Route("Delete/{participantid}")]
        public string DeleteParticipants(int participantId)
        {
            var checkDatabase = LoadParticipantsFromDB().Where(p => p.Id == participantId).ToList();

            if (checkDatabase.Count() == 0)
            {
                return "Id Not Found";
            }
            using (var conn = DatabaseManager.CreatConnection())
            {
                using (var cmd = conn.CreateCommand())
                {

                    cmd.CommandText = $"USE campformDB DELETE FROM SignUpData WHERE Id =" + participantId + "";
                    cmd.ExecuteNonQuery();
                }
            }
            return "Participant Deleted Successfully";
        }

        [HttpPut]
        [Route("Update/{participantid}")]
        public string UpdateParticipants(int participantId, Participant participant)
        {
            var checkDatabase = LoadParticipantsFromDB().Where(P => P.Id == participantId).ToList();

            if (checkDatabase.Count() == 0)
            {
                return "Id Not Found";
            }
            using (var conn = DatabaseManager.CreatConnection())
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

        private List<Participant> LoadParticipantsFromDB()
        {
            List<Participant> participants = new List<Participant>();

            using (var conn = DatabaseManager.CreatConnection())
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $" USE campformDB SELECT * FROM SignUpData";

                    var rdr = cmd.ExecuteReader(); 

                    while(rdr.Read())
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
    }
}
