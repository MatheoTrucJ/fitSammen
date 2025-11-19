using FitSammen_API.Exceptions;
using FitSammen_API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

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

        public IEnumerable<Class> GetUpcomingClasses()
        {
            IEnumerable<Class>? classes = null;

            // Prepare the SQL query
            string queryString = "SELECT c.class_ID, " +
                "c.trainingDate, " +
                "c.startTime, " +
                "c.duration, " +
                "c.capacity, " +
                "c.memberCount, " +
                "c.[name], " +
                "c.[description], " +
                "ct.classType, " +
                "u.firstName, " +
                "u.lastName, " +
                "r.roomName, " +
                "l.streetName, " +
                "l.housenumber " +
                "cty.cityName " +
                "FROM Class c " +
                "JOIN ClassType ct " +
                    "ON ct.classType_ID = c.classType_ID_FK " +
                "JOIN Employee e " +
                    "ON e.employeeUserNumber_FK = c.employeeUserNumber_FK " +
                "JOIN [User] u " +
                    "ON u.userNumber = e.employeeUserNumber_FK " +
                "JOIN Room r " +
                    "ON r.room_ID = c.room_ID_FK " +
                "JOIN [Location] l " +
                    "ON l.location_ID = r.location_ID_FK " +
                "JOIN Zipcode z " +
                    "ON z.zipcodeNumber = l.zipcodeNumber_FK " +
                "JOIN City c " +
                    "ON cty.city_ID = z.city_ID_FK " +
                "ORDER BY " +
                "c.trainingDate, " +
                "c.startTime;";
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, conn))
                {
                    if(conn != null)
                    {
                        conn.Open();

                        SqlDataReader reader = readCommand.ExecuteReader();
                        classes = UpcomingClassesBuilder(reader);
                    } else
                    {
                        throw new DataAccessException("No database connection available.");
                    }
                }

            }
            catch (SqlException sqlEx)
            {
                throw new DataAccessException("Error retrieving upcoming classes from database.", sqlEx);
            }
            return classes;
        }

        private IEnumerable<Class> UpcomingClassesBuilder(SqlDataReader reader)
        {
            List<Class> classes = new List<Class>();
            try
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(reader.GetOrdinal("class_ID"));
                    DateOnly trainingDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("trainingDate")));
                    Employee instructor = new Employee
                    {
                        FirstName = reader.GetString(reader.GetOrdinal("firstName")),
                        LastName = reader.GetString(reader.GetOrdinal("lastName"))
                    };
                    String description = reader.GetString(reader.GetOrdinal("description"));
                    Room room = new Room
                    {
                        RoomName = reader.GetString(reader.GetOrdinal("roomName")),
                        Location = new Location
                        {
                            StreetName = reader.GetString(reader.GetOrdinal("streetName")),
                            HouseNumber = reader.GetInt32(reader.GetOrdinal("housenumber")),
                            Zipcode = new Zipcode
                            {
                                City = new City
                                {
                                    CityName = reader.GetString(reader.GetOrdinal("cityName"))
                                }
                            }
                        }
                    };
                    string name = reader.GetString(reader.GetOrdinal("name"));
                    int capacity = reader.GetInt32(reader.GetOrdinal("capacity"));
                    int memberCount = reader.GetInt32(reader.GetOrdinal("memberCount"));
                    int durationInMinutes = reader.GetInt32(reader.GetOrdinal("duration"));
                    TimeOnly startTime = TimeOnly.FromTimeSpan(reader.GetTimeSpan(reader.GetOrdinal("startTime")));
                    ClassType classType = Enum.Parse<ClassType>(reader.GetString(reader.GetOrdinal("classType")));
                    Class cls = new Class(id, trainingDate, instructor, name, room, name, capacity, memberCount, durationInMinutes, startTime, classType);
                    classes.Add(cls);
                }
            }
            catch (SqlException ex)
            {
                throw new DataAccessException("Error reading class data from database.", ex);
            }
            return classes;
        }
    }
}
