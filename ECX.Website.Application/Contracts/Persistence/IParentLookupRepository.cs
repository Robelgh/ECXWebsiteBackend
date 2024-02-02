using ECX.Website.Domain.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.Contracts.Persistence
{
    public interface IParentLookupRepository : IGenericRepository<ParentLookup>
    {
    }
}
