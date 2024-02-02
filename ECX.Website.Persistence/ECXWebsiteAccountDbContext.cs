using ECX.Website.Domain;
using ECX.Website.Domain.Common;
using ECX.Website.Persistence.EntityTypeConfiguration;
using ECX.Website.Persistence.RoleConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Persistence
{
    public class ECXWebsiteAccountDbContext : IdentityDbContext
    {

        public ECXWebsiteAccountDbContext(DbContextOptions<ECXWebsiteAccountDbContext> options) : base(options)
        {

        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entity in ChangeTracker.Entries<Account>())
            {
                entity.Entity.UpdatedDate = DateTime.Now;
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedDate = DateTime.Now;
                    entity.Entity.IsActive = true;
                }

            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public DbSet<Account> Accoubts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AccountRoleConfigration());
        }
    }
}
