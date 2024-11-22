using ECX.Website.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECX.Website.Domain
{
    public class ContractFile : BaseDomainEntity
    {

        public Guid LangId { get; set; }
        public Guid CommodityId { get; set;}
        public string Name { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public Commodity Commodity { get; set; }

    }
}
