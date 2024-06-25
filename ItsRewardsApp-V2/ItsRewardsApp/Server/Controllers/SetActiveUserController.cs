using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItsRewardsApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetActiveUserController : ControllerBase
    {
        private readonly ISetActiveUser _ISetActiveUser;
        public SetActiveUserController(ISetActiveUser setActiveUser)
        {
            _ISetActiveUser = setActiveUser;
        }
        [HttpPut]
        public void Put(string cellPhone)
        {
            _ISetActiveUser.sendPIN(cellPhone);
        }
    }
}
