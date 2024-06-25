using ItsRewardsApp.Client.Pages;
using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Server.Services;
using ItsRewardsApp.Shared.Models;
using ItsRewardsApp.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Plivo.XML;

namespace ItsRewardsApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatus _istatus;
        public StatusController(IStatus status)
        {
            _istatus = status;
        }
        [HttpGet("{cellNumber},{storeId}")]
        public List<StatusViewModel> Get(string cellNumber,string storeId)
        {
            var user = _istatus.GetCartDeatils(cellNumber, storeId);
            return user;
        }
        [HttpPut]
        public string Put(StatusViewModel statusData)
        {
            var response=_istatus.AddPaymentToken(statusData);
            return response;
        }
        //[HttpPut]
        //public string Put(StatusViewModel statusView)
        //{
        //  //var data = _istatus.UpdateCartData(statusView);
        //    //return data;
        //}

        //[HttpDelete("{id},{Quantity},{unitprice},{storeId},{OrderId}")]
        //public async Task<List<StatusViewModel>> Delete(decimal id,int Quantity,decimal unitprice, int storeId,decimal OrderId)
        //{
        //    return await Task.FromResult(_istatus.RemoveCartQuatity(id,Quantity,unitprice, storeId, OrderId));
        //}
    }
}   



