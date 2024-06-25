using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.Models;
using ItsRewardsApp.Shared.ViewModels;
using Microsoft.Data.SqlClient;
using System.Data;
using ZXing;

namespace ItsRewardsApp.Server.Services
{
    public class SearchResultService : ISearchResult
    {
       
        private readonly LoyaltyUserDBContext _dbContext = new();
        private readonly LoyaltyBaseDBContext _dbBaseContext = new();
        private readonly IConfiguration _configuration;
        public SearchResultService(LoyaltyUserDBContext dbContext, LoyaltyBaseDBContext dbBaseContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _dbBaseContext = dbBaseContext;
            _configuration = configuration;
        }
        public List<SearchResultViewModel> GetSearchResultForPromotionId(string promotionId,string storeId)
        {
            try
            {
                List<SearchResultViewModel> searchResult = new List<SearchResultViewModel>();
                var stringconnect = "";
                var connections = _configuration.GetConnectionString("LoyaltyBaseDB");
                var query = "select * from tblDataBaseServer inner join tblServerMappings on tblDataBaseServer.Server = tblServerMappings.ServerName where tblDataBaseServer.DataBaseID = (select tblStore.databaseid from tblStore inner join tblTobaccoRebateProgramType on tblstore.storeid = tblTobaccoRebateProgramType.storeid where tblstore.storeid = " + "'" + storeId + "'" + ")";
                var stringquery = "Select groupproduct.ProductId,price, tblSKuMaster.description,department,[group].name,SKU, 'imagenotfound.jpg' as  image from mix inner join groupproduct on mix.groupid = groupproduct.groupid inner join [group] on groupproduct.groupid = [group].groupid inner join tblpricebookskumaster on groupproduct.productid = tblpricebookskumaster.pricebookskumasterid inner join tblskumaster on tblpricebookskumaster.skumasterid = tblskumaster.skumasterid inner join tbldepartments on tblskumaster.departmentid=tbldepartments.departmentid inner join tblsku on tblSKuMaster.SKUID = tblSKuMaster.SKUID where mix.PromotionId = '" + promotionId + "'";
                using (SqlConnection conn = new SqlConnection(connections))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    ConnectionStringViewModel details = new ConnectionStringViewModel();

                    using (var result = cmd.ExecuteReader())
                    {
                        while (result.Read())
                        {
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
                        using (SqlConnection con = new SqlConnection(stringconnect))
                        {
                            con.Open();
                            SqlCommand command = new SqlCommand(stringquery, con);
                            var dashboardlist = command.ExecuteReader();

                            if (dashboardlist.HasRows)
                            {
                                while (dashboardlist.Read())
                                {
                                    SearchResultViewModel searchdata = new SearchResultViewModel();
                                    searchdata.ProductId = (int)dashboardlist["ProductId"];
                                    searchdata.Price = (decimal)dashboardlist["Price"];
                                    searchdata.Description= dashboardlist["Description"].ToString();
                                    searchdata.Department = dashboardlist["Department"].ToString();
                                    searchdata.Group = dashboardlist["Name"].ToString();
                                    searchdata.SKU = dashboardlist["SKU"].ToString();
                                    searchdata.Image = dashboardlist["Image"].ToString();
                                    searchResult.Add(searchdata);
                                }
                            }
                            con.Close();
                        }
                        //foreach (var item in searchResult)
                        //{
                        //    string path = Path.GetFullPath("Images");
                        //    string fullpath = String.Format(path + "\\SearchResultImages" + "\\{0}", item.SKU);
                        //    if (Directory.Exists(fullpath))
                        //    {
                        //        string[] files = Directory.GetFiles(fullpath);
                        //        var filename = Path.GetFileName(files[0]);
                        //        item.ImageSrc = File.ReadAllBytes(fullpath + "//" + filename);
                        //       // item.Image = fullpath + "\\" + filename;
                        //    }
                        //}

                        //1st senario check if image avialabel in folder
                        string path = Path.GetFullPath("Images");
                        string fullpath = String.Format(path + "\\ItemNumberImages");
                        string[] files = Directory.GetFiles(fullpath);
                        foreach (string file in files)
                        {
                            var filename = Path.GetFileName(file);
                            var split = filename.Split(".");
                            var imagedata = searchResult.Where(a => a.SKU == split[0]).FirstOrDefault();
                            if (imagedata != null)
                            {
                                //var filename = Path.GetFileName(files[0]);
                                imagedata.ImageSrc = File.ReadAllBytes(fullpath + "//" + filename);
                                imagedata.Image = fullpath + "\\" + filename;
                            }
                        }
                        //2nd senario if image avialable in imagebrand databse
                        string BrandImagePath = String.Format(path + "\\BrandImages");
                        foreach (var item in searchResult)
                        {
                            if (item.ImageSrc == null)
                            {
                                var imageBrandId = _dbContext.ImageBrandLink.Where(x => x.ItemNumber == item.SKU).Select(x => x.ImageBrandId).FirstOrDefault();
                                var imageName = _dbContext.ImageBrand.Where(x => x.Id == imageBrandId).Select(x => x.ImageName).FirstOrDefault();
                                // item.Image = imageName;
                                item.ImageSrc = File.ReadAllBytes(BrandImagePath + "//" + imageName);
                                item.Image = BrandImagePath + "\\" + imageName;
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return searchResult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public PreOrderDetail AddPreOrderDetail(PreOrderDetail preOrderDetail)
        {
            try
            {
                var stringconnect = "";
                var connections = _configuration.GetConnectionString("LoyaltyBaseDB");
                var query = "select * from tblDataBaseServer inner join tblServerMappings on tblDataBaseServer.Server = tblServerMappings.ServerName where tblDataBaseServer.DataBaseID = (select tblStore.databaseid from tblStore inner join tblTobaccoRebateProgramType on tblstore.storeid = tblTobaccoRebateProgramType.storeid where tblstore.storeid = " + "'" + preOrderDetail.StoreId + "'" + ")";
                var getcustomerid = _dbContext.LoyaltyUserProfile.Where(x => x.CellPhone == preOrderDetail.CellPhone).Select(a=>a.UserProfileID).FirstOrDefault();
                    using (SqlConnection conn = new SqlConnection(connections))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);
                        ConnectionStringViewModel details = new ConnectionStringViewModel();
                        using (var result = cmd.ExecuteReader())
                        {
                            while (result.Read())
                            {
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
                        var stringquery = "select id from ePreOrders where customerid='" + getcustomerid + "'"; //check if preorder entry is exixts for customer
                        using (SqlConnection con = new SqlConnection(stringconnect))
                        {
                            con.Open();
                            SqlCommand command = new SqlCommand(stringquery, con);
                            var dashboardlist = command.ExecuteReader();
                            if (dashboardlist.HasRows)
                            {
                                while (dashboardlist.Read())
                                {
                                    preOrderDetail.OrderId = (decimal)dashboardlist["id"];
                                }
                            }
                            con.Close();
                        }
                        if (preOrderDetail.OrderId == 0)
                        {
                            using (SqlConnection con = new SqlConnection(stringconnect))
                            {
                                con.Open();
                                string sql = "INSERT INTO ePreOrders(UserId,CreatedDate,PickupTime,Total,PaidAmount,Status,PaidStatus,NeedAgeValidation,Comments,ItemCount,VendorId,customerid,tax,discounts) VALUES(@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9,@param10,@param11,@param12,@param13,@param14)";
                                using (SqlCommand cmd = new SqlCommand(sql, con)) //store data in epreorder table
                                {
                                    cmd.Parameters.Add("@param1", SqlDbType.VarChar, 50).Value = "Admin2600";
                                    cmd.Parameters.Add("@param2", SqlDbType.DateTime).Value = DateTime.Now;
                                    cmd.Parameters.Add("@param3", SqlDbType.DateTime).Value = DateTime.Now;
                                    cmd.Parameters.Add("@param4", SqlDbType.Decimal).Value = 0.00;
                                    cmd.Parameters.Add("@param5", SqlDbType.Decimal).Value = 0.00;
                                    cmd.Parameters.Add("@param6", SqlDbType.VarChar, 50).Value = "NewOrder";
                                    cmd.Parameters.Add("@param7", SqlDbType.VarChar, 50).Value = "";
                                    cmd.Parameters.Add("@param8", SqlDbType.Char, 1).Value = 'N';
                                    cmd.Parameters.Add("@param9", SqlDbType.NVarChar).Value = "test";
                                    cmd.Parameters.Add("@param10", SqlDbType.Int).Value = 0;
                                    cmd.Parameters.Add("@param11", SqlDbType.Decimal).Value = getcustomerid;
                                    cmd.Parameters.Add("@param12", SqlDbType.Decimal).Value = getcustomerid;
                                    cmd.Parameters.Add("@param13", SqlDbType.Decimal).Value = 0.00;
                                    cmd.Parameters.Add("@param14", SqlDbType.Decimal).Value = 0.00;
                                    cmd.CommandType = CommandType.Text;
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                            var stringqueryfororderid = "select id from ePreOrders where customerid='" + getcustomerid + "'"; //check if preorder entry is exixts for customer
                            using (SqlConnection con = new SqlConnection(stringconnect))
                            {
                                con.Open();
                                SqlCommand command = new SqlCommand(stringqueryfororderid, con);
                                var dashboardlist = command.ExecuteReader();

                                if (dashboardlist.HasRows)
                                {
                                    while (dashboardlist.Read())
                                    {
                                        preOrderDetail.OrderId = (decimal)dashboardlist["id"];
                                    }
                                }
                                con.Close();
                            }
                        }
                        if(preOrderDetail.OrderId!=0)//for storing data in preorderdetail
                        {
                            using (SqlConnection con = new SqlConnection(stringconnect))
                            {
                                con.Open();
                                string sql = "INSERT INTO ePreOrderDetails(OrderId,PriceBookSKUMasterId,Name,Quantity,UnitPrice,TotalPrice,DiscountAmount,LoyalityPrice,BasePrice) VALUES(@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8,@param9)";
                                using (SqlCommand cmd = new SqlCommand(sql, con)) //store data in epreorderDetail table
                                {
                                    cmd.Parameters.Add("@param1", SqlDbType.Decimal).Value =preOrderDetail.OrderId;
                                    cmd.Parameters.Add("@param2", SqlDbType.Decimal).Value =preOrderDetail.PriceBookSKUMasterId ;
                                    cmd.Parameters.Add("@param3", SqlDbType.VarChar,200).Value = preOrderDetail.Name;
                                    cmd.Parameters.Add("@param4", SqlDbType.Int).Value = preOrderDetail.Quantity;
                                    cmd.Parameters.Add("@param5", SqlDbType.Decimal).Value = preOrderDetail.UnitPrice;
                                    cmd.Parameters.Add("@param6", SqlDbType.Decimal).Value = preOrderDetail.TotalPrice;  //Quantity * Unitprice
                                    cmd.Parameters.Add("@param7", SqlDbType.Decimal).Value = 0.00;
                                    cmd.Parameters.Add("@param8", SqlDbType.Decimal).Value = 0.00;
                                    cmd.Parameters.Add("@param9", SqlDbType.Decimal).Value = preOrderDetail.UnitPrice;
                                    cmd.CommandType = CommandType.Text;
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                            using (SqlConnection con = new SqlConnection(stringconnect))
                            {
                                con.Open();
                                string sql = "Update ePreOrders set Total+=@price,ItemCount+=@quantity where Id=@orderId";
                                using (SqlCommand cmd = new SqlCommand(sql, con))    //store data in epreorderDetail table
                                {
                                    cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = preOrderDetail.TotalPrice;
                                    cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = preOrderDetail.Quantity;
                                    cmd.Parameters.Add("@orderId", SqlDbType.Decimal).Value = preOrderDetail.OrderId;
                                    cmd.CommandType = CommandType.Text;
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    }
            }
            catch (Exception ex)
            {
            }
            return preOrderDetail;
        }

        public List<SearchResultViewModel> GetImagesForSearchResult(string searchResults)
        {
            throw new NotImplementedException();
        }
    }
}
