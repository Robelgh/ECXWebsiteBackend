using ECX.Website.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Domain
{
    public class LoginTrader : BaseDomainEntity
    {
        public Guid Uniqueidentifier { get; set; }
        public string Name { get; set; } = string.Empty;
        public string RepID { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string SessionID { get; set; } = string.Empty;
        public Guid MemberGuid { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public string MemberID { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool MustChangePassword { get; set; }
    }
}
