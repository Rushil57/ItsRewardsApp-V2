using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ItsRewardsApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DescriptionListController : Controller
    {
        private readonly IEcomSearchResult _IEcomSearchResult;
        public DescriptionListController(IEcomSearchResult iEcomSearchResult)
        {
            _IEcomSearchResult = iEcomSearchResult;
        }

        [HttpGet("{storeId},{description}")]
        public List<String> GetDescription(string storeId, string description)
        {
            var result = _IEcomSearchResult.GetDescritionList(storeId, description);
            return result;
        }
    }
}
