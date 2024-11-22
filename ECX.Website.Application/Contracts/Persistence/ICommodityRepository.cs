using ECX.Website.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.Contracts.Persistence
{
    public interface ICommodityRepository : IGenericRepository<Commodity>
    {

        //DataTable GetActiveCommodities();

    }
}
