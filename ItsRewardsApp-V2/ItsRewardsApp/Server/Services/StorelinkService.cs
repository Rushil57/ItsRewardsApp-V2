using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.Models;

namespace ItsRewardsApp.Server.Services
{
    public class StorelinkService : IStorelink
    {
        private readonly LoyaltyUserDBContext _dbContext = new();
        public StorelinkService(LoyaltyUserDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public UserLoyaltyStoreMappings UserLoyaltyStoreMappings(string CellPhone, string StoreLink)
        {
            UserLoyaltyStoreMappings mappings = new UserLoyaltyStoreMappings();
            try
            {
                var userProfileId = _dbContext.LoyaltyUserProfile.Where(x => x.CellPhone == CellPhone).Select(a => a.UserProfileID).FirstOrDefault();
                var storeId = _dbContext.Tblstore_Online.Where(x => x.Storelink == StoreLink).Select(a=>a.StoreId).FirstOrDefault();
                if (userProfileId > 0 && storeId > 0)
                {
                    mappings = _dbContext.UserLoyaltyStoreMappings.Where(x => x.StoreID == storeId && x.UserProfileID == userProfileId).FirstOrDefault();
                }
                if (mappings==null && storeId > 0 && userProfileId > 0)
                {
                    UserLoyaltyStoreMappings map = new UserLoyaltyStoreMappings();
                    map.StoreID = storeId;
                    map.UserProfileID = userProfileId;
                    map.CreateData = DateTime.Now;
                    map.LastPurchase = DateTime.Now;
                    _dbContext.UserLoyaltyStoreMappings.Add(map);
                    _dbContext.SaveChanges();

                    mappings = _dbContext.UserLoyaltyStoreMappings.Where(x => x.StoreID == storeId && x.UserProfileID == userProfileId).FirstOrDefault();
                }
                return mappings;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
