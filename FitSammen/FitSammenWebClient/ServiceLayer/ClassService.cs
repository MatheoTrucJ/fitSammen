using FitSammenWebClient.Models;
using Newtonsoft.Json;
using System.Text;

namespace FitSammenWebClient.ServiceLayer
{
    public class ClassService : ServiceConnection, IClassAccess
    {
        public ClassService(IConfiguration inBaseUrl) : base(inBaseUrl["ServiceUrlToUse"])
        {

        }

        public async Task<IEnumerable<Class>?> GetClasses(int id = -1)
        {
            List<Class>? result = new List<Class>();

            UseUrl = BaseUrl;
            UseUrl += "classes";

            bool hasValidId = (id > 0);
            if (hasValidId)
            {
                UseUrl += "/" + id.ToString();
            }

            try
            {
                HttpResponseMessage? response = await CallServiceGet();
                bool wasResponse = (response != null);

                if (wasResponse && response != null && response.IsSuccessStatusCode)
                {
                    string? content = await response.Content.ReadAsStringAsync();
                    if (hasValidId)
                    {
                        Class? foundClass = JsonConvert.DeserializeObject<Class>(content);
                        if (foundClass != null)
                        {
                            result = new List<Class>();
                            result.Add(foundClass);
                        }
                    }
                    else
                    {
                        result = JsonConvert.DeserializeObject<List<Class>>(content);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in GetClasses: " + ex.Message);
                throw;
            }

            return result;
        }

        public async Task<MemberBookingResponse> SignUpMemberToClass(int userNumber, int classId)
        {
            MemberBookingResponse? Reponse = null;

            UseUrl = BaseUrl;
            UseUrl += "classes/" + classId + "/bookings";

            MemberBookingRequest request = new MemberBookingRequest
            {
                MemberId = userNumber,
            };

            try
            {
                string? json = JsonConvert.SerializeObject(request);
                StringContent? content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage? serviceResponse = await CallServicePost(content);

                if (serviceResponse != null)
                {
                    string? responseContent = await serviceResponse.Content.ReadAsStringAsync();
                    Reponse = JsonConvert.DeserializeObject<MemberBookingResponse>(responseContent);
                }

                if (serviceResponse != null && Reponse != null)
                {
                    return Reponse;
                }
            }
            catch
            {

            }
            return new MemberBookingResponse
            {
                BookingId = 0,
                Status = "Failed",
                Message = "Booking could not be completed."
            };
        }
    }
}
