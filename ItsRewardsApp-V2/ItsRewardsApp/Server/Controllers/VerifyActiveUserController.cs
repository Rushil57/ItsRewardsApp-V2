using ItsRewardsApp.Server.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItsRewardsApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifyActiveUserController : ControllerBase
    {
         private readonly IVerifyActiveUser _IVerifyActiveUser;
         public VerifyActiveUserController(IVerifyActiveUser verifyActiveUser)
         {
            _IVerifyActiveUser = verifyActiveUser;
         }
         [HttpPut]
         public void Put(string cellPhone)
         {
            _IVerifyActiveUser.sendPIN(cellPhone);
         }
      
    }
}
