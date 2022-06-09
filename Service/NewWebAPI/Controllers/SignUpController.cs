using Common.Contracts.Model;
using Microsoft.AspNetCore.Mvc;
using BusinessProviders.Business;


namespace ASP.NET.CORE.WEBAPI.Controllers
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
        [Route("ID/{participantid}")]
        public ActionResult<List<Participant>> GetParticipantByID(int participantId)
        {
            var record = business.GetRecordById(participantId);

            if (record.Count() == 0)
            {
                return NotFound();
            }

            return Ok(business.GetRecordById(participantId));
        }

        [HttpPost]
        [Route("Post")]
        public ActionResult PostParticipants(Participant participant)
        {
            business.PostRecord(participant);

            return StatusCode(201);

        }

        [HttpDelete]
        [Route("DeleteAll")]
        public ActionResult DeleteAll()
        {
            business.DeleteAllRecord();

            return NoContent();
        }

        [HttpDelete]
        [Route("Delete/{participantid}")]
        public ActionResult DeleteParticipants(int participantId)
        {
            var record = business.GetRecordById(participantId);

            if (record.Count() == 0)
            {
                return NotFound();
            }

            business.DeleteRecordById(participantId);

            return NoContent();
        }

        [HttpPut]
        [Route("Update/{participantid}")]
        public ActionResult UpdateParticipants(int participantId, Participant participant)
        {
            var record = business.GetRecordById(participantId);

            if (record.Count() == 0)
            {
                return NotFound();
            }

            business.UpdateRecord(participantId, participant);

            return NoContent();  
        }   
    }
}
