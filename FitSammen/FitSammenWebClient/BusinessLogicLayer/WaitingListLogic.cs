using FitSammenWebClient.Models;
using FitSammenWebClient.ServiceLayer;

namespace FitSammenWebClient.BusinessLogicLayer
{
    public class WaitingListLogic : IWaitingListLogic
    {
        private readonly IWaitingListAccess _waitingListAccess;

        
        public WaitingListLogic(IWaitingListAccess waitingListAccess)
        {
            _waitingListAccess = waitingListAccess;
        }

        public async Task<WaitingListEntryResponse?> AddMemberToWaitingListAsync(int classId, string token)
        {
            return await _waitingListAccess.AddMemberToWaitingListAsync(classId, token);
        }
    }
}
