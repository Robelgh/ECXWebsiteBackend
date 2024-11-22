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
    public class PageCatagoryVacancyETC : IEntityTypeConfiguration<Vacancy>
    {
        public void Configure(EntityTypeBuilder<Vacancy> builder)
        {
            builder.ToTable("Vacancy");
            builder.HasKey(x => x.Id);

            builder
                 .HasOne(e => e.PageCatagory)
                 .WithMany(e => e.Vacancy)
                 .HasForeignKey(e => e.PageCatagoryId)
                 .IsRequired()
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

