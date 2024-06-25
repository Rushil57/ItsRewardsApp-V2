using ItsRewardsApp.Shared.Models;
using ItsRewardsApp.Shared.ViewModels;

namespace ItsRewardsApp.Server.Interfaces
{
    public interface IStatus
    {
        public List<StatusViewModel> GetCartDeatils(string Cellphone,string storeId);
        public string AddPaymentToken(StatusViewModel data);  //to add PaymentToken to ePreOrder table

        //public void UpdateCartData(StatusViewModel statusView);
        //public List<StatusViewModel> RemoveCartQuatity(decimal id,int Quantity,decimal unitprice, int storeId, decimal OrderId);
    }
}
