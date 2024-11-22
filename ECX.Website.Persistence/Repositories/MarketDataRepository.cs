using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.DTOs.Email;
using ECX.Website.Application.DTOs.MarketData;
using ECX.Website.Application.Response;
using ECX.Website.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
                sqlDataAdapter.SelectCommand.CommandText = "dbo." + "RgetActiveCommodity";
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

        

        //private async  static Task<List<ScrollingData>> ConvertDataTable<ScrollingData>(DataTable dt)
        //{

        //     List <ScrollingData> data = new List<ScrollingData>();
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        ScrollingData item = GetItem<ScrollingData>(row);
        //        data.Add(item);
        //    }
        //    return  data;
        //}
        //private static T GetItem<T>(DataRow dr)
        //{
        //    Type temp = typeof(T);
        //    T obj = Activator.CreateInstance<T>();

        //    foreach (DataColumn column in dr.Table.Columns)
        //    {
        //        foreach (PropertyInfo pro in temp.GetProperties())
        //        {
        //            if (pro.Name == column.ColumnName)
        //                pro.SetValue(obj, dr[column.ColumnName], null);
        //            else
        //                continue;
        //        }
        //    }
        //    return obj;
        //}
    }

}
