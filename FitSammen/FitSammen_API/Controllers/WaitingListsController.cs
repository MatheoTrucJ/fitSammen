using FitSammen_API.BusinessLogicLayer;
using FitSammen_API.DTOs;
using FitSammen_API.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FitSammen_API.Controllers
{
    [Route("api/classes/{classId}/waitinglists")]
    [ApiController]
    [Authorize]
    public class WaitingListsController : ControllerBase
    {

        private readonly IWaitingListService _waitingListService;
        public WaitingListsController(IWaitingListService waitingListService)
        {
            _waitingListService = waitingListService;
        }

        [HttpPost]
        public ActionResult<WaitingListEntryResponseDTO> CreateWaitingListEntry(int classId, [FromBody] WaitingListEntryRequestDTO wleRequest)
        {
            WaitingListResult result = _waitingListService.AddMemberToWaitingList(classId, wleRequest.MemberId);

            WaitingListEntryResponseDTO wleResponseDTO = ModelConversion.ToWaitingListEntryResponseDTO(result);

            return result.Status switch
            {
                WaitingListStatus.Success => Created(string.Empty, wleResponseDTO),
                WaitingListStatus.AlreadySignedUp => Conflict(wleResponseDTO),
                WaitingListStatus.Error => StatusCode(500, wleResponseDTO),
                _ => StatusCode(500, wleResponseDTO)
            };
        }
    }
}
