using ECX.Website.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECX.Website.Domain.Lookup;
using System.Reflection.Emit;

namespace ECX.Website.Persistence.EntityTypeConfiguration
{
    public class ParentLookupPageCategoryETC : IEntityTypeConfiguration<PageCatagory>
    {
        public void Configure(EntityTypeBuilder<PageCatagory> builder)
        {
            builder.ToTable("PageCatagories");
            builder.HasKey(x => x.Id);
            //builder.HasOne<ParentLookup>().WithMany(x => x.PageCatagory).HasForeignKey<PageCatagory>(b => b.ParentLookupId)
            //    .OnDelete(DeleteBehavior.ClientCascade);
            //builder.HasOne(x => x.ParentLookup).WithMany(b => b.PageCatagory).HasForeignKey<PageCatagory>(b => b.ParentLookupId)
            // .OnDelete(DeleteBehavior.Cascade);
            builder
                  .HasOne(e => e.ParentLookup)
                  .WithMany(e => e.PageCatagory)
                  .HasForeignKey(e => e.ParentLookupId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
