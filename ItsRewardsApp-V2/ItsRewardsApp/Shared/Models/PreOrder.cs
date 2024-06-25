using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsRewardsApp.Shared.Models
{
    public class PreOrder
    {
        public decimal Id { get; set; }
        public decimal VendorId { get; set; }
        public decimal CustomerId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? PickupTime { get; set; }
        public decimal Total { get; set; }
        public decimal? PaidAmount { get; set; }
        public string Status { get; set; }
        public string PaidStatus { get; set; }
        public string NeedAgeValidation { get; set; }
        public string Comments { get; set; }
        public int ItemCount { get; set; }
        public decimal Tax { get; set; }
        public decimal Discounts { get; set; }

    }
}
