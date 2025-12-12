using FitSammen_API.Exceptions;
using FitSammen_API.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.OpenApi.Writers;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Transactions;

namespace FitSammen_API.DatabaseAccessLayer
{
    public class MemberAccess : IMemberAccess
    {
        public string? ConnectionString { get; }

        public MemberAccess(IConfiguration inConfiguration)
        {
            string? useConnectionString = inConfiguration["ConnectionStringToUse"];
            ConnectionString = useConnectionString is not null ? inConfiguration.GetConnectionString(useConnectionString) : null;
        }

        public MemberAccess(string inConnectionString)
        {
            ConnectionString = inConnectionString;
        }

        public int CreateMemberBooking(int memberUserID, int classId)
        {
            bool end = false;
            int tries = -1;
            int capacity = 0;
            int createdMemberBookingId = 0;
            string capacityCheck = @"SELECT capacity FROM Class WHERE class_ID = @ClassId;";
            string insertBooking = @"INSERT INTO MemberBooking(memberUser_ID_FK, class_ID_FK) 
                                    OUTPUT INSERTED.memberBookingID 
                                    VALUES(@MemberUserID, @ClassIdInsert);";
            try
            {
                while (tries < 3 && end != true)
                {
                    tries++;
                    int currCount = GetMemberCountFromClassId(classId);

                    TransactionOptions tOptions = new TransactionOptions
                    {
                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                    };
                    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, tOptions))
                    {
                        using (SqlConnection conn = new SqlConnection(ConnectionString))
                        using (SqlCommand readCommand = new SqlCommand(capacityCheck, conn))
                        {
                            readCommand.Parameters.AddWithValue("@ClassId", classId);
                            if (conn != null)
                            {
                                conn.Open();
                                var res = readCommand.ExecuteScalar();
                                capacity = Convert.ToInt32(res);
                                if (currCount >= capacity)
                                {
                                    end = true;
                                    return createdMemberBookingId = 0;
                                }
                            }
                            else
                            {
                                throw new DataAccessException("No database connection available.");
                            }
                            readCommand.Parameters.Clear();
                            readCommand.CommandText = insertBooking;
                            readCommand.Parameters.AddWithValue("@MemberUserID", memberUserID);
                            readCommand.Parameters.AddWithValue("@ClassIdInsert", classId);

                            if (conn != null)
                            {
                                var res = readCommand.ExecuteScalar();
                                createdMemberBookingId = Convert.ToInt32(res);
                            }
                            else
                            {
                                throw new DataAccessException("No database connection available.");

                            }
                        }
                        int newCount = GetMemberCountFromClassId(classId);
                        if (newCount <= capacity)
                        {
                            scope.Complete();
                            end = true;
                        }
                        else
                        {
                            scope.Dispose();
                            Random rnd = new Random();
                            Thread.Sleep(rnd.Next(0, 150));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Error in creating member booking in database.", ex);
            }
            return createdMemberBookingId;
        }

        public bool IsMemberSignedUp(int memberID, int classID)
        {
            bool res = false;

            string queryString = "SELECT * " +
                "FROM MemberBooking mb " +
                "WHERE mb.memberUser_ID_FK = @MemberID " +
                "AND mb.class_ID_FK = @classID;";
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, conn))
                {
                    readCommand.Parameters.AddWithValue("@MemberID", memberID);
                    readCommand.Parameters.AddWithValue("@classID", classID);
                    if (conn != null)
                    {
                        conn.Open();

                        SqlDataReader reader = readCommand.ExecuteReader();
                        res = reader.HasRows;
                    }
                    else
                    {
                        throw new DataAccessException("No database connection available.");
                    }
                }

            }
            catch (SqlException sqlEx)
            {
                throw new DataAccessException("Error retrieving memberBooking result from database.", sqlEx);
            }
            return res;
        }

        public int GetMemberCountFromClassId(int classId)
        {
            int res = 0;
            string query = @"SELECT COUNT(*) AS memberCount 
                        FROM MemberBooking 
                        WHERE class_ID_FK = @ClassId;";
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ClassId", classId);
                    conn.Open();
                    var temp = cmd.ExecuteScalar();
                    res = Convert.ToInt32(temp);
                }
            }
            catch (SqlException sqlEx)
            {
                throw new DataAccessException("Error retrieving member count from database.", sqlEx);
            }
            return res;
        }

        public int CreateWaitingListEntry(int MemberUserId, int ClassId)
        {
            int waitingListPosition = -1;
            int newId = 0;
            DateTime createdAt;
            int memberCount = GetMemberCountFromClassId(ClassId);

            string insertQuery = @"
        INSERT INTO WaitingListEntry (memberUser_ID_FK, class_ID_FK, CreatedAt)
        OUTPUT inserted.waitingList_ID, inserted.CreatedAt
        SELECT @MemberUserId, @ClassId, SYSDATETIME()
        FROM Class
        WHERE class_ID = @ClassId
          AND @MemberCount = capacity;";

            try
            {
                var tOptions = new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
                };

                using (var scope = new TransactionScope(TransactionScopeOption.Required, tOptions))
                {
                    using (var conn = new SqlConnection(ConnectionString))
                    using (var cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MemberUserId", MemberUserId);
                        cmd.Parameters.AddWithValue("@ClassId", ClassId);
                        cmd.Parameters.AddWithValue("@MemberCount", memberCount);

                        conn.Open();

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                newId = reader.GetInt32(reader.GetOrdinal("waitingList_ID"));
                                createdAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"));
                            }
                            else
                            {
                                return -1;
                            }
                        }
                    }


                    scope.Complete();
                }


                string positionQuery = @"
            SELECT COUNT(*) + 1 AS position
            FROM WaitingListEntry
            WHERE class_ID_FK = @ClassId
              AND CreatedAt < @CreatedAt;";

                using (var conn = new SqlConnection(ConnectionString))
                using (var cmd = new SqlCommand(positionQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@ClassId", ClassId);
                    cmd.Parameters.Add(new SqlParameter("@CreatedAt", SqlDbType.DateTime2) { Value = createdAt });

                    conn.Open();
                    waitingListPosition = (int)cmd.ExecuteScalar();
                }

                return waitingListPosition;
            }
            catch (SqlException sqlEx)
            {
                throw new DataAccessException("Error creating waiting list entry in database.", sqlEx);
            }

        }

        public int IsMemberOnWaitingList(int MemberUserId, int ClassId)
        {
            int waitingListPosition = 0;

            string queryString = "SELECT * " +
                "FROM WaitingListEntry " +
                "WHERE memberUser_ID_FK = @MemberUserId " +
                "AND class_ID_FK = @ClassID;";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, conn))
                {
                    readCommand.Parameters.AddWithValue("@MemberUserId", MemberUserId);
                    readCommand.Parameters.AddWithValue("@classID", ClassId);
                    if (conn != null)
                    {
                        conn.Open();
                        int waitingListEntryId = 0; 

                        using (SqlDataReader reader = readCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                waitingListEntryId = reader.GetInt32(reader.GetOrdinal("waitingList_ID"));
                            }
                        }
                        if (waitingListEntryId > 0)
                        {
                            string positionQuery = @"
                                    SELECT COUNT(*) + 1 AS position
                                    FROM WaitingListEntry
                                    WHERE class_ID_FK = @ClassId
                                    AND waitingList_ID < @WaitingListEntryId;";

                            using (var cmd = new SqlCommand(positionQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                                cmd.Parameters.AddWithValue("@WaitingListEntryId", waitingListEntryId);

                                waitingListPosition = (int)cmd.ExecuteScalar();
                            }
                        }
                        return waitingListPosition;
                    }
                    else
                    {
                        throw new DataAccessException("No database connection available.");
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new DataAccessException("Error retrieving WaitingListEntry result from database.", sqlEx);
            }
        }

        public User FindUserByEmailAndPassword(string email, byte[] hashedPassword)
        {
            User foundUser = new Member();
            string queryString = @"SELECT user_ID, firstName, lastName, email, ut.usertype 
            FROM [User] u JOIN UserTypes ut on u.userType_ID_FK = ut.UserType_ID  
            WHERE email = @Email AND PasswordHash = @PasswordHash;";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, conn))
                {
                    readCommand.Parameters.AddWithValue("@Email", email);
                    readCommand.Parameters.Add("@PasswordHash", SqlDbType.VarBinary).Value = hashedPassword;
                    conn.Open();
                    SqlDataReader reader = readCommand.ExecuteReader();
                    if (reader.Read())
                    {
                        foundUser = new Member
                        {
                            User_ID = reader.GetInt32(reader.GetOrdinal("user_ID")),
                            FirstName = reader.GetString(reader.GetOrdinal("firstName")),
                            LastName = reader.GetString(reader.GetOrdinal("lastName")),
                            Email = reader.GetString(reader.GetOrdinal("email")),
                            UserType = Enum.Parse<UserType>(reader.GetString(reader.GetOrdinal("usertype")))
                        };
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new DataAccessException("Error retrieving user result from database.", sqlEx);
            }
            return foundUser;
        }

        public byte[] GetSaltByEmail(string email)
        {
            byte[] StoredSalt = RandomNumberGenerator.GetBytes(16);
            string queryString = @"SELECT PasswordSalt
            FROM [User]
            WHERE email = @email;";
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, conn))
                {
                    readCommand.Parameters.AddWithValue("@email", email);
                    conn.Open();
                    SqlDataReader reader = readCommand.ExecuteReader();
                    if (reader.Read())
                    {
                        StoredSalt = (byte[])reader["PasswordSalt"];
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new DataAccessException("Error retrieving user result from database", sqlEx);
            }
            return StoredSalt;
        }
    }
}


