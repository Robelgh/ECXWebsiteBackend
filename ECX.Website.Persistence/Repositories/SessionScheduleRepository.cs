using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Persistence.Repositories
{
    public class SessionScheduleRepository : GenericRepository<SessionSchedule>, ISessionScheduleRepository
    {
        private readonly ECXWebsiteDbContext _context;

        public SessionScheduleRepository(ECXWebsiteDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
