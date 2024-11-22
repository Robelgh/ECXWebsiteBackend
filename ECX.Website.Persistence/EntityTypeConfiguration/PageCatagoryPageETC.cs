using ECX.Website.Domain.Lookup;
using ECX.Website.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Persistence.EntityTypeConfiguration
{
    public class PageCatagoryPageETC : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.ToTable("Page");
            builder.HasKey(x => x.Id);

            builder
                 .HasOne(e => e.PageCatagory)
                 .WithMany(e => e.Page)
                 .HasForeignKey(e => e.PageCatagoryId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
