using System;

namespace GameRentalSystem.Models
{
    public class Game
    {
        public int GameID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public decimal Price { get; set; }
        public int VendorID { get; set; }
        public int CategoryID { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public int? AdminApproverID { get; set; }

        public string VendorName { get; set; }
        public string CategoryName { get; set; } 
    }
}