using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Server.Services;
using ItsRewardsApp.Shared.Models;
using ItsRewardsApp.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ItsRewardsApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchResultController : ControllerBase
    {
        private readonly ISearchResult _ISearchResult;
        public SearchResultController(ISearchResult iSearchResult)
        {
            _ISearchResult = iSearchResult;
        }
        [HttpGet("{promotionId},{storeId}")]
        public List<SearchResultViewModel> Get(string promotionId,string storeId)
        {
            var result = _ISearchResult.GetSearchResultForPromotionId(promotionId, storeId);
            return result;
        }
        [HttpPost]
        public PreOrderDetail Post(PreOrderDetail orderDetail)
        {
            var result = _ISearchResult.AddPreOrderDetail(orderDetail);
            return result;
        }
    }
}
