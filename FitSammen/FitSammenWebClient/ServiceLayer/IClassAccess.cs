using FitSammen_API.Model;

namespace FitSammenWebClient.ServiceLayer
{
    public interface IClassAccess
    {
        Task<IEnumerable<Class>?> GetClasses(int id = -1);
    }
}
