using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.Models;
using ItsRewardsApp.Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ItsRewardsApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomeService _IHomeService;
        public HomeController(IHomeService iHomeService)
        {
            _IHomeService = iHomeService;

        }
        [HttpGet("{cellNumber},{longitude}, {latitude}")]
        public async Task<List<StoreDetailsViewModel>> Get(string cellNumber,string longitude, string latitude)
        {
            return await Task.FromResult(_IHomeService.GetStoreDetails(cellNumber, longitude, latitude).Result);
        }

        [HttpDelete("{id},{userProfileId}")]
        public async Task<List<UserLoyaltyStoreMappings>> Get(int id, int userProfileId)
        {
            return await Task.FromResult(_IHomeService.DeleteStore(id, userProfileId));
        }
        [HttpDelete("{cellNumber}")]
        public async Task<List<LoyaltyUserProfile>> Delete(string cellNumber)
        {
            return await Task.FromResult(_IHomeService.Deleteuser(cellNumber));
        }
    }
}
