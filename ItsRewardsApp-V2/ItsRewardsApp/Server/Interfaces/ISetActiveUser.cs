using ItsRewardsApp.Shared.ViewModels;

namespace ItsRewardsApp.Server.Interfaces
{
    public interface ISetActiveUser
    {
        public void sendPIN(string cellPhone);
    }
}
