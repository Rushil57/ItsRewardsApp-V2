using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItsRewardsApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin _ILogin;
        public LoginController(ILogin ilogin)
        {
            _ILogin = ilogin;
        }
        [HttpPost]
        public LoginViewModel Post(LoginViewModel loginViewModel)
        {
            var result = _ILogin.LoginValidation(loginViewModel);
            return result;
        }
        [HttpGet("{CellPhone}")]
        public string Get(string CellPhone)
        {
            //var result = _ILogin.SendMail(Email);
            //return "";
            var result = _ILogin.SendMessage(CellPhone);
            return result;
            //if (user != null)
            //{
            //    return Ok(user);
            //}
            //return NotFound();
        }
        [HttpPut]
        public string Put(ResetPasswordViewModel model)
        {
            var result = _ILogin.ResetUserPassword(model);
            return result;
        }
    }
}
