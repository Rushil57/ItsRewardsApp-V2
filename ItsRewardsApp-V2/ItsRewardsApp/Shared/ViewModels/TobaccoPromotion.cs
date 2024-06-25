using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsRewardsApp.Shared.ViewModels
{
    public class TobaccoPromotion
    {
        public Marlboro Marlboro { get; set; }
        public LandM LandM { get; set; }
        public Copenhagen Copenhagen { get; set; }
        public Skoal Skoal { get; set; }
        public Black Black { get; set; }
        public string IsLocal { get; set; } //to check if local 
        public string ProjectApp { get; set; } // to check a which project profile used.
    }
    public class Marlboro
    {
        public string Name { get; set; }
        public string Link { get; set; }
    }
    public class LandM
    {
        public string Name { get; set; }
        public string Link { get; set; }
    }
    public class Copenhagen
    {
        public string Name { get; set; }
        public string Link { get; set; }
    }
    public class Skoal
    {
        public string Name { get; set; }
        public string Link { get; set; }
    }
    public class Black
    {
        public string Name { get; set; }
        public string Link { get; set; }
    }
}
