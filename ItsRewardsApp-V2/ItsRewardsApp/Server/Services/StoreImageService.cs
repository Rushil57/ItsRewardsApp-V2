using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.ViewModels;
using Microsoft.Data.SqlClient;

namespace ItsRewardsApp.Server.Services
{
    public class StoreImageService: IStoreImageService
    {
        private readonly LoyaltyUserDBContext _dbContext = new();
        private readonly LoyaltyBaseDBContext _dbBaseContext = new();
        private readonly IConfiguration _configuration;
        public StoreImageService(LoyaltyUserDBContext dbContext, LoyaltyBaseDBContext dbBaseContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _dbBaseContext = dbBaseContext;
            _configuration = configuration;
        }
        public async Task<List<StoreDetailsViewModel>> GetStoreImage(string storeIds)
        {
            byte[]? ImageUrl = null;
            var stringconnect = "";
            List<StoreDetailsViewModel> modelList = new List<StoreDetailsViewModel>();
            var connections = _configuration.GetConnectionString("LoyaltyBaseDB");
            var s = connections.Split(';');
            //condition is only for internal use
            //if (s[0] != "Server=DESKTOP-ERMLF75")
            //{
            var id = storeIds.Split(',').ToList();
            var ids = id.Select(int.Parse).ToList();
            foreach (var item in ids)
            {
                StoreDetailsViewModel modelObj = new StoreDetailsViewModel();
                var query = "select * from tblDataBaseServer inner join tblServerMappings on tblDataBaseServer.Server = tblServerMappings.ServerName where tblDataBaseServer.DataBaseID = (select tblStore.databaseid from tblStore inner join tblTobaccoRebateProgramType on tblstore.storeid = tblTobaccoRebateProgramType.storeid where tblstore.storeid = " + "'" + item + "'" + ")";
                using (SqlConnection conn = new SqlConnection(connections))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    ConnectionStringViewModel details = new ConnectionStringViewModel();
                    using (var result = cmd.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            //Console.WriteLine("{0}\t{1}", result.GetInt32(0),
                            //    result.GetString(4));
                            details.DataBaseServerID = result["DataBaseServerID"].ToString();
                            details.DataBaseID = result["DataBaseID"].ToString();
                            details.ConnectionString = result["ConnectionString"].ToString();
                            details.FireWallIP = result["FireWallIP"].ToString();
                        }
                    }
                    var ConnectionString = "Data Source=" + details.FireWallIP + ";" + "Password=0mIcr0n;" + "Connect Timeout=60000;" + "User Id=ePBproduction;" + "Initial Catalog=NP";
                    var databaseid = details.DataBaseID;
                    stringconnect = ConnectionString + databaseid;
                    conn.Close();
                }
                if (stringconnect != null)
                {
                    try
                    {
                        ////check if given ids folder is exist or not 
                        string path = Path.GetFullPath("Images");
                        string fullpath = String.Format(path + "\\Store{0}", item);
                        if (Directory.Exists(fullpath))
                        {
                            string name = String.Format("Store{0}.jpeg", item);
                            //string[] imagefile = Directory.GetFiles(fullpath,"*.jpeg");
                            ImageUrl = File.ReadAllBytes(fullpath + "//" + name);
                        }
                        else
                        {
                            var stringquery = "select logo from StoreInsites  Where StoreID =" + item;
                            using (SqlConnection con = new SqlConnection(stringconnect))
                            {
                                con.Open();
                                SqlCommand command = new SqlCommand(stringquery, con);
                                var dashboardlist = command.ExecuteReader();

                                if (dashboardlist.HasRows)
                                {
                                    while (dashboardlist.Read())
                                    {
                                        var item1 = dashboardlist["logo"];
                                        ImageUrl = (byte[])item1;
                                    }
                                }
                                else
                                {
                                    ImageUrl = null;
                                }
                                if (ImageUrl != null)
                                {
                                    //Image storeImage=byteArrayToImage(ImageUrl);
                                    // string extensionOfImage = new ImageFormatConverter().ConvertToString(storeImage.RawFormat);
                                    CreateFolder(item, ImageUrl);
                                }
                                con.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    modelObj.StoreID = item;
                    modelObj.ImageUrl = ImageUrl;
                }
                modelList.Add(modelObj);
            }

            return modelList;
        }
        public void CreateFolder(int StoreId, byte[] image)
        {
            if (image != null)
            {
                String path = Path.GetFullPath("Images");
                string fullpath = String.Format(path + "\\Store{0}", StoreId);
                string name = String.Format("Store{0}.jpeg", StoreId);
                if (!Directory.Exists(fullpath))
                {
                    Directory.CreateDirectory(fullpath);
                    File.WriteAllBytes(fullpath + "//" + name, image);
                }
                else
                {
                    //File.WriteAllBytes(fullpath + "//" + name, image);
                }
            }
        }
    }
}
