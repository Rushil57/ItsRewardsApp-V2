using ItsRewardsApp.Server.Helper;
using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.Models;
using ItsRewardsApp.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ItsRewardsApp.Server.Services
{
    public class SetActiveUserService:ISetActiveUser
    {
        private readonly LoyaltyUserDBContext _dbContext = new();
        public SetActiveUserService(LoyaltyUserDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void sendPIN(string cellphone)
        {
           try
           {
                LoyaltyUserProfile user = new LoyaltyUserProfile();
                user = _dbContext.LoyaltyUserProfile.Where(x => x.CellPhone == cellphone).FirstOrDefault();
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
                 commonHelper.SendToMobile(message, cellphone);
           }
            catch (Exception ex)
           {
              throw;
           }
        }
    }
}
