using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Persistence.Repositories
{
    public class DBSPContext
    {

        private readonly ECXWebsiteDbContext _context;

        public DBSPContext(ECXWebsiteDbContext context)
        {
            _context = context;
        }

        public DataTable DBExecute(string strConnectionString,
                                   string strStoredProcedureName,
                                   ArrayList arrListParamName,
                                   ArrayList arrListParamValue,
                                   ref string strErrMsg)
        {
            SqlConnection connection = new SqlConnection(strConnectionString);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();

            try
            {
                sqlDataAdapter.SelectCommand = new SqlCommand();
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandText = "dbo." + strStoredProcedureName;
                sqlDataAdapter.SelectCommand.Connection = connection;
                sqlDataAdapter.SelectCommand.CommandTimeout = 0;

                for (int i = 0; i < arrListParamName.Count; i++)
                {
                    sqlDataAdapter.SelectCommand.Parameters.Add(new SqlParameter(arrListParamName[i].ToString(), arrListParamValue[i].ToString()));
                }

                connection.Open();
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
        public DataTable DBExecute(string strConnectionString, string strStoredProcedureName, ref string strErrMsg)
        {
            SqlConnection connection = new SqlConnection(strConnectionString);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();

            try
            {
                sqlDataAdapter.SelectCommand = new SqlCommand();
                sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand.CommandText = "dbo." + strStoredProcedureName;
                sqlDataAdapter.SelectCommand.Connection = connection;
                sqlDataAdapter.SelectCommand.CommandTimeout = 0;

                connection.Open();
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
