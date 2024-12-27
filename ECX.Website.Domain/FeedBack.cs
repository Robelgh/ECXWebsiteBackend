using ECX.Website.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECX.Website.Domain
{
    public class FeedBack : BaseDomainEntity
    {

        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public string? Answer { get; set; }
        public string? AnsweredBy { get; set; }
        public bool requestSeen { get; set; }
        public bool answerSeen { get; set; }

    }
}
