using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.Response
{
    public class ResponseAccount
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
