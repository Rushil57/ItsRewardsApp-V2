using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Server.Services;
using ItsRewardsApp.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ItsRewardsApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreImageController : ControllerBase
    {
        private readonly IStoreImageService _IStoreImageService;
        public StoreImageController(IStoreImageService iStoreImageService)
        {
            _IStoreImageService = iStoreImageService;

        }
        [HttpGet("{storeIds}")]
        public async Task<List<StoreDetailsViewModel>> Get(string storeIds)
        {
            return await Task.FromResult(_IStoreImageService.GetStoreImage(storeIds).Result);
        }
    }
}
