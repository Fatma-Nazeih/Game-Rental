using GameRentalSystem.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace GameRentalSystem.DAL
{
    public class UserDAL
    {
        // Select Data from one table: Get user by username and password (for login)
        public User GetUserByUsernameAndPassword(string username, string password)
        {
            string commandText = "SELECT UserID, Username, FullName, Email, Role FROM Users WHERE Username = @Username AND Password = @Password"; // WARNING: Plain text password
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", username),
                new SqlParameter("@Password", password) // WARNING: Plain text password
            };

            using (SqlDataReader reader = DatabaseHelper.ExecuteReader(commandText, parameters))
            {
                if (reader.Read())
                {
                    return new User
                    {
                        UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                        Username = reader.GetString(reader.GetOrdinal("Username")),
                        FullName = reader.GetString(reader.GetOrdinal("FullName")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        Role = reader.GetString(reader.GetOrdinal("Role"))
                        // Password and RegistrationDate are not retrieved for security/simplicity
                    };
                }
            }
            return null; // User not found or incorrect credentials
        }

        // Select Data from one table: Get user by ID
        public User GetUserById(int userId)
        {
            string commandText = "SELECT UserID, Username, FullName, Email, Role FROM Users WHERE UserID = @UserID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId)
            };

            using (SqlDataReader reader = DatabaseHelper.ExecuteReader(commandText, parameters))
            {
                if (reader.Read())
                {
                    return new User
                    {
                        UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                        Username = reader.GetString(reader.GetOrdinal("Username")),
                        FullName = reader.GetString(reader.GetOrdinal("FullName")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        Role = reader.GetString(reader.GetOrdinal("Role"))
                    };
                }
            }
            return null;
        }

        // Insert Statement 1: Register a new Client
        public int RegisterClient(User clientUser)
        {
            string commandText = @"INSERT INTO Users (Username, Password, FullName, Email, Role)
                                 OUTPUT INSERTED.UserID
                                 VALUES (@Username, @Password, @FullName, @Email, 'Client')";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", clientUser.Username),
                new SqlParameter("@Password", clientUser.Password), // WARNING: Plain text
                new SqlParameter("@FullName", clientUser.FullName),
                new SqlParameter("@Email", clientUser.Email)
            };
            // ExecuteScalar is used here to get the newly generated UserID
            object result = DatabaseHelper.ExecuteScalar(commandText, parameters);
            return result == null ? -1 : (int)result; // Return the new UserID or -1 on failure
        }

        // Insert Statement 2: Register a new Vendor (User part)
        // The Vendor entry in the Vendors table needs to be created after this.
        public int RegisterVendorUser(User vendorUser)
        {
            string commandText = @"INSERT INTO Users (Username, Password, FullName, Email, Role)
                                 OUTPUT INSERTED.UserID
                                 VALUES (@Username, @Password, @FullName, @Email, 'Vendor')";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", vendorUser.Username),
                new SqlParameter("@Password", vendorUser.Password), // WARNING: Plain text
                new SqlParameter("@FullName", vendorUser.FullName),
                new SqlParameter("@Email", vendorUser.Email)
            };
            object result = DatabaseHelper.ExecuteScalar(commandText, parameters);
            return result == null ? -1 : (int)result;
        }

        

        // Select Data from one table: Get all users
        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            string commandText = "SELECT UserID, Username, FullName, Email, Role, RegistrationDate FROM Users ORDER BY Role, Username";

            using (SqlDataReader reader = DatabaseHelper.ExecuteReader(commandText))
            {
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                        Username = reader.GetString(reader.GetOrdinal("Username")),
                        FullName = reader.GetString(reader.GetOrdinal("FullName")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        Role = reader.GetString(reader.GetOrdinal("Role")),
                        RegistrationDate = reader.GetDateTime(reader.GetOrdinal("RegistrationDate"))
                    });
                }
            }
            return users;
        }

        

        // Checks if a username already exists in the Users table
        public bool CheckUsernameExists(string username)
        {
            string commandText = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", username)
            };

            // ExecuteScalar returns the first column of the first row,
            // which is the count in this case.
            object result = DatabaseHelper.ExecuteScalar(commandText, parameters);

            // If the count is greater than 0, the username exists
            return result != null && (int)result > 0;
        }

        // Checks if an email already exists in the Users table
        public bool CheckEmailExists(string email)
        {
            string commandText = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Email", email)
            };

            object result = DatabaseHelper.ExecuteScalar(commandText, parameters);

            // If the count is greater than 0, the email exists
            return result != null && (int)result > 0;
        }

        // These methods were previously provided and are included again as requested.
        // Add/ensure these methods are in your existing UserDAL.cs file.

        // Update a user's role in the Users table
        public bool UpdateUserRole(int userId, string newRole)
        {
            string commandText = "UPDATE Users SET Role = @NewRole WHERE UserID = @UserID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@NewRole", newRole),
                new SqlParameter("@UserID", userId)
            };

            try
            {
                // Execute the UPDATE command
                int rowsAffected = DatabaseHelper.ExecuteNonQuery(commandText, parameters);

                // Return true if at least one row was affected (user found and updated)
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error updating user role in DAL: " + ex.Message);
                return false;
            }
        }

        public bool DeleteUser(int userId)
        {

            string commandText = "DELETE FROM Users WHERE UserID = @UserID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId)
            };

            try
            {
                // Execute the DELETE command
                int rowsAffected = DatabaseHelper.ExecuteNonQuery(commandText, parameters);

                // Return true if at least one row was affected (user was deleted)
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error deleting user in DAL: " + ex.Message);
                return false; // Deletion failed
            }
        }
    }
}