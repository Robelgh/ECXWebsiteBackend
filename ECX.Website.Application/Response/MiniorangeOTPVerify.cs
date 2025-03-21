using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.Response
{
    public class MiniorangeOTPVerify
    {
        public string txId { get; set; }
        public string responseType { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }
}
