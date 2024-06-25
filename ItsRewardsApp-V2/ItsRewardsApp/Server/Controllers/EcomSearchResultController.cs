using ItsRewardsApp.Client.Pages;
using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ItsRewardsApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EcomSearchResultController : Controller
    {
        private readonly IEcomSearchResult _IEcomSearchResult;
        public EcomSearchResultController(IEcomSearchResult iEcomSearchResult)
        {
            _IEcomSearchResult = iEcomSearchResult;
        }
        [HttpGet("{storeId},{department},{description}")]
        public List<SearchResultViewModel> GetSearchResult(string storeId, string department, string description)
        {
            var result = _IEcomSearchResult.GetSearchResultByDescription(storeId, department, description);
            return result;
        }
    }
}
