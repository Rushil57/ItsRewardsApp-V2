using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsRewardsApp.Shared.Models
{
    public class ItRewardApplicationProfile:BaseApplicationProfile
    {
        public override string Logo { get { return "images/Its_Reward_PNG.png"; } }
        public override string Icon { get { return "images/Its_Reward_PNG.png"; } }
        public override string MenuIcon { get { return "images/itsRewardsLogo.jpg"; } }
        public override string Title { get { return "Tobacco Promotions"; } }
    }
}
