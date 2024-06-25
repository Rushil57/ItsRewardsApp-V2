using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.ViewModels;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net.Http;
using System.Collections.Generic;

namespace ItsRewardsApp.Server.Services
{
    public class EcomCouponsService : IEcomCoupons
    {
        private readonly LoyaltyBaseDBContext _dbContext = new();
        private readonly LoyaltyUserDBContext _dbUserContext = new();
        private readonly IConfiguration _configuration;
        public EcomCouponsService(LoyaltyBaseDBContext dbContext, LoyaltyUserDBContext dbUserContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _dbUserContext = dbUserContext;
            _configuration = configuration;
        }
        public List<string> GetDepartmentsName(String StoreId)
        {
            List<string> listOfDepartmrnt=new List<string>();
            var stringconnect = "";
            var connections = _configuration.GetConnectionString("LoyaltyBaseDB");
            var query = "select * from tblDataBaseServer inner join tblServerMappings on tblDataBaseServer.Server = tblServerMappings.ServerName where tblDataBaseServer.DataBaseID = (select tblStore.databaseid from tblStore inner join tblTobaccoRebateProgramType on tblstore.storeid = tblTobaccoRebateProgramType.storeid where tblstore.storeid = " + "'" + StoreId + "'" + ")";
            var stringquery = "Select department From tbldepartments Where pricebookid = (select pricebookid from tblpricebookstore Where StoreID ='" + StoreId + "')";
            try
            {
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
                            var departmentData = command.ExecuteReader();
                            if (departmentData.HasRows)
                            {
                                while (departmentData.Read())
                                {
                                    listOfDepartmrnt.Add(departmentData["department"].ToString());
                                }
                            }
                            con.Close();
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }

                return listOfDepartmrnt;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
