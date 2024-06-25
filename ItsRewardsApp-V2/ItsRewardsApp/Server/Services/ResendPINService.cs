using ItsRewardsApp.Server.Helper;
using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.Models;
using ItsRewardsApp.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using System.Text;

namespace ItsRewardsApp.Server.Services
{
    public class ResendPINService: IResendPin
    {
        private readonly LoyaltyUserDBContext _dbContext = new();
        public ResendPINService(LoyaltyUserDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void ResendPIN(ProfileViewModel profileViewModel)
        {
            try
            {
                LoyaltyUserProfile user = new LoyaltyUserProfile();
                user = _dbContext.LoyaltyUserProfile.Where(x => x.CellPhone == profileViewModel.CellPhone).FirstOrDefault();
                Random generator = new Random();
                String pin = generator.Next(0, 1000000).ToString("D6");
                if (user.UserProfileID != 0)
                {
                    user.Pinnumber = pin;
                    _dbContext.Entry(user).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                }
                var message = "ItsReward's Confirmation Pin:" + pin;
                CommonHelper commonHelper = new CommonHelper();
                commonHelper.SendToMobile(message, user.CellPhone);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
    
}
