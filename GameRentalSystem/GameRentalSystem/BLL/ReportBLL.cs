using GameRentalSystem.DAL;
using System; // Added for Exception handling
using System.Data;

namespace GameRentalSystem.BLL
{
    public class ReportBLL
    {
        private readonly ReportDAL _reportDAL = new ReportDAL();

        // Generate the report for the most rented game (Report a)
        public DataTable GetMostRentedGameReport()
        {
            try
            {
                return _reportDAL.GetMostRentedGameReport();
            }
            catch (Exception ex)
            {
               
                Console.WriteLine("Error generating Most Rented Game report: " + ex.Message); 
                return new DataTable(); 
            }
        }

        // Report b: Games that hadn’t any renters (clients) last month
        public DataTable GetGamesWithNoRentersLastMonth()
        {
            try
            {
                return _reportDAL.GetGamesWithNoRentersLastMonth();
            }
            catch (Exception ex)
            {
               
                Console.WriteLine("Error generating Games with No Renters Last Month report: " + ex.Message);
                return new DataTable();
            }
        }

        // Report c: Who was the renter (client) with the maximum renting last month?
        public DataTable GetClientWithMaxRentalsLastMonth()
        {
            try
            {
                return _reportDAL.GetClientWithMaxRentalsLastMonth();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error generating Client with Max Rentals Last Month report: " + ex.Message);
                return new DataTable();
            }
        }

        // Report d: Who was the vendor with the maximum renting out last month?
        public DataTable GetVendorWithMaxRentalsLastMonth()
        {
            try
            {
                return _reportDAL.GetVendorWithMaxRentalsLastMonth();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error generating Vendor with Max Rentals Last Month report: " + ex.Message);
                return new DataTable();
            }
        }

        // Report e: Who were the vendors whose games hadn’t any renting last month?
        public DataTable GetVendorsWithNoRentalsLastMonth()
        {
            try
            {
                return _reportDAL.GetVendorsWithNoRentalsLastMonth();
            }
            catch (Exception ex)
            {
               
                Console.WriteLine("Error generating Vendors with No Rentals Last Month report: " + ex.Message);
                return new DataTable();
            }
        }

        // Report f: Who were the vendors who didn’t add any game last year?
        public DataTable GetVendorsWhoDidNotAddGamesLastYear()
        {
            try
            {
                return _reportDAL.GetVendorsWhoDidNotAddGamesLastYear();
            }
            catch (Exception ex)
            {
               
                Console.WriteLine("Error generating Vendors Who Did Not Add Games Last Year report: " + ex.Message);
                return new DataTable();
            }
        }
    }
}