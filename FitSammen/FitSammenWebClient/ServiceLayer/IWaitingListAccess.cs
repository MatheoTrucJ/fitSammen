using FitSammenWebClient.Models;

namespace FitSammenWebClient.ServiceLayer
{
    public interface IWaitingListAccess
    {
        Task<WaitingListEntryResponse> AddMemberToWaitingListAsync(int classId, int memberNumber, string token);
    }
}
