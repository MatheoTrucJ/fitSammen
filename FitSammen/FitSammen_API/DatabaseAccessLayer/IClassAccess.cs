using FitSammen_API.Model;

namespace FitSammen_API.DatabaseAccessLayer
{
    public interface IClassAccess
    {
        public IEnumerable<Class> MemberGetAllClasses();
    }
}
