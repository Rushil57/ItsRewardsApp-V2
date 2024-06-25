using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ItsRewardsApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICoupons _coupons;
        public CouponsController(ICoupons coupons, IConfiguration configuration)
        {
            _coupons = coupons;
            _configuration = configuration;
        }

        [HttpGet("{cellNumber},{storeId}")]
        public List<CouponsViewModel> Get(string cellNumber, string storeId)
        {
            var stringconnect = "";
            //List<string> coupons = new List<string>();
            List<CouponsViewModel> couponsList = new List<CouponsViewModel>();
            var connections = _configuration.GetConnectionString("LoyaltyBaseDB");
            var query = "select * from tblDataBaseServer inner join tblServerMappings on tblDataBaseServer.Server = tblServerMappings.ServerName where tblDataBaseServer.DataBaseID = (select tblStore.databaseid from tblStore inner join tblTobaccoRebateProgramType on tblstore.storeid = tblTobaccoRebateProgramType.storeid where tblstore.storeid = " + "'" + storeId + "'" + ")";
            var stringquery = "Select PromotionId,name From promotion Where Enabled = 1 and EndDate >= getdate() And pricebookid = (select pricebookid from tblpricebookstore Where storeid = " + storeId + ") And ManufacturersPromotion is not null";
            using (SqlConnection conn = new SqlConnection(connections))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                //var result = cmd.ExecuteReader();
                ConnectionStringViewModel details = new ConnectionStringViewModel();

                using (var result = cmd.ExecuteReader())
                {

                    while (result.Read())
                    {
                        //Console.WriteLine("{0}\t{1}", result.GetInt32(0),
                        //result.GetString(4));
                        details.DataBaseServerID = result["DataBaseServerID"].ToString();
                        details.DataBaseID = result["DataBaseID"].ToString();
                        details.ConnectionString = result["ConnectionString"].ToString();
                        details.FireWallIP = result["FireWallIP"].ToString();
                    }
                }
                   // ConnectionStringViewModel details = new ConnectionStringViewModel();
                //if (result.HasRows)
                //{
                //    var resultRead = result.Read();
                //    //while (result.Read())
                //    //{
                //    //    //Console.WriteLine("{0}\t{1}", result.GetInt32(0),
                //    //    //    result.GetString(4));
                //    //    details.DataBaseServerID = result["DataBaseServerID"].ToString();
                //    //    details.DataBaseID = result["DataBaseID"].ToString();
                //    //    details.ConnectionString = result["ConnectionString"].ToString();
                //    //    details.FireWallIP = result["FireWallIP"].ToString();
                //    //}
                //}
                var ConnectionString = "Data Source=" + details.FireWallIP + ";" + "Password=0mIcr0n;" + "Connect Timeout=60000;" + "User Id=ePBproduction;" + "Initial Catalog=NP";
                var databaseid = details.DataBaseID;
                stringconnect = ConnectionString + databaseid;
                conn.Close();
            }

            if(stringconnect != null)
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
                                CouponsViewModel couponsData = new CouponsViewModel();
                                couponsData.PromotionId = (int)dashboardlist["PromotionId"];
                                couponsData.CouponName = dashboardlist["name"].ToString();
                                // var id = dashboardlist["PromotionId"];
                                // var item = dashboardlist["name"].ToString();
                                // coupons.Add(item);
                                couponsData.StoreId = storeId;
                                couponsList.Add(couponsData);
                            }
                        }
                        con.Close();
                    }
                }
                catch (Exception ex)
                {

                }
            }

            List<CouponsViewModel> user = _coupons.GetCouPons(couponsList);
            return user;

        }
    }
}
