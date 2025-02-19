using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Persistence.Repositories
{
    public class MCRRepository : IMCRRepository
    {
        private IConfiguration _configuration;

        public MCRRepository(IConfiguration configuration)
        {

            _configuration = configuration;
        }
        public  DataTable GetGRNStatus(string id,string grn)
        {
            var state = "";
            SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:StagingwarehouseApplicationVersion2"]);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            var strErrMsg = "";
            try
            {
                sqlDataAdapter.SelectCommand = new SqlCommand();
                SqlParameter sqlParameter = new SqlParameter("@Id", SqlDbType.NVarChar);
                sqlParameter.Value = id;
                SqlParameter sqlParameter2 = new SqlParameter("@grnnumber", SqlDbType.NVarChar);
                sqlParameter2.Value = grn;
                sqlDataAdapter.SelectCommand.Parameters.Add(sqlParameter);
                sqlDataAdapter.SelectCommand.Parameters.Add(sqlParameter2);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandText = "dbo." + "getGRNMemberstatus";
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

        public DataTable GetMemberClientList(string id)
        {
            var state = "";
            SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:ECXStaggingConnectionStringMembership"]);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            var strErrMsg = "";
            try
            {
                sqlDataAdapter.SelectCommand = new SqlCommand();
                SqlParameter sqlParameter = new SqlParameter("@MemberId ", SqlDbType.NVarChar);
                sqlParameter.Value = id;
                sqlDataAdapter.SelectCommand.Parameters.Add(sqlParameter);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandText = "dbo." + "spGetMemberClientList";
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

        public DataTable GetPSA(string psa)
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
                sqlParameter.Value = psa;
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

        public DataTable GetPSAStatus(string psa)
        {
            throw new NotImplementedException();
        }

        public DataTable GetTradeStatusbyWHR(string whr)
        {
            var state = "";
            SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:ECXStaggingConnectionStrinECXTrade"]);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            var strErrMsg = "";
            try
            {
                sqlDataAdapter.SelectCommand = new SqlCommand();
                SqlParameter sqlParameter = new SqlParameter("@whrnumber", SqlDbType.NVarChar);
                sqlParameter.Value = whr;
                sqlDataAdapter.SelectCommand.Parameters.Add(sqlParameter);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandText = "dbo." + "spGetTradeStatusbyWGRforwebsite";
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

        public DataTable GetWHRStatus(string whr)
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
                sqlParameter.Value = whr;
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

        public DataTable GetWHRStatusbyGRN(string grn)
        {
            var state = "";
            SqlConnection connection = new SqlConnection(_configuration["ConnectionStrings:ECXStaggingConnectionString"]);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            var strErrMsg = "";
            try
            {
                sqlDataAdapter.SelectCommand = new SqlCommand();
                SqlParameter sqlParameter = new SqlParameter("@grnnumber ", SqlDbType.NVarChar);
                sqlParameter.Value = grn;
                sqlDataAdapter.SelectCommand.Parameters.Add(sqlParameter);
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandText = "dbo." + "spGetWHRStatusbyGRNforwebsite";
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
