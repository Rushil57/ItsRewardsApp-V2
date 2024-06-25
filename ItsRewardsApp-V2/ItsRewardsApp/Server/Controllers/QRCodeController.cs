using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ItsRewardsApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRCodeController : ControllerBase
    {
        private readonly IQRcode _IQRcode;
        public QRCodeController(IQRcode iQRcode)
        {
            _IQRcode = iQRcode;
        }
        [HttpGet("{CellPhone}")]
        public async Task<ActionResult> GetQrcode(string CellPhone)
        {
            var result = await _IQRcode.GetQrcode(CellPhone);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> PostFile([FromForm] IFormFile files)
        {
            UploadResult uploadResult = new UploadResult();
            var result = await _IQRcode.ReadQrCode(files);
            if (result != "")
            {
                result = result.Split('=')[1];
                uploadResult.result = result;
            }
            return new CreatedResult("", uploadResult);
        }
        [HttpGet("{CellPhone},{AltriaAccountNumber}")]
        public async Task<ActionResult> GetUserLoyaltyMapping(string CellPhone, string AltriaAccountNumber)
        {
            var user = await _IQRcode.GetUserLoyaltyMapping(CellPhone, AltriaAccountNumber);
            return Ok(user);
        }

    }
}
