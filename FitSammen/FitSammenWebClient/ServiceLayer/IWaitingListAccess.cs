using FitSammenWebClient.Models;

namespace FitSammenWebClient.ServiceLayer
{
    public interface IWaitingListAccess
    {
        Task<WaitingListEntryResponse> AddMemberToWaitingListAsync(int classId, string token);
    }
}
