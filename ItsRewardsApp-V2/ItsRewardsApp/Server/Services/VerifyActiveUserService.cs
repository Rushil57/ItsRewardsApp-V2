using ItsRewardsApp.Server.Helper;
using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ItsRewardsApp.Server.Services
{
    public class VerifyActiveUserService:IVerifyActiveUser
    {
        private readonly LoyaltyUserDBContext _dbContext = new();
        public VerifyActiveUserService(LoyaltyUserDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void sendPIN(string cellphone)
        {
            try
            {
                LoyaltyUserProfile user = new LoyaltyUserProfile();
                user = _dbContext.LoyaltyUserProfile.Where(x => x.CellPhone == cellphone).FirstOrDefault();
                if (user.UserProfileID != 0)
                {
                    user.isActive = 'Y';
                    _dbContext.Entry(user).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
