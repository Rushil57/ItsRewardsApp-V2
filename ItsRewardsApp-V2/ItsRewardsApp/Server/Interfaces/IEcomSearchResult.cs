using ItsRewardsApp.Shared.ViewModels;

namespace ItsRewardsApp.Server.Interfaces
{
    public interface IEcomSearchResult
    {
        public List<SearchResultViewModel> GetSearchResultByDescription(string storeId, string department, string description);

        public List<string> GetDescritionList(string storeId, string description);
    }
}
