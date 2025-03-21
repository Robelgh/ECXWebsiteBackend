using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.DTOs.Account
{
    public class MiniorangeDto
    {
        public string customerKey { get; set; }
        public string username { get; set; }  
        public string authType { get; set; }   
        public string transactionName { get; set; }

    }
    public class MiniorangeValidationDto
    {
        public string txId { get; set; }
        public string token { get; set; }

    }
}
