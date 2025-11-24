using FitSammenWebClient.Models;
using Newtonsoft.Json;
using System.Text;

namespace FitSammenWebClient.ServiceLayer
{
    public class WaitingListService : ServiceConnection, IWaitingListAccess
    {

        public WaitingListService(IConfiguration inBaseUrl)
            : base(inBaseUrl["ServiceUrlToUse"])
        {
        }

        public async Task<WaitingListEntryResponse> AddMemberToWaitingListAsync(int classId, int memberId)
        {
            WaitingListEntryResponse? responseDto = null;

            UseUrl = BaseUrl;
            UseUrl += $"classes/{classId}/waitinglists";

            WaitingListEntryRequest request = new WaitingListEntryRequest
            {
                MemberId = memberId
            };

            try
            {
                string json = JsonConvert.SerializeObject(request);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage? serviceResponse = await CallServicePost(content);

                if (serviceResponse != null)
                {
                    string responseContent = await serviceResponse.Content.ReadAsStringAsync();
                    responseDto = JsonConvert.DeserializeObject<WaitingListEntryResponse>(responseContent);
                }

                if (serviceResponse != null && responseDto != null)
                {
                    return responseDto;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in AddMemberToWaitingListAsync: " + ex.Message);
                throw;
            }
            return new WaitingListEntryResponse
            {
                Status = WaitingListStatus.Error,
                WaitingListPosition = null
            };
        }
    }
}
