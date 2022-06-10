using Common.Contracts.Model;
using Microsoft.AspNetCore.Mvc;
using BusinessProviders.Business;


namespace ASP.NET.CORE.WEBAPI.Controllers
{
    [ApiController]
    [Route("api/SignUp")]
    public class SignUpController : ControllerBase
    {
        //SignUpProvider business = new SignUpProvider();
        private readonly ISignUpProvider _signUpProvider;

        public SignUpController(ISignUpProvider signUpProvider)
        {
            _signUpProvider = signUpProvider;
            
        }


        [HttpGet]
        public List<Participant> GetALLParticipants()
        {
            return _signUpProvider.GetAllRecord();
        }

        [HttpGet]
        [Route("ID/{participantid}")]
        public ActionResult<List<Participant>> GetParticipantByID(int participantId)
        {
            var record = _signUpProvider.GetRecordById(participantId);

            if (record.Count() == 0)
            {
                return NotFound();
            }

            return Ok(_signUpProvider.GetRecordById(participantId));
        }

        [HttpPost]
        [Route("Post")]
        public ActionResult PostParticipants(Participant participant)
        {
            _signUpProvider.PostRecord(participant);

            return StatusCode(201);

        }

        [HttpDelete]
        [Route("DeleteAll")]
        public ActionResult DeleteAll()
        {
            _signUpProvider.DeleteAllRecord();

            return NoContent();
        }

        [HttpDelete]
        [Route("Delete/{participantid}")]
        public ActionResult DeleteParticipants(int participantId)
        {
            var record = _signUpProvider.GetRecordById(participantId);

            if (record.Count() == 0)
            {
                return NotFound();
            }

            _signUpProvider.DeleteRecordById(participantId);

            return NoContent();
        }

        [HttpPut]
        [Route("Update/{participantid}")]
        public ActionResult UpdateParticipants(int participantId, Participant participant)
        {
            var record = _signUpProvider.GetRecordById(participantId);

            if (record.Count() == 0)
            {
                return NotFound();
            }

            _signUpProvider.UpdateRecord(participantId, participant);

            return NoContent();
        }
    }
}
