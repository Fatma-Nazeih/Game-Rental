using GameRentalSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace GameRentalSystem.DAL
{
    public class GameDAL
    {
        // (if Vendor/Admin adds game): Insert a new Game
        public int InsertGame(Game game)
        {
            string commandText = @"INSERT INTO Games (Title, Description, ReleaseYear, Price, VendorID, CategoryID, IsApproved, ApprovalDate, AdminApproverID)
                                 OUTPUT INSERTED.GameID
                                 VALUES (@Title, @Description, @ReleaseYear, @Price, @VendorID, @CategoryID, @IsApproved, @ApprovalDate, @AdminApproverID)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Title", game.Title),
                new SqlParameter("@Description", game.Description ?? (object)DBNull.Value), 
                new SqlParameter("@ReleaseYear", game.ReleaseYear),
                new SqlParameter("@Price", game.Price),
                new SqlParameter("@VendorID", game.VendorID),
                new SqlParameter("@CategoryID", game.CategoryID),
                new SqlParameter("@IsApproved", game.IsApproved),
                // Use DBNull.Value for nullable fields if the value is null
                new SqlParameter("@ApprovalDate", game.ApprovalDate ?? (object)DBNull.Value),
                new SqlParameter("@AdminApproverID", game.AdminApproverID ?? (object)DBNull.Value)
            };
            object result = DatabaseHelper.ExecuteScalar(commandText, parameters);
            return result == null ? -1 : (int)result;
        }

        //  Update Game details (Admin/Vendor)
        public bool UpdateGame(Game game)
        {
            string commandText = @"UPDATE Games SET
                                 Title = @Title,
                                 Description = @Description,
                                 ReleaseYear = @ReleaseYear,
                                 Price = @Price,
                                 VendorID = @VendorID, -- Might not allow changing vendor post-creation
                                 CategoryID = @CategoryID,
                                 IsApproved = @IsApproved,
                                 ApprovalDate = @ApprovalDate,
                                 AdminApproverID = @AdminApproverID
                                 WHERE GameID = @GameID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Title", game.Title),
                new SqlParameter("@Description", game.Description ?? (object)DBNull.Value),
                new SqlParameter("@ReleaseYear", game.ReleaseYear),
                new SqlParameter("@Price", game.Price),
                new SqlParameter("@VendorID", game.VendorID),
                new SqlParameter("@CategoryID", game.CategoryID),
                new SqlParameter("@IsApproved", game.IsApproved),
                 new SqlParameter("@ApprovalDate", game.ApprovalDate ?? (object)DBNull.Value),
                new SqlParameter("@AdminApproverID", game.AdminApproverID ?? (object)DBNull.Value),
                new SqlParameter("@GameID", game.GameID)
            };
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(commandText, parameters);
            return rowsAffected > 0;
        }

        // Update Statement 3: Approve a Game (Admin)
        public bool ApproveGame(int gameId, int adminUserId)
        {
            string commandText = "UPDATE Games SET IsApproved = 1, ApprovalDate = GETDATE(), AdminApproverID = @AdminApproverID WHERE GameID = @GameID AND IsApproved = 0";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@AdminApproverID", adminUserId),
                new SqlParameter("@GameID", gameId)
            };
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(commandText, parameters);
            return rowsAffected > 0;
        }


        // Delete Statement 2 (with condition): Delete a Game IF it has no active rentals
        public bool DeleteGame(int gameId)
        {
            // Check if there are any active rentals for this game first (BLL should ideally do this check before calling)
            string checkRentalsCommand = "SELECT COUNT(*) FROM Rentals WHERE GameID = @GameID AND Status = 'Rented'";
            SqlParameter[] checkParams = new SqlParameter[] { new SqlParameter("@GameID", gameId) };
            object rentalCount = DatabaseHelper.ExecuteScalar(checkRentalsCommand, checkParams);

            if (rentalCount != null && (int)rentalCount > 0)
            {
                return false; // Cannot delete - active rentals exist
            }

            // Proceed with deletion if no active rentals
            string commandText = "DELETE FROM Games WHERE GameID = @GameID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@GameID", gameId)
            };
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(commandText, parameters);
            return rowsAffected > 0;
        }

        public bool RejectGame(int gameId)
        {
            // SQL UPDATE statement to set IsApproved to 0 (false)
            // and clear AdminApproverID and ApprovalDate for the given GameID.
            string commandText = @"
                UPDATE Games
                SET IsApproved = 0,
                    AdminApproverID = NULL,
                    ApprovalDate = NULL
                WHERE GameID = @GameID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@GameID", gameId)
            };

            try
            {
                // Execute the UPDATE command
                int rowsAffected = DatabaseHelper.ExecuteNonQuery(commandText, parameters);

                // Return true if at least one row was affected (game found and updated)
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error rejecting game in DAL: " + ex.Message);
                return false; // Update failed
            }
        }



        // Select Data that involves more than one table (using joins): Get All Approved Games with Vendor and Category Names
        public List<Game> GetApprovedGamesWithDetails()
        {
            List<Game> games = new List<Game>();
            string commandText = @"SELECT G.GameID, G.Title, G.Description, G.ReleaseYear, G.Price, G.VendorID, V.CompanyName AS VendorName, G.CategoryID, C.Name AS CategoryName, G.IsApproved
                                 FROM Games G
                                 JOIN Vendors V ON G.VendorID = V.VendorID
                                 JOIN Categories C ON G.CategoryID = C.CategoryID
                                 WHERE G.IsApproved = 1";

            using (SqlDataReader reader = DatabaseHelper.ExecuteReader(commandText))
            {
                while (reader.Read())
                {
                    games.Add(new Game
                    {
                        GameID = reader.GetInt32(reader.GetOrdinal("GameID")),
                        Title = reader.GetString(reader.GetOrdinal("Title")),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                        ReleaseYear = reader.GetInt32(reader.GetOrdinal("ReleaseYear")),
                        Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                        VendorID = reader.GetInt32(reader.GetOrdinal("VendorID")),
                        VendorName = reader.GetString(reader.GetOrdinal("VendorName")),
                        CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                        CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                        IsApproved = reader.GetBoolean(reader.GetOrdinal("IsApproved"))
                        // ApprovalDate and AdminApproverID not needed for client view
                    });
                }
            }
            return games;
        }

        // Select Data that involves more than one table (using joins): Get All Games with Vendor and Category Names (for Admin/Vendor)
        public List<Game> GetAllGamesWithDetails()
        {
            List<Game> games = new List<Game>();
            string commandText = @"SELECT G.GameID, G.Title, G.Description, G.ReleaseYear, G.Price, G.VendorID, V.CompanyName AS VendorName, G.CategoryID, C.Name AS CategoryName, G.IsApproved, G.ApprovalDate, G.AdminApproverID
                                 FROM Games G
                                 JOIN Vendors V ON G.VendorID = V.VendorID
                                 JOIN Categories C ON G.CategoryID = C.CategoryID";

            using (SqlDataReader reader = DatabaseHelper.ExecuteReader(commandText))
            {
                while (reader.Read())
                {
                    games.Add(new Game
                    {
                        GameID = reader.GetInt32(reader.GetOrdinal("GameID")),
                        Title = reader.GetString(reader.GetOrdinal("Title")),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                        ReleaseYear = reader.GetInt32(reader.GetOrdinal("ReleaseYear")),
                        Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                        VendorID = reader.GetInt32(reader.GetOrdinal("VendorID")),
                        VendorName = reader.GetString(reader.GetOrdinal("VendorName")),
                        CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                        CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                        IsApproved = reader.GetBoolean(reader.GetOrdinal("IsApproved")),
                        ApprovalDate = reader.IsDBNull(reader.GetOrdinal("ApprovalDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ApprovalDate")),
                        AdminApproverID = reader.IsDBNull(reader.GetOrdinal("AdminApproverID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("AdminApproverID"))
                    });
                }
            }
            return games;
        }

        // Select Games by VendorID (for Vendor view)
        public List<Game> GetGamesByVendorId(int vendorId)
        {
            List<Game> games = new List<Game>();
            string commandText = @"SELECT G.GameID, G.Title, G.Description, G.ReleaseYear, G.Price, G.VendorID, V.CompanyName AS VendorName, G.CategoryID, C.Name AS CategoryName, G.IsApproved, G.ApprovalDate, G.AdminApproverID
                                 FROM Games G
                                 JOIN Vendors V ON G.VendorID = V.VendorID
                                 JOIN Categories C ON G.CategoryID = C.CategoryID
                                 WHERE G.VendorID = @VendorID";
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@VendorID", vendorId) };

            using (SqlDataReader reader = DatabaseHelper.ExecuteReader(commandText, parameters))
            {
                while (reader.Read())
                {
                    games.Add(new Game
                    {
                        GameID = reader.GetInt32(reader.GetOrdinal("GameID")),
                        Title = reader.GetString(reader.GetOrdinal("Title")),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                        ReleaseYear = reader.GetInt32(reader.GetOrdinal("ReleaseYear")),
                        Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                        VendorID = reader.GetInt32(reader.GetOrdinal("VendorID")),
                        VendorName = reader.GetString(reader.GetOrdinal("VendorName")),
                        CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                        CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                        IsApproved = reader.GetBoolean(reader.GetOrdinal("IsApproved")),
                        ApprovalDate = reader.IsDBNull(reader.GetOrdinal("ApprovalDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ApprovalDate")),
                        AdminApproverID = reader.IsDBNull(reader.GetOrdinal("AdminApproverID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("AdminApproverID"))
                    });
                }
            }
            return games;
        }

        // Add this method to your existing GameDAL.cs file

        // Checks if a user who is a Vendor has any games associated with their VendorID
        public bool HasGamesByVendorUser(int userId)
        {
            // This query joins Users and Vendors to find the VendorID for the given UserID,
            // then checks if there are any games associated with that VendorID.
            string commandText = @"
                SELECT COUNT(G.GameID)
                FROM Games G
                JOIN Vendors V ON G.VendorID = V.VendorID
                WHERE V.UserID = @UserID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId)
            };

            try
            {
                // ExecuteScalar returns the count of games.
                object result = DatabaseHelper.ExecuteScalar(commandText, parameters);

                // If the result is not null and the count is greater than 0, the vendor user has games.
                return result != null && (int)result > 0;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error checking for games by vendor user: " + ex.Message);
                // Return true to be safe
                return true;
            }
        }

        // --- Previously provided GetGameById method (included again as requested) ---

        // Select a single Game by ID
        public Game GetGameById(int gameId)
        {
            string commandText = @"SELECT G.GameID, G.Title, G.Description, G.ReleaseYear, G.Price, G.VendorID, G.CategoryID, G.IsApproved, G.ApprovalDate, G.AdminApproverID
                                 FROM Games G
                                 WHERE G.GameID = @GameID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@GameID", gameId)
            };

            try
            {
                using (SqlDataReader reader = DatabaseHelper.ExecuteReader(commandText, parameters))
                {
                    if (reader.Read())
                    {
                        // Map the data from the reader to a Game object
                        return new Game
                        {
                            GameID = reader.GetInt32(reader.GetOrdinal("GameID")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                            ReleaseYear = reader.GetInt32(reader.GetOrdinal("ReleaseYear")),
                            Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                            VendorID = reader.GetInt32(reader.GetOrdinal("VendorID")),
                            CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                            IsApproved = reader.GetBoolean(reader.GetOrdinal("IsApproved")),
                            ApprovalDate = reader.IsDBNull(reader.GetOrdinal("ApprovalDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("ApprovalDate")),
                            AdminApproverID = reader.IsDBNull(reader.GetOrdinal("AdminApproverID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("AdminApproverID"))
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error getting game by ID: " + ex.Message);
            }
            return null; // Game not found or error occurred
        }
    }
}