using FitSammenWebClient.Models;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace FitSammenWebClient.ServiceLayer
{
    public class WaitingListService : ServiceConnection, IWaitingListAccess
    {

        public WaitingListService(HttpClient httpClient, IConfiguration inBaseUrl)
            : base(httpClient, inBaseUrl["ServiceUrlToUse"])
        {
        }

        public async Task<WaitingListEntryResponse> AddMemberToWaitingListAsync(int classId, int memberId, string token)
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
                _httpEnabler.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string json = JsonConvert.SerializeObject(request);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage? serviceResponse = await CallServicePost(content);
                _httpEnabler.DefaultRequestHeaders.Authorization = null;

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
                _httpEnabler.DefaultRequestHeaders.Authorization = null;
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
