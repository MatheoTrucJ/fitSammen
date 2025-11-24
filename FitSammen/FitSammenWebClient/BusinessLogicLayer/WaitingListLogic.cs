using FitSammenWebClient.Models;
using FitSammenWebClient.ServiceLayer;

namespace FitSammenWebClient.BusinessLogicLayer
{
    public class WaitingListLogic
    {
        private readonly IWaitingListAccess _waitingListAccess;

        // Production constructor keeps existing behavior
        public WaitingListLogic(IConfiguration inConfiguration)
        {
            _waitingListAccess = new WaitingListService(inConfiguration);
        }

        // Test/DI friendly constructor
        public WaitingListLogic(IWaitingListAccess waitingListAccess)
        {
            _waitingListAccess = waitingListAccess;
        }

        public async Task<WaitingListEntryResponse?> AddMemberToWaitingListAsync(int classId, int memberNumber)
        {
            return await _waitingListAccess.AddMemberToWaitingListAsync(classId, memberNumber);
        }
    }
}
