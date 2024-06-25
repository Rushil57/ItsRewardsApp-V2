using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsRewardsApp.Shared.ViewModels
{
    public class SearchResultViewModel
    {
        public int ProductId { get; set; } = 0;
        public decimal Price { get; set; } = decimal.Zero;
        public string Description { get; set; } = "";
        public string Department { get; set; } = "";
        public string Group { get; set; } = ""; 
        public string SKU { get; set; }
        public byte[]? ImageSrc { get; set; } 
        public string Image { get; set; }  // for searchresult image

    }
}
