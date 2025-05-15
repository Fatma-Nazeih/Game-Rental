using GameRentalSystem.DAL;
using GameRentalSystem.Models;
using System; 

namespace GameRentalSystem.BLL
{
    public class AuthBLL
    {
        private readonly UserDAL _userDAL = new UserDAL();
        private readonly VendorDAL _vendorDAL = new VendorDAL();

        public User AuthenticateUser(string username, string password)
        {
            return _userDAL.GetUserByUsernameAndPassword(username, password);
        }

        public bool RegisterClient(User clientUser)
        {
            if (string.IsNullOrWhiteSpace(clientUser.Username) || string.IsNullOrWhiteSpace(clientUser.Password) ||
                string.IsNullOrWhiteSpace(clientUser.FullName) || string.IsNullOrWhiteSpace(clientUser.Email))
            {
              
                return false; 
            }

          
            if (_userDAL.CheckUsernameExists(clientUser.Username))
            {
               
                return false;
            }

            if (_userDAL.CheckEmailExists(clientUser.Email))
            {
              
                return false; 
            }

            try
            {
                int newUserId = _userDAL.RegisterClient(clientUser);
                return newUserId > 0;
            }
            catch (Exception ex)
            {
               
                Console.WriteLine("Error registering client: " + ex.Message);
                return false; 
            }
        }

        public bool RegisterVendor(User vendorUser, Vendor vendorDetails)
        {
           
            if (string.IsNullOrWhiteSpace(vendorUser.Username) || string.IsNullOrWhiteSpace(vendorUser.Password) ||
               string.IsNullOrWhiteSpace(vendorUser.FullName) || string.IsNullOrWhiteSpace(vendorUser.Email) ||
               string.IsNullOrWhiteSpace(vendorDetails.CompanyName)) 
            {
              
                return false;
            }

            if (_userDAL.CheckUsernameExists(vendorUser.Username))
            {
                return false;
            }

            if (_userDAL.CheckEmailExists(vendorUser.Email))
            {
                return false; 
            }

            try
            {
                int newUserId = _userDAL.RegisterVendorUser(vendorUser);

                if (newUserId > 0)
                {
                   
                    vendorDetails.UserID = newUserId; 
                    bool vendorDetailsSuccess = _vendorDAL.InsertVendorDetails(newUserId, vendorDetails);

                    if (vendorDetailsSuccess)
                    {
                        return true; 
                    }
                    else
                    {
                        Console.WriteLine($"Warning: User {newUserId} created, but vendor details failed.");
                        return false; 
                    }
                }
                else
                {
                 
                    return false;
                }
            }
            catch (Exception ex)
            {
             
                Console.WriteLine("Error registering vendor: " + ex.Message);
                return false;
            }
        }

    }
}