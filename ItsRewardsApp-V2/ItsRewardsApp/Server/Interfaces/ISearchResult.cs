using ItsRewardsApp.Shared.Models;
using ItsRewardsApp.Shared.ViewModels;

namespace ItsRewardsApp.Server.Interfaces
{
    public interface ISearchResult
    {
        public PreOrderDetail AddPreOrderDetail(PreOrderDetail orderDetail);
        public List<SearchResultViewModel> GetSearchResultForPromotionId(string id,string storeId);
    }
}
