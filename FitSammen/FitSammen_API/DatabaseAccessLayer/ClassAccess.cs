using FitSammen_API.Exceptions;
using FitSammen_API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System.Transactions;

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
                "l.housenumber, " +
                "cty.cityName " +
                "FROM Class c " +
                "JOIN ClassType ct " +
                    "ON ct.classType_ID = c.classType_ID_FK " +
                "JOIN Employee e " +
                    "ON e.employeeUser_ID_FK = c.employeeUser_ID_FK " +
                "JOIN [User] u " +
                    "ON u.user_ID = e.employeeUser_ID_FK " +
                "JOIN Room r " +
                    "ON r.room_ID = c.room_ID_FK " +
                "JOIN [Location] l " +
                    "ON l.location_ID = r.location_ID_FK " +
                "JOIN Zipcode z " +
                    "ON z.zipcodeNumber = l.zipcodeNumber_FK " +
                "JOIN City cty " +
                    "ON cty.city_ID = z.city_ID_FK " +
                "ORDER BY " +
                "c.trainingDate, " +
                "c.startTime;";
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, conn))
                {
                    if (conn != null)
                    {
                        conn.Open();

                        SqlDataReader reader = readCommand.ExecuteReader();
                        classes = UpcomingClassesBuilder(reader);
                    }
                    else
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
                    Class cls = new Class(id, trainingDate, instructor, description, room, name, capacity, memberCount, durationInMinutes, startTime, classType);
                    classes.Add(cls);
                }
            }
            catch (SqlException ex)
            {
                throw new DataAccessException("Error reading class data from database.", ex);
            }
            return classes;
        }

        public IEnumerable<Location> GetAllLocations()
        {
            List<Location> locations = new List<Location>();

            string queryString = @"SELECT
            L.location_ID,
            L.streetName,
            C.cityName,
            L.houseNumber,
            Z.zipcodeNumber
            FROM Location L
            JOIN Zipcode Z
            ON L.zipcodeNumber_FK = Z.zipcodeNumber
            JOIN City C
            ON Z.city_ID_FK = C.city_ID;";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, conn))
                {
                    if (conn != null)
                    {
                        conn.Open();

                        SqlDataReader reader = readCommand.ExecuteReader();
                        try
                        {
                            while (reader.Read())
                            {
                                City city = new City
                                {
                                    CityName = reader.GetString(reader.GetOrdinal("cityName")),
                                };
                                Zipcode zipcode = new Zipcode
                                {
                                    ZipcodeNumber = reader.GetInt32(reader.GetOrdinal("zipcodeNumber")),
                                    City = city
                                };
                                Location location = new Location
                                {
                                    LocationId = reader.GetInt32(reader.GetOrdinal("location_ID")),
                                    StreetName = reader.GetString(reader.GetOrdinal("streetName")),
                                    HouseNumber = reader.GetInt32(reader.GetOrdinal("houseNumber")),
                                    Zipcode = zipcode
                                };
                                locations.Add(location);
                            }
                        }
                        catch (SqlException ex)
                        {
                            throw new DataAccessException("Error reading class data from database.", ex);
                        }
                    }
                    else
                    {
                        throw new DataAccessException("No database connection available.");
                    }
                }
                return locations;

            }
            catch (SqlException sqlEx)
            {
                throw new DataAccessException("Error retrieving upcoming classes from database.", sqlEx);
            }

        }

        public IEnumerable<Room> GetRoomsByLocationId(int LocationId)
        {
            List<Room> rooms = new List<Room>();

            string queryString = @"SELECT
            L.location_ID,
            R.room_ID,
            R.roomName,
            R.capacity
            FROM Room R
            JOIN Location L
            ON R.location_ID_FK = L.location_ID
            WHERE R.location_ID_FK = @LocationId;";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, conn))
                {
                    readCommand.Parameters.AddWithValue("@LocationId", LocationId);
                    if (conn != null)
                    {
                        conn.Open();
                        SqlDataReader reader = readCommand.ExecuteReader();
                        try
                        {
                            while (reader.Read())
                            {
                                Location location = new Location
                                {
                                    LocationId = reader.GetInt32(reader.GetOrdinal("location_ID")),

                                };
                                Room room = new Room
                                {
                                    RoomId = reader.GetInt32(reader.GetOrdinal("room_ID")),
                                    RoomName = reader.GetString(reader.GetOrdinal("roomName")),
                                    Capacity = reader.GetInt32(reader.GetOrdinal("capacity")),
                                    Location = location
                                };
                                rooms.Add(room);
                            }
                        }
                        catch (SqlException sqlEx)
                        {
                            throw new DataAccessException("Error reading class data from database.", sqlEx);
                        }
                    }
                    else
                    {
                        throw new DataAccessException("No database connection available.");
                    }
                }
                return rooms;
            }
            catch (SqlException sqlEx)
            {
                throw new DataAccessException("Error retrieving upcoming classes from database.", sqlEx);
            }
        }

        public IEnumerable<Employee> GetEmployeesByLocationId(int LocationId)
        {
            List<Employee> Employees = new List<Employee>();

            string queryString = @"SELECT
            L.location_ID,
            U.user_ID,
            U.firstName,
            U.lastName
            FROM[User] U
            JOIN Employee E
            ON U.userType_ID_FK = E.employeeUser_ID_FK
            JOIN Location L
            ON E.location_ID_FK = L.location_ID
            WHERE
            U.userType_ID_FK = 1
            AND
            L.location_ID = @LocationId;";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, conn))
                {
                    readCommand.Parameters.AddWithValue("@LocationId", LocationId);
                    if (conn != null)
                    {
                        conn.Open();
                        SqlDataReader reader = readCommand.ExecuteReader();
                        try
                        {
                            while (reader.Read())
                            {
                                Location location = new Location
                                {
                                    LocationId = reader.GetInt32(reader.GetOrdinal("location_ID"))
                                };
                                Employee employee = new Employee
                                {
                                    User_ID = reader.GetInt32(reader.GetOrdinal("user_ID")),
                                    FirstName = reader.GetString(reader.GetOrdinal("firstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("lastName"))
                                };
                                Employees.Add(employee);
                                
                            }
                        }
                        catch (SqlException sqlEx)
                        {
                            throw new DataAccessException("Error reading class data from database.", sqlEx);
                        }
                    }
                    else
                    {
                        throw new DataAccessException("No database connection available.");
                    }
                }
                return Employees;
            }
            catch (SqlException sqlEx)
            {
                throw new DataAccessException("Error retrieving upcoming classes from database.", sqlEx);
            }
        }

        public int CreateClass(Class NewClass)
        {
            int ClassId = 0;
            DateTime newClassStartDateTime = NewClass.TrainingDate.ToDateTime(NewClass.StartTime);
            DateTime newClassEndDateTime = newClassStartDateTime.AddMinutes(NewClass.DurationInMinutes);
            int roomId = NewClass.Room.RoomId;
            int employeeId = NewClass.Instructor.User_ID;
            int classTypeId = (int)NewClass.ClassType;

            string queryString = @"IF NOT EXISTS (
            SELECT 1
            FROM Class AS C
            WHERE
            C.trainingDate = CONVERT(DATE, @NewStartDateTime)
            AND (
            C.room_ID_FK = @RoomId
            OR
            C.employeeUser_ID_FK = @EmployeeId
            )
            AND (
            CAST(C.trainingDate AS DATETIME) + CAST(C.startTime AS DATETIME)
            < 
            @NewEndDateTime 
            AND
            @New_Start_DateTime
            <
            DATEADD(MINUTE, C.duration, CAST(C.trainingDate AS DATETIME) + CAST(C.startTime AS DATETIME))
            )
            )
            BEGIN
            INSERT INTO Class(
            name, description, memberCount, capacity, startTime, duration, trainingDate, classType_ID_FK, employeeUser_ID_FK, room_ID_FK)
            OUTPUT INSERTED.class_ID
            VALUES(
            @ClassName, @Description, @MemberCount, @Capacity, CONVERT(TIME(0), @NewStartDateTime),
            CONVERT(TIME(0), DATEADD(minute, @NewDuration, 0)), CONVERT(DATE, @NewStartDateTime), @ClassType, @EmployeeId, @RoomId);
            END
            ELSE
            BEGIN
            SELECT 0 AS class_ID
            END";


            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        using (SqlCommand readCommand = new SqlCommand(queryString, conn))
                        {
                            //Overlap check
                            readCommand.Parameters.AddWithValue("@NewStartDateTime", newClassStartDateTime);
                            readCommand.Parameters.AddWithValue("@NewEndDateTime", newClassEndDateTime);
                            readCommand.Parameters.AddWithValue("@RoomId", roomId);
                            readCommand.Parameters.AddWithValue("@EmployeeId", employeeId);
                            //Insert parametre
                            readCommand.Parameters.AddWithValue("@ClassName", NewClass.Name);
                            readCommand.Parameters.AddWithValue("@Description", NewClass.Description);
                            readCommand.Parameters.AddWithValue("@MemberCount", NewClass.MemberCount);
                            readCommand.Parameters.AddWithValue("@Capacity", NewClass.Capacity);
                            readCommand.Parameters.AddWithValue("@NewDuration", NewClass.DurationInMinutes);
                            readCommand.Parameters.AddWithValue("@ClassType", classTypeId);

                            object res = readCommand.ExecuteScalar();

                            ClassId = (res == null || res == DBNull.Value) ? 0 : Convert.ToInt32(res);

                            if (ClassId > 0)
                            {
                                scope.Complete();
                            }
                        }
                    }
                }
                return ClassId;
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Couldnt insert new class.", ex);
            }
        }


    }
}



