using FitSammenWebClient.Models;
using System.Net;
using System.Text;
using System.Text.Json;

namespace FitSammenWebClient.ServiceLayer
{
    public class LoginService : ServiceConnection, ILoginService
    {
        public LoginService(IConfiguration inBaseUrl) : base(inBaseUrl["ServiceUrlToUse"])
        {

        }
        public async Task<LoginResponseModel?> GetTokenFromApiAsync(string email, string password)
        {
            UseUrl = BaseUrl;
            UseUrl += "/tokens";

            try
            {
                var loginRequest = new
                {
                    Email = email,
                    Password = password
                };
                string jsonContent = JsonSerializer.Serialize(loginRequest);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage? response = await CallServicePost(content);
                if (response != null && response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonSerializer.Deserialize<LoginResponseModel>(responseBody,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return loginResponse;
                }
                else if (response != null && (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.BadRequest))
                {
                    return null;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Exception in GetTokenFromApiAsync", ex);
            }
        }
    }
}
