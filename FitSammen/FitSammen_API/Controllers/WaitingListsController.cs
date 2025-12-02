using FitSammen_API.BusinessLogicLayer;
using FitSammen_API.DTOs;
using FitSammen_API.Mapping;
using FitSammen_API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Authorize(Roles = nameof(UserType.Member))]
        public ActionResult<WaitingListEntryResponseDTO> CreateWaitingListEntry(int classId)
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (id == null)
            {
                return Unauthorized("User identifier not found.");
            }

            int userId = int.Parse(id);


            WaitingListResult result = _waitingListService.AddMemberToWaitingList(classId, userId);

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
