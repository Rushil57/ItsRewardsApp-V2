using ItsRewardsApp.Client.Pages;
using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Shared.Models;
using ItsRewardsApp.Shared.ViewModels;
using Microsoft.Data.SqlClient;
using Plivo.XML;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Input;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ItsRewardsApp.Server.Services
{
    public class StatusService:IStatus
    {
        private readonly LoyaltyUserDBContext _dbContext = new();
        private readonly LoyaltyBaseDBContext _dbBaseContext = new();
        private readonly IConfiguration _configuration;
        public StatusService(LoyaltyUserDBContext dbContext, LoyaltyBaseDBContext dbBaseContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _dbBaseContext = dbBaseContext;
            _configuration = configuration;
        }
        public List<StatusViewModel> GetCartDeatils(string Cellphone,string storeId)
        {
            List<StatusViewModel> statusViewDetails = new List<StatusViewModel>();
            int OrderID = 0;
            try
            {
                var connections = _configuration.GetConnectionString("LoyaltyBaseDB");
                var stringconnect = ServerConnection(storeId);
                var getcustomerid = _dbContext.LoyaltyUserProfile.Where(x => x.CellPhone == Cellphone).Select(a => a.UserProfileID).FirstOrDefault();
                var query = "select Id from ePreOrders where customerid = '" + getcustomerid + "'";
                if (stringconnect != null)
                {
                    try
                    {
                        using (SqlConnection con = new SqlConnection(stringconnect))
                        {
                            con.Open();
                            SqlCommand command = new SqlCommand(query, con);
                            var dashboardlist = command.ExecuteReader();
                            if (dashboardlist.HasRows)
                            {
                                while (dashboardlist.Read())
                                {
                                    OrderID = (int)(decimal)dashboardlist["Id"];
                                }
                            }
                            con.Close();
                        }
                        if (OrderID > 0)
                        {
                            var list = "select ePreOrderDetails.Id,OrderId,PriceBookSKUMasterId,Name,ePreOrderDetails.Quantity,UnitPrice,TotalPrice,total,tax,discounts from ePreOrderDetails inner join ePreOrders on ePreOrderDetails.OrderId=ePreOrders.Id where OrderId='" + OrderID + "'";
                            using (SqlConnection con = new SqlConnection(stringconnect))
                            {
                                con.Open();
                                SqlCommand command = new SqlCommand(list, con);
                                var dashboardlist = command.ExecuteReader();
                                if (dashboardlist.HasRows)
                                {
                                    while (dashboardlist.Read())
                                    {
                                        StatusViewModel statusView = new StatusViewModel();
                                        statusView.Id = (decimal)dashboardlist["Id"];
                                        statusView.OrderId = (decimal)dashboardlist["OrderId"];
                                        statusView.PriceBookSKUMasterId = (decimal)dashboardlist["PriceBookSKUMasterId"];
                                        statusView.Name = dashboardlist["Name"].ToString();
                                        statusView.Quantity = (int)dashboardlist["Quantity"];
                                        statusView.UnitPrice = (decimal)dashboardlist["UnitPrice"];
                                        statusView.TotalPrice = (decimal)dashboardlist["TotalPrice"];
                                        statusView.Tax = (decimal)dashboardlist["Tax"];
                                        statusView.Discounts = (decimal)dashboardlist["Discounts"];
                                        statusView.Total = (decimal)dashboardlist["Total"];
                                        statusViewDetails.Add(statusView);
                                    }
                                }
                                con.Close();
                            }
                        }
                        var getCustomerInfo = _dbContext.LoyaltyUserProfile.Where(x => x.CellPhone == Cellphone).FirstOrDefault();
                        if (getCustomerInfo != null)
                        {
                            statusViewDetails[0].Address1 = getCustomerInfo.Address1;
                            statusViewDetails[0].City = getCustomerInfo.City;
                            statusViewDetails[0].State = getCustomerInfo.State;
                            statusViewDetails[0].ZipCode = getCustomerInfo.ZipCode;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                return statusViewDetails;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public string AddPaymentToken(StatusViewModel statusViewModel) //to add paymentToken in ePreOrder table
        {
            var response = "";
            try
            {
                var connections = _configuration.GetConnectionString("LoyaltyBaseDB");
                var stringconnect = ServerConnection(statusViewModel.storeId);
                var getcustomerid = _dbContext.LoyaltyUserProfile.Where(x => x.CellPhone == statusViewModel.cellnumber).Select(a => a.UserProfileID).FirstOrDefault();
                var query = "select Id from ePreOrders where customerid = '" + getcustomerid + "'";
                if (stringconnect != null)
                {
                    try
                    {
                        using (SqlConnection con = new SqlConnection(stringconnect))
                        {
                            con.Open();
                            SqlCommand command = new SqlCommand(query, con);
                            var dashboardlist = command.ExecuteReader();
                            if (dashboardlist.HasRows)
                            {
                                while (dashboardlist.Read())
                                {
                                    statusViewModel.OrderId = (int)(decimal)dashboardlist["Id"];
                                }
                            }
                            con.Close();
                        }
                        if (statusViewModel.OrderId > 0)
                        {
                            using (SqlConnection con = new SqlConnection(stringconnect))
                            {
                                con.Open();
                                string sql = "Update ePreOrders set PaymentToken=@payToken where Id=@orderId";
                                using (SqlCommand cmd = new SqlCommand(sql, con))    //store data in epreorderDetail table
                                {
                                    cmd.Parameters.Add("@payToken", SqlDbType.VarChar).Value = statusViewModel.PaymentToken;
                                    cmd.Parameters.Add("@orderId", SqlDbType.Decimal).Value = statusViewModel.OrderId;
                                    cmd.CommandType = CommandType.Text;
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }

                        }
                        response = "record updated succesfully";
                    }
                    catch (Exception ex)
                    {
                        response = "some error accured";
                    }
                }
            }
            catch (Exception)
            {

                response = "some error accured"; 
            }
            return response;
        }

        public List<StatusViewModel> RemoveCartQuatity(decimal id, int Quantity,decimal unitprice, int storeId, decimal OrderId)
        {
            var stringconnect = ServerConnection(storeId.ToString());
            if (stringconnect != null)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(stringconnect))
                    {
                        con.Open();
                        SqlCommand command = con.CreateCommand();
                        var UpdatedTotalPrice = 0 * unitprice;
                        command.CommandText = "update ePreOrderDetails set Quantity =" + 0 + ",TotalPrice="+ UpdatedTotalPrice + " where Id = " + id;
                        command.ExecuteNonQuery();

                        command.CommandText = "SELECT SUM(TotalPrice) AS TotalPrice FROM ePreOrderDetails where OrderId =" + OrderId;
                        var TotalPrice = command.ExecuteScalar();
                        if (TotalPrice != null)
                        {
                            SqlCommand commands = con.CreateCommand();
                            commands.CommandText = "Update ePreOrders set Total = " + TotalPrice + " where Id = " + OrderId;
                            commands.ExecuteNonQuery();
                        }
                        con.Close();
                       
                    }
                }
                catch (Exception ex)
                {
                }
            }
            throw new NotImplementedException();
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

        public void UpdateCartData(StatusViewModel statusView)
        {
            var UpdatedTotalPrice = statusView.Quantity * statusView.UnitPrice;
            statusView.Storage["UnitPrice"] = statusView.UnitPrice;
            statusView.Storage["Quantity"] = statusView.Quantity;
            statusView.Storage["OrderId"] = statusView.OrderId;
            statusView.Storage["Id"] = statusView.Id;
            statusView.Storage["TotalPrice"] = UpdatedTotalPrice;
        

            //try
            //  {
            //      localStorage.setItem("test", 1);
            //  }
            //var stringconnect = ServerConnection(statusView.storeId); 
            //if (stringconnect != null)
            //{
            //    try
            //    {
            //        using (SqlConnection con = new SqlConnection(stringconnect))
            //        {
            //            string sql = "Update ePreOrderDetails set Quantity=@quantity,TotalPrice=@totalPrice where Id=@orderId";
            //            con.Open();
            //            using (SqlCommand cmd = new SqlCommand(sql, con))    //store data in epreorderDetail table
            //            {
            //                cmd.Parameters.Add("@quantity", SqlDbType.Int).Value = statusView.Quantity;
            //                var UpdatedTotalPrice = statusView.Quantity * statusView.UnitPrice;
            //                cmd.Parameters.Add("@totalPrice", SqlDbType.Int).Value = UpdatedTotalPrice;
            //                cmd.Parameters.Add("@orderId", SqlDbType.Decimal).Value = statusView.Id;
            //                cmd.CommandType = CommandType.Text;
            //                cmd.ExecuteNonQuery();
            //                //con.Close();
            //            }
            //            SqlCommand command = con.CreateCommand();
            //            command.CommandText = "SELECT SUM(TotalPrice) AS TotalPrice FROM ePreOrderDetails where OrderId =" + statusView.OrderId;

            //            var TotalPrice = command.ExecuteScalar();
            //            if(TotalPrice != null)
            //            {
            //                SqlCommand commands = con.CreateCommand();
            //                commands.CommandText = "Update ePreOrders set Total = "+ TotalPrice + " where Id = " + statusView.OrderId;
            //                commands.ExecuteNonQuery();
            //            }
            //            con.Close();

            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //    }
            //}
        }
    }
}
