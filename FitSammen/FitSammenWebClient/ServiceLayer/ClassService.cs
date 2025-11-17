using FitSammenWebClient.Models;
using Newtonsoft.Json;

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
            UseUrl += "Class";

            bool hasValidId = (id > 0);
            if (hasValidId)
            {
                UseUrl += "/" + id.ToString();
            }
            try
            {
                var response = await CallServiceGet();
                bool wasResponse = (response != null);

                if (wasResponse && response != null && response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
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
    }
}
