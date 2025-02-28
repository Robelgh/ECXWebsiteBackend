using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ECX.Website.Persistence.Repositories
{

    public class MarketDataRepository : IMarketDataRepository
    {
        
        private IConfiguration _configuration;

        public MarketDataRepository(IConfiguration configuration)
        {
            
            _configuration = configuration;
        }

        public DataTable GetActiveCommodities()
        {
            var state = "";
            SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:ECXLookupConnectionString"]);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            var strErrMsg = "";
            try
            {
                sqlDataAdapter.SelectCommand = new SqlCommand();
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandText = "dbo." + "spGetCommodity";
                sqlDataAdapter.SelectCommand.Connection = connection;
                sqlDataAdapter.SelectCommand.CommandTimeout = 0;

                connection.Open();
                state = ConnectionState.Open.ToString();
                sqlDataAdapter.Fill(dt);
            }
            catch (Exception e)
            {
                strErrMsg = e.Message;
            }
            finally
            {
                if (connection.State.ToString() == System.Data.ConnectionState.Open.ToString())
                    connection.Close();

                sqlDataAdapter.Dispose();
            }

            return dt;


        }


        public DataTable GetScrollingData()
        {
            var state = "";
            SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:ECXMarketDataConnectionString"]);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            var strErrMsg = "";
            try
            {
                sqlDataAdapter.SelectCommand = new SqlCommand();
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandText = "dbo." + "RgetAllScrollingPice";
                sqlDataAdapter.SelectCommand.Connection = connection;
                sqlDataAdapter.SelectCommand.CommandTimeout = 0;

                connection.Open();
                state= ConnectionState.Open.ToString();
                sqlDataAdapter.Fill(dt);
            }
            catch (Exception e)
            {
                strErrMsg = e.Message;
            }
            finally
            {
                if (connection.State.ToString() == System.Data.ConnectionState.Open.ToString())
                    connection.Close();

                sqlDataAdapter.Dispose();
            }

            return dt;


        }

        public DataTable GetHistoryData(DateTime from , DateTime to , string commodity)
        {
            var state = "";
            SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:ECXMarketDataConnectionString"]);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            var strErrMsg = "";
            try
            {
                sqlDataAdapter.SelectCommand = new SqlCommand();
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandText = "dbo." + "RgetHistoryMarketData";
                sqlDataAdapter.SelectCommand.Connection = connection;
                sqlDataAdapter.SelectCommand.CommandTimeout = 0;

                connection.Open();
                state = ConnectionState.Open.ToString();
                sqlDataAdapter.Fill(dt);
            }
            catch (Exception e)
            {
                strErrMsg = e.Message;
            }
            finally
            {
                if (connection.State.ToString() == System.Data.ConnectionState.Open.ToString())
                    connection.Close();

                sqlDataAdapter.Dispose();
            }

            return dt;


        }

        public DataTable GetRealTimeData()
        {
            var state = "";
            SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:ECXMarketDataConnectionString"]);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            var strErrMsg = "";
            try
            {
                sqlDataAdapter.SelectCommand = new SqlCommand();
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandText = "dbo." + "RgetRealtimeData";
                sqlDataAdapter.SelectCommand.Connection = connection;
                sqlDataAdapter.SelectCommand.CommandTimeout = 0;

                connection.Open();
                state = ConnectionState.Open.ToString();
                sqlDataAdapter.Fill(dt);
            }
            catch (Exception e)
            {
                strErrMsg = e.Message;
            }
            finally
            {
                if (connection.State.ToString() == System.Data.ConnectionState.Open.ToString())
                    connection.Close();

                sqlDataAdapter.Dispose();
            }

            return dt;


        }

        public DataTable GetCommodityMarketData(string commodity)
        {
            var state = "";
            SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:ECXMarketDataConnectionString"]);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            var strErrMsg = "";
            try
            {
                sqlDataAdapter.SelectCommand = new SqlCommand();
                SqlParameter sqlParameter = new SqlParameter("@Commodity", SqlDbType.NVarChar);
                sqlParameter.Value = commodity;
                sqlDataAdapter.SelectCommand.Parameters.Add(sqlParameter);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandText = "dbo." + "RgetCommodityMarketData";
                sqlDataAdapter.SelectCommand.Connection = connection;
                sqlDataAdapter.SelectCommand.CommandTimeout = 0;

                connection.Open();
                state = ConnectionState.Open.ToString();
                sqlDataAdapter.Fill(dt);
            }
            catch (Exception e)
            {
                strErrMsg = e.Message;
            }
            finally
            {
                if (connection.State.ToString() == System.Data.ConnectionState.Open.ToString())
                    connection.Close();

                sqlDataAdapter.Dispose();
            }

            return dt;

        }

        public DataTable GetSmbolMarketData(string symbol)
        {
            var state = "";
            SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:ECXMarketDataConnectionString"]);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            var strErrMsg = "";
            try
            {
                sqlDataAdapter.SelectCommand = new SqlCommand();
                SqlParameter sqlParameter = new SqlParameter("@symbol", SqlDbType.NVarChar);
                sqlParameter.Value = symbol;
                sqlDataAdapter.SelectCommand.Parameters.Add(sqlParameter);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandText = "dbo." + "Rget30MarketDataForSymbol";
                sqlDataAdapter.SelectCommand.Connection = connection;
                sqlDataAdapter.SelectCommand.CommandTimeout = 0;

                connection.Open();
                state = ConnectionState.Open.ToString();
                sqlDataAdapter.Fill(dt);
            }
            catch (Exception e)
            {
                strErrMsg = e.Message;
            }
            finally
            {
                if (connection.State.ToString() == System.Data.ConnectionState.Open.ToString())
                    connection.Close();

                sqlDataAdapter.Dispose();
            }

            return dt;

        }

        public DataTable GetcommodityGrade()
        {
            var state = "";
            SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:ECXLookupConnectionString"]);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            var strErrMsg = "";
            try
            {
                sqlDataAdapter.SelectCommand = new SqlCommand();
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandText = "dbo." + "spGetCommodityGrade";
                sqlDataAdapter.SelectCommand.Connection = connection;
                sqlDataAdapter.SelectCommand.CommandTimeout = 0;

                connection.Open();
                state = ConnectionState.Open.ToString();
                sqlDataAdapter.Fill(dt);
            }
            catch (Exception e)
            {
                strErrMsg = e.Message;
            }
            finally
            {
                if (connection.State.ToString() == System.Data.ConnectionState.Open.ToString())
                    connection.Close();

                sqlDataAdapter.Dispose();
            }

            return dt;
        }

        public DataTable GetCommodity()
        {
            var state = "";
            SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:ECXConnectionString"]);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            var strErrMsg = "";
            try
            {
                sqlDataAdapter.SelectCommand = new SqlCommand();
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandText = "dbo." + "spGetCommodityForWebSite";
                sqlDataAdapter.SelectCommand.Connection = connection;
                sqlDataAdapter.SelectCommand.CommandTimeout = 0;

                connection.Open();
                state = ConnectionState.Open.ToString();
                sqlDataAdapter.Fill(dt);
            }
            catch (Exception e)
            {
                strErrMsg = e.Message;
            }
            finally
            {
                if (connection.State.ToString() == System.Data.ConnectionState.Open.ToString())
                    connection.Close();

                sqlDataAdapter.Dispose();
            }

            return dt;
        }

        public DataTable GetPretradeNonCoffeeMarketData(string commodity)
        {
            var state = "";
            SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:ECXStaggingConnectionString"]);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            var strErrMsg = "";
            try
            {
                sqlDataAdapter.SelectCommand = new SqlCommand();
                SqlParameter sqlParameter = new SqlParameter("@CommodityID", SqlDbType.NVarChar);
                sqlParameter.Value = commodity;
                sqlDataAdapter.SelectCommand.Parameters.Add(sqlParameter);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandText = "dbo." + "spGetPreTradeInfoForNonCoffeepercommodityforwebsite";
                sqlDataAdapter.SelectCommand.Connection = connection;
                sqlDataAdapter.SelectCommand.CommandTimeout = 0;

                connection.Open();
                state = ConnectionState.Open.ToString();
                sqlDataAdapter.Fill(dt);
            }
            catch (Exception e)
            {
                strErrMsg = e.Message;
            }
            finally
            {
                if (connection.State.ToString() == System.Data.ConnectionState.Open.ToString())
                    connection.Close();

                sqlDataAdapter.Dispose();
            }

            return dt;
        }

        public DataTable GetPretradeCoffeeMarketData(string Num)
        {
            var state = "";
            SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:ECXStaggingConnectionString"]);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            var strErrMsg = "";
            try
            {
                sqlDataAdapter.SelectCommand = new SqlCommand();
                SqlParameter sqlParameter = new SqlParameter("@IsLocal", SqlDbType.NVarChar);
                sqlParameter.Value = Num;
                sqlDataAdapter.SelectCommand.Parameters.Add(sqlParameter);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandText = "dbo." + "spGetPreTradeInfoForWebsite";
                sqlDataAdapter.SelectCommand.Connection = connection;
                sqlDataAdapter.SelectCommand.CommandTimeout = 0;

                connection.Open();
                state = ConnectionState.Open.ToString();
                sqlDataAdapter.Fill(dt);
            }
            catch (Exception e)
            {
                strErrMsg = e.Message;
            }
            finally
            {
                if (connection.State.ToString() == System.Data.ConnectionState.Open.ToString())
                    connection.Close();

                sqlDataAdapter.Dispose();
            }



            // TRV


            return dt;
        }

        public DataTable GetCommodityTradeData(string commodity)
        {
            var state = "";
            SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:ECXMarketDataConnectionString"]);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            var strErrMsg = "";
            try
            {
                sqlDataAdapter.SelectCommand = new SqlCommand();
                SqlParameter sqlParameter = new SqlParameter("@Commodity", SqlDbType.NVarChar);
                sqlParameter.Value = commodity;
                sqlDataAdapter.SelectCommand.Parameters.Add(sqlParameter);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandText = "dbo." + "RgetCommodityDailyTradeData";
                sqlDataAdapter.SelectCommand.Connection = connection;
                sqlDataAdapter.SelectCommand.CommandTimeout = 0;

                connection.Open();
                state = ConnectionState.Open.ToString();
                sqlDataAdapter.Fill(dt);
            }
            catch (Exception e)
            {
                strErrMsg = e.Message;
            }
            finally
            {
                if (connection.State.ToString() == System.Data.ConnectionState.Open.ToString())
                    connection.Close();

                sqlDataAdapter.Dispose();
            }

            return dt;
        }

        public DataTable getNonTraceableCoffeePretrade()
        {
            var state = "";
            SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:ECXStaggingConnectionString"]);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            var strErrMsg = "";
            try
            {
                sqlDataAdapter.SelectCommand = new SqlCommand();
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandText = "dbo." + "spGetPreTradeInfoNonTraceableForWebsite";
                sqlDataAdapter.SelectCommand.Connection = connection;
                sqlDataAdapter.SelectCommand.CommandTimeout = 0;

                connection.Open();
                state = ConnectionState.Open.ToString();
                sqlDataAdapter.Fill(dt);
            }
            catch (Exception e)
            {
                strErrMsg = e.Message;
            }
            finally
            {
                if (connection.State.ToString() == System.Data.ConnectionState.Open.ToString())
                    connection.Close();

                sqlDataAdapter.Dispose();
            }

            return dt;
        }
    }

}
