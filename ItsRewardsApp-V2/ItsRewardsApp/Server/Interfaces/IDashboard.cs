using ItsRewardsApp.Shared.Models;
using ItsRewardsApp.Shared.ViewModels;

namespace ItsRewardsApp.Server.Interfaces
{
    public interface IDashboard
    {
        public StoreRetailChoiceDataViewModel GetStoreNameByAltriaAccountNumber(string AltriaAccountNumber,string cellNumber);
        public StoreRetailChoiceDataViewModel PostRetailChoice(StoreRetailChoiceDataViewModel storeRetailChoiceDataViewModel);

        public TobaccoRebate GetStoreNameByStoreId(int StoreID);


    }
}
