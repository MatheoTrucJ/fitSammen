using FitSammenDekstopClient.Model;
using FitSammenDesktopClient.ServiceLayer;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitSammenDekstopClient.ServiceLayer
{
    internal class ClassService : ServiceConnection, IClassService
    {
        public ClassService(IConfiguration inBaseUrl) : base(inBaseUrl["ServiceUrlToUse"]) { }

        public async Task<IEnumerable<Class>?> GetAllClassesAsync()
        {
            List<Class>? result = new List<Class>();
            UseUrl = BaseUrl + "classes";

            try
            {
                HttpResponseMessage? response = await CallServiceGet();

                if (response != null && response.IsSuccessStatusCode)
                {
                    string? content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<List<Class>>(content);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public async Task<CreateClassResponse?> CreateClassAsync(CreateClassRequest request)
        {
            UseUrl = BaseUrl + "classes";

            try
            {

                string requestAsJson = JsonConvert.SerializeObject(request);
                StringContent content = new StringContent(requestAsJson, Encoding.UTF8, "application/json");

                HttpResponseMessage? response = await CallServicePost(content);

                if (response != null && response.IsSuccessStatusCode)
                {
                    string? responseContent = await response.Content.ReadAsStringAsync();
                    var createdClass = JsonConvert.DeserializeObject<CreateClassResponse>(responseContent);

                    if (createdClass != null)
                    {
                        return createdClass;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return new CreateClassResponse
            {
                Status = CreateClassStatus.Error,
                Message = "Fejl ved oprettelse af hold.",
                ClassId = 0,
                CreatedClass = null
            };
        }
    }
}
