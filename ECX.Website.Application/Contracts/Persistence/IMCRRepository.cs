using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.Contracts.Persistence
{
    public interface IMCRRepository
    {

        DataTable GetGRNStatus(string username,string grn);
        DataTable GetWHRStatus(string whr);

        DataTable GetPSAStatus(string psa); 
        DataTable GetMemberClientList(string id);
        DataTable GetWHRStatusbyGRN(string grn);
        DataTable GetTradeStatusbyWHR(string whr); 


    }
}
