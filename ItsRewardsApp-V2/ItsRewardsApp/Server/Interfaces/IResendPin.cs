using ItsRewardsApp.Shared.ViewModels;

namespace ItsRewardsApp.Server.Interfaces
{
    public interface IResendPin
    {
        public void ResendPIN(ProfileViewModel user);
    }
}
