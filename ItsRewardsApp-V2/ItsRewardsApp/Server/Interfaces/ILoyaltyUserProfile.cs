using ItsRewardsApp.Shared.Models;
using ItsRewardsApp.Shared.ViewModels;

namespace ItsRewardsApp.Server.Interfaces
{
    public interface ILoyaltyUserProfile
    {
        public List<LoyaltyUserProfile> GetUserDetails();
        public LoyaltyUserProfileViewModel AddUser(LoyaltyUserProfileViewModel user);
        public void UpdateUserDetails(ProfileViewModel user);
        public ProfileViewModel GetUserData(string cellNumber);
        public void DeleteUser(int id);
        public string? GetUserDataByEmailAndPhone(string FieldValue, string PropertyName);
    }
}
