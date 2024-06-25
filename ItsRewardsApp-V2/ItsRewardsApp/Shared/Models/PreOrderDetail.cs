using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsRewardsApp.Shared.Models
{
    public class PreOrderDetail
    {
        public decimal Id { get; set; }
        public decimal OrderId { get; set; }
        public decimal? PriceBookSKUMasterId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? LoyalityPrice { get; set; }
        public decimal? BasePrice { get; set; }
        public string CellPhone { get; set; } // to get the userprofileid 
        public string StoreId { get; set; }
    }
}
