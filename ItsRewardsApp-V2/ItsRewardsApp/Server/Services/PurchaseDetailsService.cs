using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.Models;
using ItsRewardsApp.Shared.ViewModels;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace ItsRewardsApp.Server.Services
{
    public class PurchaseDetailsService : IpurchaseDetails
    {
        private readonly LoyaltyUserDBContext _dbContext = new();
        private readonly LoyaltyBaseDBContext _dbBaseContext = new();
        private readonly IConfiguration _configuration;
        public PurchaseDetailsService(LoyaltyUserDBContext dbContext, LoyaltyBaseDBContext dbBaseContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _dbBaseContext = dbBaseContext;
            _configuration = configuration;
        }
        public List<PreOrder> PurchaseDetailsList(string cellNumber, string storeId)
        {
            List<PreOrder> preOrders=new List<PreOrder>();
            try
            {
                var connections = _configuration.GetConnectionString("LoyaltyBaseDB");
                var stringconnect = ServerConnection(storeId);
                var getcustomerid = _dbContext.LoyaltyUserProfile.Where(x => x.CellPhone == cellNumber).Select(a => a.UserProfileID).FirstOrDefault();
                if (stringconnect != null)
                {
                    try
                    {
                        var list = "select * from ePreOrders where customerid = '" + getcustomerid + "'";
                        using (SqlConnection con = new SqlConnection(stringconnect))
                        {
                            con.Open();
                            SqlCommand command = new SqlCommand(list, con);
                            var dashboardlist = command.ExecuteReader();
                            if (dashboardlist.HasRows)
                            {
                                while (dashboardlist.Read())
                                {
                                    PreOrder preOrder = new PreOrder();
                                    preOrder.Id = (decimal)dashboardlist["Id"];
                                    preOrder.Status = dashboardlist["Status"].ToString();
                                    preOrder.CreatedDate = (DateTime)dashboardlist["CreatedDate"];
                                    preOrder.Tax = (decimal)dashboardlist["Tax"];
                                    preOrder.Total = (decimal)dashboardlist["Total"];
                                    preOrder.NeedAgeValidation = dashboardlist["NeedAgeValidation"].ToString();
                                    preOrders.Add(preOrder);
                                }
                            }
                            con.Close();
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return preOrders;
            }
            catch (Exception)
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
