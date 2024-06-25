using ItsRewardsApp.Shared.Models;

namespace ItsRewardsApp.Server.Interfaces
{
    public interface IpurchaseDetails
    {
        public List<PreOrder> PurchaseDetailsList(string cellNumber,string storeId);
    }
}
