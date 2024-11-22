using ECX.Website.Application.DTOs.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.DTOs.Fact
{
    public class FactFormDto : BaseDtos
    {
        public Guid LangId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
    }
}
