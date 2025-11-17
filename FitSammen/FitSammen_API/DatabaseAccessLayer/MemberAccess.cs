using FitSammen_API.Model;

namespace FitSammen_API.DatabaseAccessLayer
{
    public class MemberAccess : IMemberAccess
    {
        public MemberAccess(IConfiguration inConfiguration)
        {
            // From configuration data get name of conn-string - and then fetch the conn-string
            string? useConnectionString = inConfiguration["ConnectionStringToUse"];
            ConnectionString = useConnectionString is not null ? inConfiguration.GetConnectionString(useConnectionString) : null;
        }

        public string? ConnectionString { get; }
    }
}
