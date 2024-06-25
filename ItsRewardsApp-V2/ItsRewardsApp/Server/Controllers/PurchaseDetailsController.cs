using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace ItsRewardsApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseDetailsController : ControllerBase
    {
        private readonly IpurchaseDetails _ipurchaseDetails;
        public PurchaseDetailsController(IpurchaseDetails ipurchaseDetails)
        {
            _ipurchaseDetails = ipurchaseDetails;
        }
        [HttpGet("{cellNumber},{storeId}")]
        public List<PreOrder> Get(string cellNumber, string storeId)
        {
            var user = _ipurchaseDetails.PurchaseDetailsList(cellNumber, storeId);
            return user;
        }
    }
}
