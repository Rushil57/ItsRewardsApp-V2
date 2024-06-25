using ItsRewardsApp.Shared.Models;

namespace ItsRewardsApp.Server.Interfaces
{
    public interface IStorelink
    {
        UserLoyaltyStoreMappings UserLoyaltyStoreMappings(string CellPhone,string storeLink);
    }
}
