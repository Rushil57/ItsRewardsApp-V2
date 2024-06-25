using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.Models;
using ItsRewardsApp.Shared.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Web;
namespace ItsRewardsApp.Server.Services
{
    public class HomeService : IHomeService
    {
        private readonly LoyaltyUserDBContext _dbContext = new();
        private readonly LoyaltyBaseDBContext _dbBaseContext = new();
        private readonly IConfiguration _configuration;
        public HomeService(LoyaltyUserDBContext dbContext, LoyaltyBaseDBContext dbBaseContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _dbBaseContext = dbBaseContext;
            _configuration = configuration;
        }

        public List<UserLoyaltyStoreMappings> DeleteStore(int id, int userProfileId)
        {
          // bool confirmation = false;
            var userlist = _dbContext.UserLoyaltyStoreMappings.Where(x => x.UserProfileID == userProfileId && x.StoreID == id).FirstOrDefault();
            if (userlist != null)
            {
                _dbContext.UserLoyaltyStoreMappings.Remove(userlist);
                _dbContext.SaveChanges();
            }
            var list = _dbContext.UserLoyaltyStoreMappings.Where(x => x.UserProfileID == userProfileId).ToList();
            return list;
        }

        public List<LoyaltyUserProfile> Deleteuser(string cellNumber)
        {
            var deletelist = _dbContext.LoyaltyUserProfile.Where(x => x.CellPhone == cellNumber).FirstOrDefault();

            if (deletelist != null)
            {
                var storelist = _dbContext.UserLoyaltyStoreMappings.Where(x => x.UserProfileID == deletelist.UserProfileID).ToList();
                _dbContext.UserLoyaltyStoreMappings.RemoveRange(storelist);
                _dbContext.SaveChanges();

                _dbContext.LoyaltyUserProfile.Remove(deletelist);
                _dbContext.SaveChanges();
            }
            var list = _dbContext.LoyaltyUserProfile.Where(x => x.CellPhone == cellNumber).ToList();
            return list;
        }
       public async Task <List<StoreDetailsViewModel>> GetStoreDetails(string cellNumber, string longitude, string latitude)
       {
            var stringconnect = "";
            List<int?> storeListIds = new List<int?>();
            List<StoreDetailsViewModel> modelList = new List<StoreDetailsViewModel>();
            LoyaltyUserProfile? loyalty = _dbContext.LoyaltyUserProfile.Where(x => x.CellPhone == cellNumber).FirstOrDefault();
            if (loyalty != null)
            {
                storeListIds = _dbContext.UserLoyaltyStoreMappings.Where(x=> x.UserProfileID == loyalty.UserProfileID).Select(x => x.StoreID).ToList();
                if (storeListIds != null && storeListIds.Count > 0)
                {
                    var store = _dbBaseContext.Stores.Where(m => storeListIds.Any(c => c == m.StoreID)).ToList();
                    var Rebates = _dbBaseContext.Rebates.Where(m => storeListIds.Any(c => c == m.StoreID)).ToList();

                    foreach (var item in storeListIds)
                    {
                        StoreDetailsViewModel modelObj = new StoreDetailsViewModel();
                        var storeRecord = store.Where(x => x.StoreID == item).FirstOrDefault();
                        var rebatesRecord=Rebates.Where(x => x.StoreID == item).FirstOrDefault();

                        modelObj.StoreID = storeRecord.StoreID;
                        modelObj.StoreName = storeRecord.StoreName;
                        modelObj.Address1 = storeRecord.Address1;
                        modelObj.City = storeRecord.City;
                        modelObj.State = storeRecord.State;
                        modelObj.Zip5 = storeRecord.Zip5;
                        modelObj.isStoreActive = false;
                        modelObj.UserProfileId = loyalty.UserProfileID;
                        modelObj.Tier = rebatesRecord?.Tier;
                        modelObj.ImageUrl = null;
                       
                        //double distance;
                        //if (store.Latitude != null && store.Longitude != null)
                        //    //{
                        //    //    distance = GetDistance(double.Parse(longitude), double.Parse(latitude), double.Parse(store.Longitude), double.Parse(store.Latitude));
                        //    //    if (distance <= 15)
                        //    //    {
                        //    //        modelList.Add(modelObj);
                        //    //    }
                        //    //}
                        //    //}
                        modelList.Add(modelObj);
                      //implimented in seperate function to fetch images....
                        //if (store != null)
                        //{
                        //    byte[]? ImageUrl = null;
                        //    var connections = _configuration.GetConnectionString("LoyaltyBaseDB");
                        //    var s = connections.Split(';');
                        //     //condition is only for internal use
                        //    //if (s[0] != "Server=DESKTOP-ERMLF75")
                        //    //{
                        //        var query = "select * from tblDataBaseServer inner join tblServerMappings on tblDataBaseServer.Server = tblServerMappings.ServerName where tblDataBaseServer.DataBaseID = (select tblStore.databaseid from tblStore inner join tblTobaccoRebateProgramType on tblstore.storeid = tblTobaccoRebateProgramType.storeid where tblstore.storeid = " + "'" + storeRecord.StoreID + "'" + ")";
                        //        using (SqlConnection conn = new SqlConnection(connections))
                        //        {
                        //            conn.Open();
                        //            SqlCommand cmd = new SqlCommand(query, conn);
                        //            ConnectionStringViewModel details = new ConnectionStringViewModel();
                        //            using (var result = cmd.ExecuteReader())
                        //            {
                        //                while (result.Read())
                        //                {
                        //                    //Console.WriteLine("{0}\t{1}", result.GetInt32(0),
                        //                    //    result.GetString(4));
                        //                    details.DataBaseServerID = result["DataBaseServerID"].ToString();
                        //                    details.DataBaseID = result["DataBaseID"].ToString();
                        //                    details.ConnectionString = result["ConnectionString"].ToString();
                        //                    details.FireWallIP = result["FireWallIP"].ToString();
                        //                }
                        //            }
                        //            var ConnectionString = "Data Source=" + details.FireWallIP + ";" + "Password=0mIcr0n;" + "Connect Timeout=60000;" + "User Id=ePBproduction;" + "Initial Catalog=NP";
                        //            var databaseid = details.DataBaseID;
                        //            stringconnect = ConnectionString + databaseid;
                        //            conn.Close();
                        //        }
                        //        if (stringconnect != null)
                        //        {
                        //            try
                        //            {
                        //            ////check if given ids folder is exist or not 
                        //            string path = Path.GetFullPath("Images");
                        //            string fullpath = String.Format(path + "\\Store{0}", item);
                        //            if (Directory.Exists(fullpath))
                        //            {
                        //                string name = String.Format("Store{0}.jpeg", item);
                        //                //string[] imagefile = Directory.GetFiles(fullpath,"*.jpeg");
                        //                ImageUrl = File.ReadAllBytes(fullpath + "//" + name);
                        //            }
                        //            else
                        //            {
                        //                var stringquery = "select logo from StoreInsites  Where StoreID =" + storeRecord.StoreID;
                        //                    using (SqlConnection con = new SqlConnection(stringconnect))
                        //                    {
                        //                        con.Open();
                        //                        SqlCommand command = new SqlCommand(stringquery, con);
                        //                        var dashboardlist = command.ExecuteReader();

                        //                        if (dashboardlist.HasRows)
                        //                        {
                        //                            while (dashboardlist.Read())
                        //                            {
                        //                            var item1 = dashboardlist["logo"];
                        //                            ImageUrl = (byte[])item1;
                        //                            }
                        //                        }
                        //                        if (ImageUrl != null)
                        //                        {
                        //                        //Image storeImage=byteArrayToImage(ImageUrl);
                        //                        // string extensionOfImage = new ImageFormatConverter().ConvertToString(storeImage.RawFormat);
                        //                        CreateFolder(storeRecord.StoreID, ImageUrl);
                        //                        }
                        //                    con.Close();
                        //                    }
                        //                }
                        //            }
                        //            catch (Exception ex)
                        //            {

                        //            }
                        //        }
                        //   //}
                            
                        //    //modelObj.ImageUrl = ImageUrl;
                           
                        //    //if (store.Latitude != null && store.Longitude != null)
                        //    //{
                        //    //    distance = GetDistance(double.Parse(longitude), double.Parse(latitude), double.Parse(store.Longitude), double.Parse(store.Latitude));
                        //    //    if (distance <= 15)
                        //    //    {
                        //    //        modelList.Add(modelObj);
                        //    //    }
                        //    //}
                        //}
                    }
                }
            }
            return modelList;
        }
       
        public double GetDistance(double longitude, double latitude, double otherLongitude, double otherLatitude)
        {
            var d1 = latitude * (Math.PI / 180.0);
            var num1 = longitude * (Math.PI / 180.0);
            var d2 = otherLatitude * (Math.PI / 180.0);
            var num2 = otherLongitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
            //retrun values in miles 
            return (6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)))) * 0.000621371192;
        }
        
    }
}
