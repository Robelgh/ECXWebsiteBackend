using ECX.Website.Domain.Common;
using ECX.Website.Domain.Lookup;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECX.Website.Domain
{
    public class PageCatagory : BaseDomainEntity
    {

        public Guid LangId { get; set; }

        public Guid? ParentLookupId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImgName { get; set; }
        public ParentLookup ParentLookup { get; set; } = null!; // Required reference navigation to principal

        public ICollection<Page> Page { get; } = new List<Page>(); // Collection navigation containing dependents
        public ICollection<Vacancy> Vacancy { get; } = new List<Vacancy>(); // Collection navigation containing dependents

    }
}
