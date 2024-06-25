using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsRewardsApp.Shared.Models
{
    public class tblstore_online
    {
        public decimal Id { get; set; }
        public int StoreId { get; set; }
        public string Public_key { get; set; }
        public char AllowDeferedCash { get;set;}
        public char Active { get; set; }
        public string Storelink { get; set; }
        public char whenchanged { get; set; }
    }
}
