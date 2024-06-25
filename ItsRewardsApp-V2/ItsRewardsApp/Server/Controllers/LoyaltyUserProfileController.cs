using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.Models;
using ItsRewardsApp.Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItsRewardsApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltyUserProfileController : ControllerBase
    {
        private readonly ILoyaltyUserProfile _IUser;
        public LoyaltyUserProfileController(ILoyaltyUserProfile iUser)
        {
            _IUser = iUser;
        }
        [HttpGet]
        public async Task<List<LoyaltyUserProfile>> Get()
        {
            return await Task.FromResult(_IUser.GetUserDetails());
        }
        [HttpGet("{cellNumber}")]
        public IActionResult Get(string cellNumber)
        {
            ProfileViewModel user = _IUser.GetUserData(cellNumber);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }
        //[HttpGet("{EMail},{CellPhone},{Address1}")]
        //public string Get(string EMail,string CellPhone,string Address1)
        //{
        //    var user = _IUser.GetUserDataByEmailAndPhone(EMail,CellPhone, Address1);
        //    return user;
        //}
        [HttpPost]
        public LoyaltyUserProfileViewModel Post(LoyaltyUserProfileViewModel user)
        {
            var result= _IUser.AddUser(user);
            return result;
        }
        [HttpPut]
        public void Put(ProfileViewModel profileViewModel)
        {
            _IUser.UpdateUserDetails(profileViewModel);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _IUser.DeleteUser(id);
            return Ok();
        }

        [HttpGet("{FieldValue},{PropertyName}")]
        public string? GetUserDataByEmailAndPhone(string FieldValue, string PropertyName)
        {
            var user = _IUser.GetUserDataByEmailAndPhone(FieldValue, PropertyName);
            return user;
        }

        /// <summary>
        /// Helper function to update for status of confirmation s
        /// </summary>
        /// <returns></returns>
        //[HttpPut]
        //[Route("Confirmed")]
        //public IHttpActionResult PinConfirmed()
        //{
        //    if (string.IsNullOrWhiteSpace(Request.Headers.Authorization?.Parameter))
        //        return BadRequest();
        //    Encoding encoding;
        //    string[] Values;
        //    getBasicEncryption(out encoding, out Values);

        //    bool Email = Values[0].Contains("@");
        //    string ScrubPhone = Email ? Values[0].Trim() :
        //             Values[0].Replace("-", "").Replace(".", "").Replace("/", string.Empty);
        //    if (string.IsNullOrEmpty(ScrubPhone))
        //        return BadRequest();
        //    HttpStatusCode statusCode = HttpStatusCode.OK;
        //    try
        //    {
        //        using (LoyaltyUserDB context = new LoyaltyUserDB(_userDb))
        //        {
        //            LoyaltyUserProfile loyalty = context.Users.FirstOrDefault(
        //                                    m => m.CellPhone == ScrubPhone);
        //            if (loyalty != null)
        //            {
        //                loyalty.isActive = "Y";
        //                context.Entry(loyalty).State = EntityState.Modified;
        //                statusCode = context.SaveChanges() > 0 ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
        //            }
        //            else
        //                statusCode = HttpStatusCode.NotFound;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log(ex, "Unable to confirm users");
        //        statusCode = HttpStatusCode.InternalServerError;
        //    }
        //    return ResponseMessage(new HttpResponseMessage(statusCode));
        //}
    }
}
