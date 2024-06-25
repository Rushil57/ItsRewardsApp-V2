using ItsRewardsApp.Shared.Models;
using ItsRewardsApp.Shared.ViewModels;

namespace ItsRewardsApp.Server.Interfaces
{
    public interface IHomeService
    {
        public Task<List<StoreDetailsViewModel>> GetStoreDetails(string cellNumber, string longitude, string latitude);

        public List<UserLoyaltyStoreMappings> DeleteStore(int id, int userProfileId);

        public List<LoyaltyUserProfile> Deleteuser(string cellNumber);
    }
}
