using GameRentalSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace GameRentalSystem.DAL
{
    public class AdminRequestDAL
    {
        // Submit a new Admin Request
        public int InsertAdminRequest(int clientId)
        {
            string commandText = @"INSERT INTO AdminRequests (ClientID, Status)
                                 OUTPUT INSERTED.RequestID
                                 VALUES (@ClientID, 'Pending')";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ClientID", clientId)
            };
            object result = DatabaseHelper.ExecuteScalar(commandText, parameters);
            return result == null ? -1 : (int)result;
        }

        //  Update Admin Request Status (Approve/Reject)
        public bool UpdateRequestStatus(int requestId, string status, int adminApproverId)
        {
            string commandText = @"UPDATE AdminRequests SET
                                 Status = @Status,
                                 AdminApproverID = @AdminApproverID,
                                 ApprovalDate = GETDATE()
                                 WHERE RequestID = @RequestID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Status", status),
                new SqlParameter("@AdminApproverID", adminApproverId),
                new SqlParameter("@RequestID", requestId)
            };
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(commandText, parameters);
            return rowsAffected > 0;
        }

        // Select pending admin requests (for Admin view)
        public List<AdminRequest> GetPendingRequests()
        {
            List<AdminRequest> requests = new List<AdminRequest>();
            // Join with Users to get Client Name and Username
            string commandText = @"SELECT AR.RequestID, AR.ClientID, U.Username, U.FullName, AR.RequestDate, AR.Status
                                 FROM AdminRequests AR
                                 JOIN Users U ON AR.ClientID = U.UserID
                                 WHERE AR.Status = 'Pending' ORDER BY AR.RequestDate ASC";

            using (SqlDataReader reader = DatabaseHelper.ExecuteReader(commandText))
            {
                while (reader.Read())
                {
                    requests.Add(new AdminRequest
                    {
                        RequestID = reader.GetInt32(reader.GetOrdinal("RequestID")),
                        ClientID = reader.GetInt32(reader.GetOrdinal("ClientID")),
                        ClientUsername = reader.GetString(reader.GetOrdinal("Username")),
                        ClientFullName = reader.GetString(reader.GetOrdinal("FullName")), 
                        RequestDate = reader.GetDateTime(reader.GetOrdinal("RequestDate")),
                        Status = reader.GetString(reader.GetOrdinal("Status"))
                       
                    });
                }
            }
            return requests;
        }

        public bool HasPendingRequest(int clientId)
        {
            string commandText = "SELECT COUNT(*) FROM AdminRequests WHERE ClientID = @ClientID AND Status = 'Pending'";
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ClientID", clientId) };
            object result = DatabaseHelper.ExecuteScalar(commandText, parameters);
            return result != null && (int)result > 0;
        }

        // Delete Statement 6 (with condition): Delete a rejected admin request
        public bool DeleteRejectedRequest(int requestId)
        {
            string commandText = "DELETE FROM AdminRequests WHERE RequestID = @RequestID AND Status = 'Rejected'";
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@RequestID", requestId) };
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(commandText, parameters);
            return rowsAffected > 0;
        }
       

        // Select a single AdminRequest by ID
        public AdminRequest GetAdminRequestById(int requestId)
        {
            string commandText = @"SELECT AR.RequestID, AR.ClientID, U.Username, U.FullName, AR.RequestDate, AR.Status, AR.AdminApproverID, AR.ApprovalDate
                                 FROM AdminRequests AR
                                 JOIN Users U ON AR.ClientID = U.UserID
                                 WHERE AR.RequestID = @RequestID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@RequestID", requestId)
            };

            using (SqlDataReader reader = DatabaseHelper.ExecuteReader(commandText, parameters))
            {
                if (reader.Read())
                {
                  
                    return new AdminRequest
                    {
                        RequestID = reader.GetInt32(reader.GetOrdinal("RequestID")),
                        ClientID = reader.GetInt32(reader.GetOrdinal("ClientID")),
                        ClientUsername = reader.GetString(reader.GetOrdinal("Username")), 
                        ClientFullName = reader.GetString(reader.GetOrdinal("FullName")), 
                        RequestDate = reader.GetDateTime(reader.GetOrdinal("RequestDate")),
                        Status = reader.GetString(reader.GetOrdinal("Status")),
                        AdminApproverID = reader.IsDBNull(reader.GetOrdinal("AdminApproverID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("AdminApproverID")),
                        ApprovalDate = reader.IsDBNull(reader.GetOrdinal("ApprovalDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ApprovalDate"))
                    };
                }
            }
            return null; 
        }

       
        public bool HasPendingOrApprovedRequests(int userId)
        {
            string commandText = "SELECT COUNT(*) FROM AdminRequests WHERE ClientID = @UserID AND (Status = 'Pending' OR Status = 'Approved')";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId)
            };

            try
            {
                object result = DatabaseHelper.ExecuteScalar(commandText, parameters);
                return result != null && (int)result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error checking for pending/approved admin requests: " + ex.Message);
      
                return true;
            }
        }
    }
}