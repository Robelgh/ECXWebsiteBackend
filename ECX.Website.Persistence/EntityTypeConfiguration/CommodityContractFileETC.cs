using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

using ECX.Website.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECX.Website.Persistence.EntityTypeConfiguration
{
    public class CommodityContractFileETC : IEntityTypeConfiguration<ContractFile>
    {
        public void Configure(EntityTypeBuilder<ContractFile> builder)
        {
            builder.ToTable("ContractFiles");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Commodity).WithOne(b => b.ContractFile).HasForeignKey<ContractFile>(b => b.CommodityId);
             
        }
    }
}
