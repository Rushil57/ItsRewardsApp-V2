using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Plivo.XML;

namespace ItsRewardsApp.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ResendPinController : ControllerBase
    {
        private readonly IResendPin _IResendPin;
        public ResendPinController(IResendPin iResendPin)
        {
            _IResendPin = iResendPin;
        }
        [HttpPut]
        public void Put(ProfileViewModel profileViewModel)
        {
            _IResendPin.ResendPIN(profileViewModel);
        }
    }
}
