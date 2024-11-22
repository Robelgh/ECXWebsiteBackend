using ECX.Website.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Domain.Lookup
{
    public class ParentLookup : BaseDomainEntity
    {
        public Guid LangId { get; set; }
        public string Title { get; set; }
        public string ? Description { get; set; }
        public string ? ImgName { get; set; }
        public ICollection<PageCatagory> PageCatagory { get; } = new List<PageCatagory>(); // Collection navigation containing dependents


    }
}
