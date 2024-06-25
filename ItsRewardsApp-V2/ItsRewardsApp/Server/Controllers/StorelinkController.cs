using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace ItsRewardsApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorelinkController : ControllerBase
    {
        private readonly IStorelink _Istorelink;
        public StorelinkController(IStorelink istorelink)
        {
            _Istorelink = istorelink;
        }
        [HttpGet("{cellPhone},{storeLink}")]
        public UserLoyaltyStoreMappings getUserStoreMapping(string cellPhone,string storeLink)
        { 
            var user= _Istorelink.UserLoyaltyStoreMappings(cellPhone, storeLink);
            return user;
        }

    }
}
