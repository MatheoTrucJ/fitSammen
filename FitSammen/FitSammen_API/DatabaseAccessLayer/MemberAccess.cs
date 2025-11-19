using FitSammen_API.Exceptions;
using FitSammen_API.Model;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
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

        public int CreateMemberBooking(int memberUserNumber, int classId)
        {
            int createdMemberBookingId = 0;
            // Prepare the SQL query
            string queryString = "UPDATE Class " +
                "SET memberCount = memberCount + 1 " +
                "WHERE class_ID = @ClassId " +
                "AND memberCount < capacity; " +
                "IF @@ROWCOUNT = 1 " +
                "BEGIN " +
                "INSERT INTO MemberBooking(memberUserNumber_FK, class_ID_FK) " +
                "OUTPUT INSERTED.memberBookingID " +
                "VALUES(@MemberUserNumber, @ClassId); " +
                "END;";
            try
            {
                var tOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadUncommitted
                };
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, tOptions))
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    using (SqlCommand readCommand = new SqlCommand(queryString, conn))

                    {
                        readCommand.Parameters.AddWithValue("@MemberUserNumber", memberUserNumber);
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

        public bool IsMemberBookingThereForTest(int memberBookingId)
        {
            bool res = false;

            // Prepare the SQL query
            string queryString = "SELECT * " +
                "FROM MemberBooking mb " +
                "WHERE mb.memberBookingID = @MemberBookingId;";
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, conn))
                {
                    readCommand.Parameters.AddWithValue("@MemberBookingId", memberBookingId);
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
    }
}

