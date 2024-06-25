using ItsRewardsApp.Client.Pages;
using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.ViewModels;
using Microsoft.Data.SqlClient;

namespace ItsRewardsApp.Server.Services
{
    public class EcomSearchResultService : IEcomSearchResult
    {
        private readonly LoyaltyUserDBContext _dbContext = new();
        private readonly LoyaltyBaseDBContext _dbBaseContext = new();
        private readonly IConfiguration _configuration;
        public EcomSearchResultService(LoyaltyUserDBContext dbContext, LoyaltyBaseDBContext dbBaseContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _dbBaseContext = dbBaseContext;
            _configuration = configuration;
        }

        public List<string> GetDescritionList(string storeId, string description)
        {
            List<string> searchResult = new List<string>();
            try
            {
                var stringconnect = ServerConnection(storeId);
                var stringquery = "Select  Description from tblpricebookskumaster inner join tblskumaster on tblpricebookskumaster.skumasterid = tblskumaster.skumasterid inner join tblDepartments on tblskumaster.departmentid = tblDepartments.departmentid and pricebookid = (select PriceBookID from tblPriceBookStore where storeid =" + storeId + ") and Description like '" + description + "%'";
                if (stringconnect != null)
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
                                searchResult.Add(dashboardlist["Description"].ToString());
                            }
                        }
                        con.Close();
                    }

                }
                return searchResult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<SearchResultViewModel> GetSearchResultByDescription(string storeId, string department, string description)
        {
            List<SearchResultViewModel> searchResult = new List<SearchResultViewModel>();
            var stringquery = "";
            try
            {
                var stringconnect = ServerConnection(storeId);
                if (department == "0")
                {
                    // stringquery = "Select tblpricebookskumaster.PriceBookSKUMasterID as ProductId,price, tblskumaster.description , department, null as name,SKU, 'imagenotfound.jpg' as image from tblpricebookskumaster inner join tblskumaster on tblpricebookskumaster.skumasterid = tblskumaster.skumasterid inner join tblDepartments on tblskumaster.departmentid = tblDepartments.departmentid inner join tblsku on tblSKuMaster.SKUID = tblSKuMaster.SKUID where tblskumaster.description like '%" + description + "%'";
                    stringquery = "select tblpricebookskumaster.PriceBookSKUMasterID as ProductId, Price, tblSKuMaster.Description,department, null as name, SKU,'imagenotfound.jpg' as Image from tblPriceBookSKUMaster inner join tblskumaster on tblPriceBookSKUMaster.SKUMasterID = tblSKuMaster.SKUMasterID inner join tblDepartments on tblSKuMaster.DepartmentID = tblDepartments.DepartmentID inner join tblSKU on tblSKuMaster.SKUID = tblsku.SKUID inner join tblPriceBookStore on tbldepartments.PriceBookID = tblPriceBookStore.PriceBookID where tblSKuMaster.Description like '%" + description + "%' and storeid =  " + storeId;
                }
                else
                {
                    //stringquery = "Select tblpricebookskumaster.PriceBookSKUMasterID as ProductId,price, tblskumaster.description , department, null as name,SKU, 'imagenotfound.jpg' as image from tblpricebookskumaster inner join tblskumaster on tblpricebookskumaster.skumasterid = tblskumaster.skumasterid inner join tblDepartments on tblskumaster.departmentid = tblDepartments.departmentid inner join tblsku on tblSKuMaster.SKUID = tblSKuMaster.SKUID where tblskumaster.description like '%" + description + "%' and Department like '%" + department + "%'";
                    stringquery = "select tblpricebookskumaster.PriceBookSKUMasterID as ProductId, Price, tblSKuMaster.Description,department, null as name, SKU,'imagenotfound.jpg' as Image from tblPriceBookSKUMaster inner join tblskumaster on tblPriceBookSKUMaster.SKUMasterID = tblSKuMaster.SKUMasterID inner join tblDepartments on tblSKuMaster.DepartmentID = tblDepartments.DepartmentID inner join tblSKU on tblSKuMaster.SKUID = tblsku.SKUID inner join tblPriceBookStore on tbldepartments.PriceBookID = tblPriceBookStore.PriceBookID where tblSKuMaster.Description like '%" + description + "%' and storeid =  " + storeId + "and Department like '%" + department + "%'";
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
                                    searchdata.Description = dashboardlist["Description"].ToString();
                                    searchdata.Department = dashboardlist["Department"].ToString();
                                    searchdata.Group = dashboardlist["Name"].ToString();
                                    searchdata.SKU = dashboardlist["SKU"].ToString();
                                    searchdata.Image = dashboardlist["Image"].ToString();
                                    searchResult.Add(searchdata);
                                }
                            }
                            con.Close();
                        }

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

                        //2nd senario if image avialable in imagebrand table
                        string BrandImagePath = String.Format(path + "\\BrandImages");
                        foreach (var item in searchResult)
                        {
                            if (item.ImageSrc == null)
                            {
                                var imageBrandId = _dbContext.ImageBrandLink.Where(x => x.ItemNumber == item.SKU).Select(x => x.ImageBrandId).FirstOrDefault();
                                var imageName = _dbContext.ImageBrand.Where(x => x.Id == imageBrandId).Select(x => x.ImageName).FirstOrDefault();
                                //item.Image = imageName;
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
        public string ServerConnection(string storeId)
        {
            try
            {
                var stringconnect = "";
                var connections = _configuration.GetConnectionString("LoyaltyBaseDB");
                var query = "select * from tblDataBaseServer inner join tblServerMappings on tblDataBaseServer.Server = tblServerMappings.ServerName where tblDataBaseServer.DataBaseID = (select tblStore.databaseid from tblStore inner join tblTobaccoRebateProgramType on tblstore.storeid = tblTobaccoRebateProgramType.storeid where tblstore.storeid = " + "'" + storeId + "'" + ")";
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
                return stringconnect;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

