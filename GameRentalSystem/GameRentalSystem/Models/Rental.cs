using System;

namespace GameRentalSystem.Models
{
    public class Rental
    {
        public int RentalID { get; set; }
        public int GameID { get; set; }
        public int ClientID { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }

        
        public string GameTitle { get; set; } 
        public string ClientName { get; set; } 
    }
}