using Common.Contracts.Model;
using DataProviders.Data;

namespace BusinessProviders.Business
{
    public class SignUpProvider : ISignUpProvider
    {
        private readonly ISignUpDataProvider _signUpDataProvider;

        public SignUpProvider(ISignUpDataProvider signUpDataProvider)
        {
            _signUpDataProvider = signUpDataProvider;
            _signUpDataProvider.CreateDatabase();
        }

        public List<Participant> GetAllRecord()
        {
            return _signUpDataProvider.LoadParticipantsFromDB();
        }
        public List<Participant> GetRecordById(int participantId)
        {
            return _signUpDataProvider.LoadParticipantsFromDB().Where(p => p.Id == participantId).ToList();
        }
        public void PostRecord(Participant participant)
        {
            _signUpDataProvider.InsertData(participant);
        }
        public void DeleteAllRecord()
        {
            _signUpDataProvider.DeleteAllData();
        }
        public void DeleteRecordById(int participantId)
        {
            _signUpDataProvider.DeleteById(participantId);
        }
        public void UpdateRecord(int participantId, Participant participant)
        {
            _signUpDataProvider.UpdateData(participantId, participant);
        }
    }
}
