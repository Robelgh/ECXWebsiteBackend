using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Domain;
using ECX.Website.Domain.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Persistence.Repositories
{
    public class ParentLookupRepository : GenericRepository<ParentLookup>, IParentLookupRepository
    {
        private readonly ECXWebsiteDbContext _context;

        public ParentLookupRepository(ECXWebsiteDbContext context) : base(context)
        {
            _context = context;
        }
    }
 
}
