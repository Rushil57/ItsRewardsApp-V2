/*
  5/6/2022 ali - controller for allowing text messages
 */

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plivo;
namespace ItsRewardsApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlivoController : ControllerBase
    {
        [HttpGet]
        [Route("Send/{Number}/{Pin}")]
        public bool SendText(string Number, string Pin)
        {
            var phloClient = new PhloApi("MANDNJMMU3ZMRIMTI1NJ", "ZDQxMzcyODEzNWE1MmE1MzQzMDgwMTYxMGY2ZDcz");
            var phloID = "f01f1fb6-0105-4272-bd32-eee63353ef63";
            bool Success = false;
            try
            {
                var phlo = phloClient.Phlo.Get(phloID);
            Dictionary<string, object> result = new Dictionary<string, object>();
            //2032401355&from=1234569
            result.Add("to", Number);
            result.Add("from", Pin);
           
            

                var value = phlo.Run(result);
                Success = true;
            }
            catch (Exception)
            {

            }
            return Success;
        }
    }
}
