﻿using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECX.Website.Persistence.Repositories
{
    public class ContractFileRepository : GenericRepository<ContractFile>, IContractFileRepository
    {
        private readonly ECXWebsiteDbContext _context;

        public ContractFileRepository(ECXWebsiteDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContractFile>> GetContractByCommodityId(Guid commodityId)
        {
            return _context.Set<ContractFile>().Where(p => p.CommodityId == commodityId);
        }

        
    }
}
