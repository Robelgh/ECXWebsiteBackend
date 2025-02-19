using ECX.Website.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Domain
{
    public class TradeAnalysis : BaseDomainEntity
    {

        //  public Guid LangId { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Type { get; set; }
        public string FileName { get; set; }

        public DateTime Date { get; set; }
    }
}