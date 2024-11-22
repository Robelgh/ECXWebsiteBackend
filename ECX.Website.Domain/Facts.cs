using ECX.Website.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECX.Website.Domain
{
    public class Facts : BaseDomainEntity
    {

        public Guid LangId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
      

    }
}
