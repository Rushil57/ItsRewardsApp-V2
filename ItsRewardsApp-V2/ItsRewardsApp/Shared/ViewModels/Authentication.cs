using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsRewardsApp.Shared.ViewModels
{
    public class Authentication
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }
        public string GrantType { get; set; }
        public string SubScriptionkey { get; set; }
        public string AccessToken { get; set; }
        public string NewSubScriptionkey { get; set; }
        public string NewTokenGenerateKey { get; set; }
    }
}
