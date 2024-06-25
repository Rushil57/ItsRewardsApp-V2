using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsRewardsApp.Shared.Models
{
    public class EcommerceApplicationProfile:BaseApplicationProfile
    {
        public override string Logo { get { return "images/ecommerce.png"; } }
        public override string Icon { get { return "images/ecommerce.png"; } }
        public override string MenuIcon { get { return "images/ecommerce.png"; } }
        public override bool HasCheckOut { get; set; }
        public override string Title { get; set; }
    }
}
