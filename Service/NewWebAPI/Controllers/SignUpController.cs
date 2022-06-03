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
            return DatabaseManager.LoadParticipantsFromDB();
        }

        [HttpGet]
        [Route("GetByID/{participantid}")]
        public List<Participant>GetParticipantByID(int participantId)
        {
            return DatabaseManager.LoadParticipantsFromDB().Where(p => p.Id == participantId).ToList();
        }

        [HttpPost]
        [Route("Post")]
        public string PostParticipants(Participant participant)
        {

            return DatabaseManager.InsertData(participant);
 
        }

        [HttpDelete]
        [Route("DeleteAll")]
        public string DeleteAll()
        {           
            return DatabaseManager.DeleteAllData();     
        }


        [HttpDelete]
        [Route("Delete/{participantid}")]
        public string DeleteParticipants(int participantId)
        {
            return DatabaseManager.DeleteById(participantId);
        }

        [HttpPut]
        [Route("Update/{participantid}")]
        public string UpdateParticipants(int participantId, Participant participant)
        {
            return DatabaseManager.UpdateData(participantId, participant);  
        }

       
    }
}
