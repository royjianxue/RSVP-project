using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Contracts.Model;
using DataProviders.Data;

namespace BusinessProviders.Business
{
    public class BusinessLogic
    {
        public List<Participant> GetAllRecord()
        {
            return DatabaseManager.LoadParticipantsFromDB();
        }
        public List<Participant> GetRecordById(int participantId)
        {
           return DatabaseManager.LoadParticipantsFromDB().Where(p => p.Id == participantId).ToList();
        }

        public void PostRecord(Participant participant)
        {
            DatabaseManager.InsertData(participant);
        }

        public void DeleteAllRecord()
        {
            DatabaseManager.DeleteAllData();
        }

        public void DeleteRecordById(int participantId)
        {

           DatabaseManager.DeleteById(participantId);
        
        }

        public void UpdateRecord(int participantId, Participant participant)
        {
            var checkDatabase = DatabaseManager.LoadParticipantsFromDB().Where(P => P.Id == participantId).ToList();

            if (checkDatabase.Count() == 0)
            {
                //return "Id Not Found";
            }
            else
            {
                DatabaseManager.UpdateData(participantId, participant);
            }
        }

    }
}
