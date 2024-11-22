using ECX.Website.Domain.Common;
using ECX.Website.Domain.Lookup;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECX.Website.Domain
{
    public class Page : BaseDomainEntity
    {

        public Guid LangId { get; set; }

        public Guid? PageCatagoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        // public Guid CatagoryId { get; set; }
        public string ImgName { get; set; }

        public PageCatagory PageCatagory { get; set; } = null!; // Required reference navigation to principal

        //public PageCatagory PageCatagory { get; set;}

    }
}