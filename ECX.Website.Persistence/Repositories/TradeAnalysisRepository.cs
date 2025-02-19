using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Persistence.Repositories
{
    public class TradeAnalysisRepository : GenericRepository<TradeAnalysis>, ITradeAnalysisRepository
    {
        private readonly ECXWebsiteDbContext _context;

    public TradeAnalysisRepository(ECXWebsiteDbContext context) : base(context)
    {
        _context = context;
    }
}
}
