using ItsRewardsApp.Shared.Models;
using ItsRewardsApp.Shared.ViewModels;

namespace ItsRewardsApp.Server.Interfaces
{
    public interface IStoreImageService
    {
        public Task<List<StoreDetailsViewModel>> GetStoreImage(string storeIds);
    }
}
