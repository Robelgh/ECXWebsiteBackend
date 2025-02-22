﻿using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECX.Website.Persistence.Repositories
{
    public class VacancyRepository : GenericRepository<Vacancy>, IVacancyRepository
    {
        private readonly ECXWebsiteDbContext _context;

        public VacancyRepository(ECXWebsiteDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vacancy>> getVacancy()
        {
            return _context.Set<Vacancy>().Where(p => p.ExpDate > DateTime.Today).ToList();
        }
    }
}
