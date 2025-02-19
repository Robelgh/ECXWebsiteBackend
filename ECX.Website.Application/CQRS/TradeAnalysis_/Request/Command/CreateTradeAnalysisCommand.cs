using ECX.Website.Application.DTOs.TradeAnalysis;
using ECX.Website.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.CQRS.TradeAnalysis_.Request.Command
{ 
    public class CreateTradeAnalysisCommand : IRequest<BaseCommonResponse>
    {
        public TradeAnalysisFormDto TradeAnalysisFormDto { get; set; }
    }
}
