using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.Models;
using ItsRewardsApp.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using ZXing;
using ZXing.QrCode;

namespace ItsRewardsApp.Server.Services
{
    public class QRcode : IQRcode
    {
        private readonly LoyaltyUserDBContext _dbContext = new();
        private readonly LoyaltyBaseDBContext _dbBaseContext = new();
        public QRcode(LoyaltyUserDBContext dbContext, LoyaltyBaseDBContext dbBaseContext)
        {
            _dbContext = dbContext;
            _dbBaseContext = dbBaseContext;
        }
        public async Task<string> GetQrcode(string CellPhone)
        {
            LoyaltyUserProfile user = new LoyaltyUserProfile();
            string password = string.Empty;
            user = _dbContext.LoyaltyUserProfile.Where(x => x.CellPhone == CellPhone).FirstOrDefault();
            var QrcodeValue = "";
            if (user != null)
            {
                var result = "http://itsreward.com/users?cell=" + user.CellPhone + "&email=" + user.EMail + "&lastname=" + user.LastName + "&firstname=" + user.FirstName + "&address=" + user.Address1 + "&city=" + user.City + "&state=" + user.State + "&zip=" + user.ZipCode;
                using (MemoryStream ms = new MemoryStream())
                {
                    QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                    QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(result, QRCodeGenerator.ECCLevel.Q);
                    QRCode qRCode = new QRCode(qRCodeData);
                    using (Bitmap bitmap = qRCode.GetGraphic(20))
                    {
                        bitmap.Save(ms, ImageFormat.Png);
                        QrcodeValue = "data:image/png;Base64," + Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            return QrcodeValue;
        }
        public async Task<string> ReadQrCode(IFormFile files)
        {
            var resultScanCode = "";
            var file = files;
            try
            {
                string mainData = "";
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        mainData = Convert.ToBase64String(fileBytes);
                    }
                }
                byte[] bytes = Convert.FromBase64String(mainData);
                Bitmap image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = (Bitmap)Image.FromStream(ms);
                }


                var reader = new BarcodeReaderGeneric();

                using (image)
                {
                    LuminanceSource source;
                    source = new ZXing.Windows.Compatibility.BitmapLuminanceSource(image);
                    Result result = reader.Decode(source);
                    if (result != null)
                    {
                        resultScanCode = result.Text;
                    }
                }
            }
            catch (IOException ex)
            {

            }
            return resultScanCode;
        }

        public async Task<string> GetUserLoyaltyMapping(string CellPhone, string AltriaAccountNumber)
        {
            var result = "";
            LoyaltyUserProfile? loyalty = _dbContext.LoyaltyUserProfile.Where(x => x.CellPhone == CellPhone).FirstOrDefault();
            if (loyalty != null)
            {
                var Result = from r in _dbBaseContext.Rebates
                             join s in _dbBaseContext.Stores on r.StoreID equals s.StoreID
                             where r.AltriaAccountNumber == AltriaAccountNumber
                             select s;
                var storeRecord = Result.FirstOrDefault();
                var storeId = storeRecord?.StoreID; 
                if(storeId != null)
                {
                    //UserLoyaltyStoreMappings userLoyaltyStore = loyalty.Mappings.FirstOrDefault(m => m.StoreID == storeId);
                    UserLoyaltyStoreMappings userLoyaltyStore = _dbContext.UserLoyaltyStoreMappings.Where(x => x.StoreID == storeId && x.UserProfileID == loyalty.UserProfileID).FirstOrDefault();
                    if (userLoyaltyStore == null)
                    {
                        userLoyaltyStore = new UserLoyaltyStoreMappings()
                        {
                            StoreID = storeId,
                            CreateDate = DateTime.Now,
                            UserProfileID = loyalty.UserProfileID
                        };
                        _dbContext.UserLoyaltyStoreMappings.Add(userLoyaltyStore);
                        //loyalty.Mappings.Add(userLoyaltyStore);

                        //_dbContext.Entry<LoyaltyUserProfile>(loyalty).State = EntityState.Modified;
                        var statusCode = _dbContext.SaveChanges() > 0 ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
                        if (statusCode == HttpStatusCode.OK)
                        {
                            result = "Store mapped successfully.";
                        }
                    }
                    else
                    {
                        result = "Store is already mapped.";
                    }
                }
                else
                {
                    result = "store not found.";
                }
            }
            else
            {
                result = "User not found!";
            }
            return result;
        }
    }
}
