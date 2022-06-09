using Common.Contracts.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using BusinessProviders.Business;

namespace NewWebAPI.Controllers
{
    [ApiController]
    [Route("api/SignUp")]
    public class SignUpController : ControllerBase
    {
        BusinessLogic business = new BusinessLogic();

        [HttpGet]
        public List<Participant> GetALLParticipants()
        {
            return business.GetAllRecord();
        }

        [HttpGet]
        [Route("GetByID/{participantid}")]
        public List<Participant>GetParticipantByID(int participantId)
        {
            return business.GetRecordById(participantId);
        }

        [HttpPost]
        [Route("Post")]
        public void PostParticipants(Participant participant)
        {
            business.PostRecord(participant);
 
        }

        [HttpDelete]
        [Route("DeleteAll")]
        public void DeleteAll()
        {
            business.DeleteAllRecord();
        }


        [HttpDelete]
        [Route("Delete/{participantid}")]
        public void DeleteParticipants(int participantId)
        {
            business.DeleteRecordById(participantId);
        }

        [HttpPut]
        [Route("Update/{participantid}")]
        public void UpdateParticipants(int participantId, Participant participant)
        {
            business.UpdateRecord(participantId, participant);  
        }

       
    }
}
