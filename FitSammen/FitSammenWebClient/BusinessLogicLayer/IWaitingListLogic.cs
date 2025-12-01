using FitSammenWebClient.Models;

namespace FitSammenWebClient.BusinessLogicLayer
{
    public interface IWaitingListLogic
    {
        Task<WaitingListEntryResponse?> AddMemberToWaitingListAsync(int classId, int memberNumber, string token);
    }
}
