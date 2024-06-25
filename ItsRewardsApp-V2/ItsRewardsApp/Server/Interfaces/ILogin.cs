using ItsRewardsApp.Shared.ViewModels;

namespace ItsRewardsApp.Server.Interfaces
{
    public interface ILogin
    {
        LoginViewModel LoginValidation(LoginViewModel loginViewModel);
        string? SendMail(string Email);
        string ResetUserPassword(ResetPasswordViewModel model);
        string? SendMessage(string CellPhone);
    }
}
