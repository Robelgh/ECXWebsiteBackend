using ECX.Website.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECX.Website.Domain
{
    public class Vacancy : BaseDomainEntity
    {

        public Guid LangId { get; set; }
        public string Title { get; set; }
        public Guid PageCatagoryId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTime ExpDate { get; set; }

        public PageCatagory PageCatagory { get; set; } = null!; // Required reference navigation to principal


    }
}
