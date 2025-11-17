using FitSammen_API.Model;

namespace FitSammen_API.DatabaseAccessLayer
{
    public class ClassAccess : IClassAccess
    {
        public ClassAccess(IConfiguration inConfiguration)
        {
            // From configuration data get name of conn-string - and then fetch the conn-string
            string? useConnectionString = inConfiguration["ConnectionStringToUse"];
            ConnectionString = useConnectionString is not null ? inConfiguration.GetConnectionString(useConnectionString) : null;
        }

        public ClassAccess(string inConnectionString)
        {
            ConnectionString = inConnectionString;
        }

        public string? ConnectionString { get; }

        public IEnumerable<Class> GetAllClasses(ClassType classType, Location location, DateOnly endDate)
        {
            try
            {
                string sqlQuery = @"
                SELECT
                c.class_ID,
                c.[name] AS className,
                c.description,
                c.startTime,
                c.duration,
                c.capacity,
                c.memberCount,
                c.trainingDate,

                 ct.classType,

                u.firstName AS employeeFirstName,
                u.lastName AS employeeLastName,

                r.roomName
                FROM Class c
                JOIN ClassType ct
                    ON ct.classType_ID = c.classType_ID_FK
                JOIN Room r
                    ON r.room_ID = c.room_ID_FK
                JOIN [Location] l
                    ON l.location_ID = r.location_ID_FK
                JOIN Employee e
                    ON e.employeeUserNumber_FK = c.employeeUserNumber_FK
                JOIN [User] u
                    ON u.userNumber = e.employeeUserNumber_FK
                WHERE
                    c.trainingDate BETWEEN CAST(GETDATE() AS DATE)
                        AND CAST(@EndDate AS DATE)
                        AND l.location_ID = @LocationId
                        AND ct.classType = @ClassType
                ORDER BY
                    c.trainingDate, c.startTime";
            }
            catch
            {
            }
            return null;

        }
    }
}
