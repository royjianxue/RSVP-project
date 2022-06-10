using Common.Contracts.Model;

namespace BusinessProviders.Business
{
    public interface ISignUpProvider
    {
        void DeleteAllRecord();
        void DeleteRecordById(int participantId);
        List<Participant> GetAllRecord();
        List<Participant> GetRecordById(int participantId);
        void PostRecord(Participant participant);
        void UpdateRecord(int participantId, Participant participant);
    }
}