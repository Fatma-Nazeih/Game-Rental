using System;

namespace GameRentalSystem.Models
{
    public class AdminRequest
    {
        public int RequestID { get; set; }

        public int ClientID { get; set; }

        public DateTime RequestDate { get; set; }

        public string Status { get; set; }

        public int? AdminApproverID { get; set; }

        public DateTime? ApprovalDate { get; set; }
        public string ClientUsername { get; set; } 
        public string ClientFullName { get; set; }
    }
}
