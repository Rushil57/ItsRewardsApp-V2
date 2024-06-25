using ItsRewardsApp.Shared.ViewModels;

namespace ItsRewardsApp.Server.Interfaces
{
    public interface IQRcode
    {
        Task<string> GetQrcode(string CellPhone);
        Task<string> ReadQrCode(IFormFile files);
        Task<string> GetUserLoyaltyMapping(string CellPhone, string AltriaAccountNumber);
    }
}
