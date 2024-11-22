using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECX.Website.Persistence.Repositories
{
    public class FactRepository : GenericRepository<Facts>, IFactRepository
    {
        private readonly ECXWebsiteDbContext _context;

        public FactRepository(ECXWebsiteDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
