using ECX.Website.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.Contracts.Persistence
{
    public interface IPageCatagoryRepository : IGenericRepository<PageCatagory>
    {
        Task<IEnumerable<PageCatagory>> GetCatagoryByLangId(Guid langId);

        Task<IEnumerable<PageCatagory>> GetCatagoryByParentId(Guid parentId);

        
    }
}
