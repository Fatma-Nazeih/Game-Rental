using System;
using System.Data;
using System.Data.SqlClient;

namespace GameRentalSystem.DAL
{
    public class ReportDAL
    {
        // Generate 1 Meaningful Report (Requirement a: Most rented game)
        
        public DataTable GetMostRentedGameReport()
        {

            string commandText = @"
                SELECT TOP 1
                    G.Title AS GameTitle,
                    COUNT(R.RentalID) AS TotalRentals
                FROM Games G
                JOIN Rentals R ON G.GameID = R.GameID
                GROUP BY G.GameID, G.Title
                ORDER BY TotalRentals DESC";

            return DatabaseHelper.ExecuteDataTable(commandText);
        }

        // Example for Report b (Games with no renters last month):
        public DataTable GetGamesWithNoRentersLastMonth()
        {
            
            string commandText = @"
                SELECT G.Title
                FROM Games G
                LEFT JOIN Rentals R ON G.GameID = R.GameID
                                    AND R.RentalDate >= DATEADD(month, DATEDIFF(month, 0, GETDATE()) - 1, 0) -- First day of last month
                                    AND R.RentalDate < DATEADD(month, DATEDIFF(month, 0, GETDATE()), 0) -- First day of current month
                WHERE R.RentalID IS NULL -- Game was not rented in the last month period
                GROUP BY G.GameID, G.Title; -- Group to avoid duplicates if a game has multiple approved entries (unlikely with schema, but safe)
             ";

            return DatabaseHelper.ExecuteDataTable(commandText);
        }

        // Example for Report c (Renter with maximum renting last month):
        public DataTable GetClientWithMaxRentalsLastMonth()
        {
            // Get the first day of last month and the first day of the current month
            string commandText = @"
                SELECT TOP 1
                    U.Username,
                    U.FullName,
                    COUNT(R.RentalID) AS TotalRentalsLastMonth
                FROM Users U
                JOIN Rentals R ON U.UserID = R.ClientID
                WHERE U.Role = 'Client'
                  AND R.RentalDate >= DATEADD(month, DATEDIFF(month, 0, GETDATE()) - 1, 0) -- First day of last month
                  AND R.RentalDate < DATEADD(month, DATEDIFF(month, 0, GETDATE()), 0) -- First day of current month
                GROUP BY U.UserID, U.Username, U.FullName
                ORDER BY TotalRentalsLastMonth DESC";

            return DatabaseHelper.ExecuteDataTable(commandText);
        }

        // Report d: Who was the vendor with the maximum renting out last month?
        public DataTable GetVendorWithMaxRentalsLastMonth()
        {
            // Get the first day of last month and the first day of the current month
            string commandText = @"
                SELECT TOP 1
                    U.Username AS VendorUsername,
                    U.FullName AS VendorFullName,
                    V.CompanyName,
                    COUNT(R.RentalID) AS TotalRentalsLastMonth
                FROM Users U
                JOIN Vendors V ON U.UserID = V.UserID
                JOIN Games G ON V.VendorID = G.VendorID
                JOIN Rentals R ON G.GameID = R.GameID
                WHERE R.RentalDate >= DATEADD(month, DATEDIFF(month, 0, GETDATE()) - 1, 0) -- First day of last month
                  AND R.RentalDate < DATEADD(month, DATEDIFF(month, 0, GETDATE()), 0) -- First day of current month
                GROUP BY U.UserID, U.Username, U.FullName, V.CompanyName
                ORDER BY TotalRentalsLastMonth DESC";

            return DatabaseHelper.ExecuteDataTable(commandText);
        }

        // Report e: Who were the vendors whose games hadn’t any renting last month?
        public DataTable GetVendorsWithNoRentalsLastMonth()
        {
            // Get the first day of last month and the first day of the current month
            string commandText = @"
                SELECT DISTINCT
                    U.Username AS VendorUsername,
                    U.FullName AS VendorFullName,
                    V.CompanyName
                FROM Users U
                JOIN Vendors V ON U.UserID = V.UserID
                WHERE V.VendorID NOT IN (
                    -- Subquery to find VendorIDs whose games *were* rented last month
                    SELECT DISTINCT G.VendorID
                    FROM Games G
                    JOIN Rentals R ON G.GameID = R.GameID
                    WHERE R.RentalDate >= DATEADD(month, DATEDIFF(month, 0, GETDATE()) - 1, 0) -- First day of last month
                      AND R.RentalDate < DATEADD(month, DATEDIFF(month, 0, GETDATE()), 0) -- First day of current month
                )";
            return DatabaseHelper.ExecuteDataTable(commandText);
        }

        // Report f: Who were the vendors who didn’t add any game last year?
        public DataTable GetVendorsWhoDidNotAddGamesLastYear()
        {
            // Get the first day of last year and the first day of the current year
            string commandText = @"
                SELECT DISTINCT
                    U.Username AS VendorUsername,
                    U.FullName AS VendorFullName,
                    V.CompanyName
                FROM Users U
                JOIN Vendors V ON U.UserID = V.UserID
                WHERE V.VendorID NOT IN (
                    -- Subquery to find VendorIDs who *did* add/approve games last year
                    SELECT DISTINCT G.VendorID
                    FROM Games G
                    -- Using ApprovalDate as the proxy for when a game became available/added by vendor (and approved)
                    WHERE G.ApprovalDate IS NOT NULL -- Only considering approved games
                      AND G.ApprovalDate >= DATEADD(year, DATEDIFF(year, 0, GETDATE()) - 1, 0) -- First day of last year
                      AND G.ApprovalDate < DATEADD(year, DATEDIFF(year, 0, GETDATE()), 0) -- First day of current year
                )";

            return DatabaseHelper.ExecuteDataTable(commandText);
        }
    }
}