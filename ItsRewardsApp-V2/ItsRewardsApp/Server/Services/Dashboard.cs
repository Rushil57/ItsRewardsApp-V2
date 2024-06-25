using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.Models;
using ItsRewardsApp.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
namespace ItsRewardsApp.Server.Services
{
    public class Dashboard : IDashboard
    {
        private readonly LoyaltyBaseDBContext _dbContext = new();
        private readonly LoyaltyUserDBContext _dbUserContext = new();
        public Dashboard(LoyaltyBaseDBContext dbContext, LoyaltyUserDBContext dbUserContext)
        {
            _dbContext = dbContext;
            _dbUserContext = dbUserContext;
        }
        public StoreRetailChoiceDataViewModel GetStoreNameByAltriaAccountNumber(string AltriaAccountNumber, string cellNumber)
        {
            try
            {
                var Result = from r in _dbContext.Rebates
                             join s in _dbContext.Stores on r.StoreID equals s.StoreID
                             where r.AltriaAccountNumber == AltriaAccountNumber || r.RCN==AltriaAccountNumber 
                             select new StoreViewModel()
                             {
                                 Id = s.StoreID,
                                 StoreName = s.StoreName,
                                 tiernumber = r.Tier
                             };
                var storeRecord = Result.FirstOrDefault();

                StoreRetailChoiceDataViewModel storeRetailChoiceDataViewModel = new StoreRetailChoiceDataViewModel();

                if (storeRecord != null)
                {
                    storeRetailChoiceDataViewModel.StoreID = storeRecord.Id;
                    storeRetailChoiceDataViewModel.StoreName = storeRecord.StoreName;
                    storeRetailChoiceDataViewModel.Tier = storeRecord.tiernumber;
                }

                var userProfile = _dbUserContext.LoyaltyUserProfile.Where(x => x.CellPhone == cellNumber).FirstOrDefault();

                if (userProfile != null)
                {
                    storeRetailChoiceDataViewModel.FirstName = userProfile.FirstName;
                    storeRetailChoiceDataViewModel.LastName = userProfile.LastName;

                    storeRetailChoiceDataViewModel.DateOfBirth = userProfile.BirthDate;
                    storeRetailChoiceDataViewModel.EmailAddress = userProfile.EMail;
                    storeRetailChoiceDataViewModel.CertificationCD = "Y";
                    storeRetailChoiceDataViewModel.LoyaltyId = userProfile.CellPhone;

                    var addresslist = new CurrentAddressViewModel();
                    addresslist.AddressLine1 = userProfile.Address1;
                    addresslist.City = userProfile.City;
                    addresslist.state = userProfile.State;
                    addresslist.Zip = userProfile.ZipCode;

                    storeRetailChoiceDataViewModel.CurrentAddress = addresslist;
                }
                return storeRetailChoiceDataViewModel;

            }
            catch
            {
                throw;
            }
        }

        public TobaccoRebate GetStoreNameByStoreId(int StoreID)
        {
            var storedetails = _dbContext.Rebates.Where(x => x.StoreID == StoreID).FirstOrDefault();
            return storedetails;
        }

        public StoreRetailChoiceDataViewModel PostRetailChoice(StoreRetailChoiceDataViewModel storeRetailChoiceDataViewModel)
        {
            return storeRetailChoiceDataViewModel;
        }
    }
}
