﻿using ECX.Website.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.Contracts.Persistence
{
    public interface IPageRepository : IGenericRepository<Page>
    {
        Task<IEnumerable<Page>> GetByPageCatagoryLangId(Guid Id, Guid langId);
    }
}
