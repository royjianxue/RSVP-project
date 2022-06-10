using Common.Contracts.Model;
using Microsoft.Data.SqlClient;

namespace DataProviders.Data
{
    public interface ISignUpDataProvider
    {
        SqlConnection CreatConnection();
        void CreateDatabase();
        void CreateTable();
        void DeleteAllData();
        void DeleteById(int participantId);
        void InsertData(Participant participant);
        List<Participant> LoadParticipantsFromDB();
        void UpdateData(int participantId, Participant participant);
    }
}