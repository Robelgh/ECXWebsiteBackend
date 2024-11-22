using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.Contracts.Persistence
{
    public interface IDBSPContext
    {
   

        public DataTable DBExcute(string strConnection, string strStoreProcedureName, ArrayList arrListParamValue, ref string strErrMsg);
        public DataTable DBExcute(string strConnection, string strStoreProcedureName, ref string strErrMsg);
    }
}
