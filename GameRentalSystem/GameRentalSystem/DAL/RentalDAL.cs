using GameRentalSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace GameRentalSystem.DAL
{
    public class RentalDAL
    {
        // Insert Statement 4: Create a new Rental
        public int InsertRental(Rental rental)
        {
            string commandText = @"INSERT INTO Rentals (GameID, ClientID, RentalDate, DueDate, Status)
                                 OUTPUT INSERTED.RentalID
                                 VALUES (@GameID, @ClientID, @RentalDate, @DueDate, @Status)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@GameID", rental.GameID),
                new SqlParameter("@ClientID", rental.ClientID),
                new SqlParameter("@RentalDate", rental.RentalDate),
                new SqlParameter("@DueDate", rental.DueDate),
                new SqlParameter("@Status", rental.Status) // Should be 'Rented' initially
            };
            object result = DatabaseHelper.ExecuteScalar(commandText, parameters);
            return result == null ? -1 : (int)result;
        }

        // Update Statement 4: Update a Rental (e.g., on return)
        public bool UpdateRental(Rental rental)
        {
            string commandText = @"UPDATE Rentals SET
                                 GameID = @GameID, -- Should not change
                                 ClientID = @ClientID, -- Should not change
                                 RentalDate = @RentalDate, -- Should not change
                                 ReturnDate = @ReturnDate,
                                 DueDate = @DueDate, -- Can be extended? Not in requirements, keep as is.
                                 Status = @Status
                                 WHERE RentalID = @RentalID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@GameID", rental.GameID),
                new SqlParameter("@ClientID", rental.ClientID),
                new SqlParameter("@RentalDate", rental.RentalDate),
                new SqlParameter("@ReturnDate", rental.ReturnDate ?? (object)DBNull.Value), // Allow NULL for ongoing rentals
                new SqlParameter("@DueDate", rental.DueDate),
                new SqlParameter("@Status", rental.Status),
                new SqlParameter("@RentalID", rental.RentalID)
            };
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(commandText, parameters);
            return rowsAffected > 0;
        }

        // Update Rental Status and ReturnDate (Specific for Return)
        public bool ReturnRental(int rentalId, DateTime returnDate)
        {
            string commandText = "UPDATE Rentals SET ReturnDate = @ReturnDate, Status = 'Returned' WHERE RentalID = @RentalID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ReturnDate", returnDate),
                new SqlParameter("@RentalID", rentalId)
            };
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(commandText, parameters);
            return rowsAffected > 0;
        }


        // Select rentals for a specific client (Client view)
        public List<Rental> GetRentalsByClient(int clientId)
        {
            List<Rental> rentals = new List<Rental>();
            // Join with Games to get Game Title
            string commandText = @"SELECT R.RentalID, R.GameID, G.Title AS GameTitle, R.ClientID, R.RentalDate, R.ReturnDate, R.DueDate, R.Status
                                 FROM Rentals R
                                 JOIN Games G ON R.GameID = G.GameID
                                 WHERE R.ClientID = @ClientID ORDER BY R.RentalDate DESC";
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ClientID", clientId) };

            using (SqlDataReader reader = DatabaseHelper.ExecuteReader(commandText, parameters))
            {
                while (reader.Read())
                {
                    rentals.Add(new Rental
                    {
                        RentalID = reader.GetInt32(reader.GetOrdinal("RentalID")),
                        GameID = reader.GetInt32(reader.GetOrdinal("GameID")),
                        GameTitle = reader.GetString(reader.GetOrdinal("GameTitle")),
                        ClientID = reader.GetInt32(reader.GetOrdinal("ClientID")),
                        RentalDate = reader.GetDateTime(reader.GetOrdinal("RentalDate")),
                        ReturnDate = reader.IsDBNull(reader.GetOrdinal("ReturnDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ReturnDate")),
                        DueDate = reader.GetDateTime(reader.GetOrdinal("DueDate")),
                        Status = reader.GetString(reader.GetOrdinal("Status"))
                    });
                }
            }
            return rentals;
        }

        // Check if a client currently has an active rental for a specific game
        public bool IsGameCurrentlyRentedByUser(int gameId, int clientId)
        {
            string commandText = "SELECT COUNT(*) FROM Rentals WHERE GameID = @GameID AND ClientID = @ClientID AND Status = 'Rented'";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@GameID", gameId),
                new SqlParameter("@ClientID", clientId)
            };
            object result = DatabaseHelper.ExecuteScalar(commandText, parameters);
            return result != null && (int)result > 0;
        }


        public Rental GetRentalById(int rentalId)
        {
            string commandText = @"SELECT RentalID, GameID, ClientID, RentalDate, ReturnDate, DueDate, Status
                                 FROM Rentals
                                 WHERE RentalID = @RentalID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@RentalID", rentalId)
            };

            // Use DatabaseHelper to execute the query and get a reader
            using (SqlDataReader reader = DatabaseHelper.ExecuteReader(commandText, parameters))
            {
                // If a record is found, read it
                if (reader.Read())
                {
                    // Map the data from the reader to a Rental object
                    return new Rental
                    {
                        RentalID = reader.GetInt32(reader.GetOrdinal("RentalID")),
                        GameID = reader.GetInt32(reader.GetOrdinal("GameID")),
                        ClientID = reader.GetInt32(reader.GetOrdinal("ClientID")),
                        RentalDate = reader.GetDateTime(reader.GetOrdinal("RentalDate")),
                        // Check for DBNull for nullable fields (ReturnDate)
                        ReturnDate = reader.IsDBNull(reader.GetOrdinal("ReturnDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ReturnDate")),
                        DueDate = reader.GetDateTime(reader.GetOrdinal("DueDate")),
                        Status = reader.GetString(reader.GetOrdinal("Status"))
                    };
                }
            } // The 'using' statement ensures the reader and connection are closed

            return null; // Rental not found with the given ID
        }

 

        // And this method for the DeleteGame check in GameBLL:
        public bool HasActiveRentalsForGame(int gameId)
        {
             string commandText = "SELECT COUNT(*) FROM Rentals WHERE GameID = @GameID AND Status = 'Rented'";
             SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@GameID", gameId) };
             object result = DatabaseHelper.ExecuteScalar(commandText, parameters);
             return result != null && (int)result > 0;
        }



        // Checks if a user (client) has any active rentals (Status = 'Rented')
        public bool HasActiveRentalsForUser(int userId)
        {
            string commandText = "SELECT COUNT(*) FROM Rentals WHERE ClientID = @UserID AND Status = 'Rented'";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId)
            };

            try
            {
                // ExecuteScalar returns the first column of the first row, which is the count.
                object result = DatabaseHelper.ExecuteScalar(commandText, parameters);

                // If the result is not null and the count is greater than 0, the user has active rentals.
                return result != null && (int)result > 0;
            }
            catch (Exception ex)
            {
                // Log the exception (in a real application)
                Console.WriteLine("Error checking for active rentals for user: " + ex.Message);
                // Return true to be safe and prevent deletion if an error occurs
                return true;
            }
        }
    }
}