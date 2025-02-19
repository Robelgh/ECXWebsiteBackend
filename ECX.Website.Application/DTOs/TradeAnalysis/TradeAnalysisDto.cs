using ECX.Website.Application.DTOs.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.DTOs.TradeAnalysis
{
    public class TradeAnalysisDto : BaseDtos
    {
        public string Name { get; set; }
        public string Summary { get; set; } 
        public string Type { get; set; }
        public string FileName { get; set; }
        public DateTime Date { get; set; }
    }
}
