using ECX.Website.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Domain
{
    public class ComplainFeedBack : BaseDomainEntity
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }


    }
}
