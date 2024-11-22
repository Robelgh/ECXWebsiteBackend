using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Domain;
using ECX.Website.Domain.Lookup;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECX.Website.Persistence.Repositories
{
    public class PageCatagoryRepository : GenericRepository<PageCatagory>, IPageCatagoryRepository
    {
        private readonly ECXWebsiteDbContext _context;

        public PageCatagoryRepository(ECXWebsiteDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<IEnumerable<PageCatagory>> GetCatagoryByLangId(Guid langId)
        {
            return _context.Set<PageCatagory>().Where(p => p.LangId == langId);
        }

        public async Task<IEnumerable<PageCatagory>> GetCatagoryByParentId(Guid parentId)
        {
            return _context.Set<PageCatagory>().Where(p => p.ParentLookupId == parentId);
        }

        
    }


}
