using GameRentalSystem.Models;
using System.Data.SqlClient;
using System.Data;

namespace GameRentalSystem.DAL
{
    public class VendorDAL
    {
        // Insert the Vendor specific details after the User record is created
        public bool InsertVendorDetails(int userId, Vendor vendor)
        {
            string commandText = "INSERT INTO Vendors (UserID, CompanyName, ContactNumber) VALUES (@UserID, @CompanyName, @ContactNumber)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@CompanyName", vendor.CompanyName),
                new SqlParameter("@ContactNumber", vendor.ContactNumber ?? (object)DBNull.Value) // Handle potential NULL
            };
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(commandText, parameters);
            return rowsAffected > 0;
        }

        
        public List<Vendor> GetAllVendors()
        {
            List<Vendor> vendors = new List<Vendor>();
            // Select VendorID, CompanyName, and UserID (UserID might be useful for linking)
            string commandText = "SELECT VendorID, CompanyName, UserID FROM Vendors ORDER BY CompanyName";

            try
            {
                // Use DatabaseHelper to execute the query and get a reader
                using (SqlDataReader reader = DatabaseHelper.ExecuteReader(commandText))
                {
                    // Read data row by row from the reader
                    while (reader.Read())
                    {
                        // Create a new Vendor object and populate its properties
                        vendors.Add(new Vendor
                        {
                            VendorID = reader.GetInt32(reader.GetOrdinal("VendorID")),
                            CompanyName = reader.GetString(reader.GetOrdinal("CompanyName")),
                            UserID = reader.GetInt32(reader.GetOrdinal("UserID"))
                            // ContactNumber is not needed for this list, but could be included
                        });
                    }
                } // The 'using' statement ensures the reader and connection are closed
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error getting all vendors: " + ex.Message);
                // Return an empty list on error
                return new List<Vendor>();
            }

            return vendors; // Return the list of Vendor objects
        }

        // Retrieves the VendorID for a given UserID
        public int GetVendorIdByUserId(int userId)
        {
            string commandText = "SELECT VendorID FROM Vendors WHERE UserID = @UserID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId)
            };

            try
            {
                // ExecuteScalar returns the first column of the first row, which is the VendorID.
                object result = DatabaseHelper.ExecuteScalar(commandText, parameters);

                return result == null || result is DBNull ? -1 : (int)result;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error getting vendor ID by user ID: " + ex.Message);
                // Return -1 on error
                return -1;
            }
        }
    }
}