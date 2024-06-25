using ItsRewardsApp.Client.Pages;
using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ItsRewardsApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EcomCouponsController : Controller
    {
        private readonly IEcomCoupons _IEcomCoupons;
        public EcomCouponsController(IEcomCoupons iEcomCoupons)
        {
            _IEcomCoupons = iEcomCoupons;
        }
        [HttpGet("{storeId}")]
        public List<String> GetDepartment(String StoreId)
        {
            var result = _IEcomCoupons.GetDepartmentsName(StoreId);
            return result;
        }
    }
}
