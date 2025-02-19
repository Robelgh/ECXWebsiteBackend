using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.Publication_.Request.Queries;
using ECX.Website.Application.CQRS.TradeAnalysis_.Request.Queries;
using ECX.Website.Application.DTOs.Publication;
using ECX.Website.Application.DTOs.TradeAnalysis;
using ECX.Website.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.CQRS.TradeAnalysis_.Handler.Queries
{
    public class GetTradeAnalysisListRequestHandler : IRequestHandler<GetTradeAnalysisListRequest, BaseCommonResponse>
    {
        
        private ITradeAnalysisRepository _tradeanalysisRepository;
        private IMapper _mapper;
        public GetTradeAnalysisListRequestHandler(ITradeAnalysisRepository tradeanalysisRepository, IMapper mapper)
        {
            _tradeanalysisRepository = tradeanalysisRepository;
            _mapper = mapper;
        }

            public async Task<BaseCommonResponse> Handle(GetTradeAnalysisListRequest request, CancellationToken cancellationToken)
            {
                var response = new BaseCommonResponse();
                var data = await _tradeanalysisRepository.GetAll();

                response.Success = true;
                response.Data = _mapper.Map<List<TradeAnalysisDto>>(data);
                response.Status = "200";

                return response;
            }
        }
}
