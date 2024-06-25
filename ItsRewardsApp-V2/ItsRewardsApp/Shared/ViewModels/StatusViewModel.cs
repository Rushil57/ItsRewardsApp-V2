using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsRewardsApp.Shared.ViewModels
{
    public class StatusViewModel
    {
        public string storeId { get; set; } = "";
        public decimal OrderId { get; set; } = 0;
        public decimal Id { get; set; } = 0;
        public string Name { get; set; } = "";
        public int Quantity { get; set; } = 0;
        public decimal TotalPrice { get; set; } = 0; //orderdetail table
        public decimal Tax { get; set; } = 0;
        public decimal Discounts { get; set; } = 0;
        public decimal Total { get; set; } = 0; //preorder table
        public string Address1 { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string ZipCode { get; set; } = "";
        public decimal UnitPrice { get; set; } = 0;
        public decimal PriceBookSKUMasterId { get; set; }
        public string PaymentToken { get; set; }
        public string cellnumber { get; set; }
        public Dictionary<string, object> Storage { get; set; } = new();
    }
}
