using ItsRewardsApp.Shared.ViewModels;

namespace ItsRewardsApp.Server.Interfaces
{
    public interface ICoupons
    {
        public List<CouponsViewModel> GetCouPons(List<CouponsViewModel> coupons);
    }
}
