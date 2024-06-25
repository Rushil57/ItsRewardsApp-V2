using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsRewardsApp.Shared.ViewModels
{
    public class StoreDetailsViewModel
    {
        public int StoreID { get; set; }
        public string StoreName { get; set; }
        public string? Address1 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip5 { get; set; }
        public bool isStoreActive { get; set; }
        public int UserProfileId { get; set; }

        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        public byte[]? ImageUrl { get; set; }
        public string Tier { get; set; }

    }
}
