using FitSammen_API.Exceptions;
using FitSammen_API.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.OpenApi.Writers;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Data;
using System.Transactions;

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

        public MemberAccess(string inConnectionString)
        {
            ConnectionString = inConnectionString;
        }

        public string? ConnectionString { get; }

        public int CreateMemberBooking(int memberUserID, int classId)
        {
            int createdMemberBookingId = 0;
            // Prepare the SQL query
            string queryString = "UPDATE Class " +
                "SET memberCount = memberCount + 1 " +
                "WHERE class_ID = @ClassId " +
                "AND memberCount < capacity; " +
                "IF @@ROWCOUNT = 1 " +
                "BEGIN " +
                "INSERT INTO MemberBooking(memberUser_ID_FK, class_ID_FK) " +
                "OUTPUT INSERTED.memberBookingID " +
                "VALUES(@MemberUserID, @ClassId); " +
                "END;";
            try
            {
                TransactionOptions tOptions = new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                };
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, tOptions))
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    using (SqlCommand readCommand = new SqlCommand(queryString, conn))

                    {
                        readCommand.Parameters.AddWithValue("@MemberUserID", memberUserID);
                        readCommand.Parameters.AddWithValue("@ClassId", classId);

                        if (conn != null)
                        {
                            conn.Open();
                            var res = readCommand.ExecuteScalar();
                            createdMemberBookingId = Convert.ToInt32(res);
                        }
                        else
                        {
                            throw new DataAccessException("No database connection available.");

                        }

                    }
                    scope.Complete();
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

            // Prepare the SQL query
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




        public int CreateWaitingListEntry(int MemberUserId, int ClassId)
        {
            int waitingListPosition = -1;
            int newId = 0;
            DateTime createdAt;

            string insertQuery = @"
        INSERT INTO WaitingListEntry (memberUser_ID_FK, class_ID_FK, CreatedAt)
        OUTPUT inserted.waitingList_ID, inserted.CreatedAt
        SELECT @MemberUserId, @ClassId, SYSDATETIME()
        FROM Class
        WHERE class_ID = @ClassId
          AND memberCount = capacity;";

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
                    //cmd.Parameters.AddWithValue("@CreatedAt", createdAt);
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

            // Prepare the SQL query
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
                        DateTime? CreatedAt = null;

                        using (SqlDataReader reader = readCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"));
                            }
                        }
                        if (CreatedAt.HasValue)
                        {
                            string positionQuery = @"
                                    SELECT COUNT(*) + 1 AS position
                                    FROM WaitingListEntry
                                    WHERE class_ID_FK = @ClassId
                                    AND CreatedAt < @CreatedAt;";

                            using (var cmd = new SqlCommand(positionQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@ClassId", ClassId);
                                cmd.Parameters.AddWithValue("@CreatedAt", CreatedAt.Value);

                                waitingListPosition = (int)cmd.ExecuteScalar();
                            }
                        }
                        return waitingListPosition;
                    } else
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

    }
}


