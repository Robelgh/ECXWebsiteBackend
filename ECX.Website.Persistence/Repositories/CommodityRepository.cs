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
    public class CommodityRepository : GenericRepository<Commodity>, ICommodityRepository
    {
        private readonly ECXWebsiteDbContext _context;
        private IConfiguration _configuration;

        public CommodityRepository(ECXWebsiteDbContext context , IConfiguration configuration) : base(context)
        {
            _context = context;
        }

  

     

        public DataTable GetCommodityDetailData()
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
