using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.ViewModels;

namespace ItsRewardsApp.Server.Services
{
    public class Coupons : ICoupons
    {
        private readonly LoyaltyBaseDBContext _dbContext = new();
        private readonly LoyaltyUserDBContext _dbUserContext = new();
        public Coupons(LoyaltyBaseDBContext dbContext, LoyaltyUserDBContext dbUserContext)
        {
            _dbContext = dbContext;
            _dbUserContext = dbUserContext;
        }
        public List<CouponsViewModel> GetCouPons(List<CouponsViewModel> coupons)
        {
            return coupons;
        }
    }
}
