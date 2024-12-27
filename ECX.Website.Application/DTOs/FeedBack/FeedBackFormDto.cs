using ECX.Website.Application.DTOs.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.DTOs.FeedBack
{
    public class FeedBackFormDto : BaseDtos
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
