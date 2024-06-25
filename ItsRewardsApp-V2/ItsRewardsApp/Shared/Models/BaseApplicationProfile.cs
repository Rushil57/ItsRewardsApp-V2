using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsRewardsApp.Shared.Models
{
    public class BaseApplicationProfile
    {
        public virtual string Logo { get; set; }
        public virtual string Icon { get; set; }
        public virtual string MenuIcon { get; set; }
        public virtual string[] MenuOptions { get; set; }
        public virtual bool HasCheckOut { get; set; }
        public virtual string Title { get; set; }
    }
   
}
