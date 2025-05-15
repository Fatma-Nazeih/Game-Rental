using GameRentalSystem.DAL;
using GameRentalSystem.Models;
using System.Collections.Generic;

namespace GameRentalSystem.BLL
{
    public class AdminBLL
    {
        private readonly AdminRequestDAL _adminRequestDAL = new AdminRequestDAL();
        private readonly UserDAL _userDAL = new UserDAL();

        // For Admin
        public List<AdminRequest> GetPendingAdminRequests()
        {
            return _adminRequestDAL.GetPendingRequests();
        }

        // For Client
        public bool RequestAdminRole(int clientId)
        {
            if (_adminRequestDAL.HasPendingRequest(clientId))
            {
                return false;
            }

            int newRequestId = _adminRequestDAL.InsertAdminRequest(clientId);
            return newRequestId > 0;
        }

        // For Admin
        public bool ProcessAdminRequest(int requestId, int adminUserId, bool approve)
        {
            var request = GetAdminRequestById(requestId);
            if (request == null || request.Status != "Pending")
            {
                return false;
            }

            if (approve)
            {
                bool roleUpdated = _userDAL.UpdateUserRole(request.ClientID, "Admin");
                if (roleUpdated)
                {
                    return _adminRequestDAL.UpdateRequestStatus(requestId, "Approved", adminUserId);
                }
                return false;
            }
            else
            {
                return _adminRequestDAL.UpdateRequestStatus(requestId, "Rejected", adminUserId);
            }
        }

        // For Admin
        public List<User> GetAllUsers()
        {
            return _userDAL.GetAllUsers();
        }

        public User GetUserById(int userId)
        {
            return _userDAL.GetUserById(userId);
        }

        public AdminRequest GetAdminRequestById(int requestId)
        {
   
            return _adminRequestDAL.GetAdminRequestById(requestId);
        }


        // For Admin: Update a user's role
        public bool UpdateUserRole(int userId, string newRole)
        {
           
            if (string.IsNullOrWhiteSpace(newRole) || (newRole != "Client" && newRole != "Vendor" && newRole != "Admin"))
            {
                return false; 
            }

            try
            {
                bool success = _userDAL.UpdateUserRole(userId, newRole); // Assuming UserDAL has UpdateUserRole

                return success;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating user role: " + ex.Message);
                return false;
            }
        }

        public bool DeleteUser(int userId)
        {
            var rentalDAL = new RentalDAL();
            var gameDAL = new GameDAL(); 
            var adminRequestDAL = new AdminRequestDAL();

            try
            {
                if (rentalDAL.HasActiveRentalsForUser(userId)) 
                {
                    return false;
                }

                if (gameDAL.HasGamesByVendorUser(userId)) 
                {
                    return false; 
                }

                if (adminRequestDAL.HasPendingOrApprovedRequests(userId)) 
                {
                   
                    return false;
                }

                bool success = _userDAL.DeleteUser(userId); 

                return success;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting user: " + ex.Message);
                return false;
            }
        }
    }
}