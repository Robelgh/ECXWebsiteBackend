using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.TradeAnalysis_.Request.Queries;
using ECX.Website.Application.CQRS.TrainingDoc_.Request.Queries;
using ECX.Website.Application.DTOs.TradeAnalysis;
using ECX.Website.Application.DTOs.TrainingDoc;
using ECX.Website.Application.Exceptions;
using ECX.Website.Application.Response;
using ECX.Website.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.CQRS.TradeAnalysis_.Handler.Queries
{
    public class GetTradeAnalysisDetailRequestHandler : IRequestHandler<GetTradeAnalysisDetailRequest, BaseCommonResponse>
    {
        private ITradeAnalysisRepository _tradeanalysisRepository;
        private IMapper _mapper;

        public GetTradeAnalysisDetailRequestHandler(ITradeAnalysisRepository tradeanalysisRepository, IMapper mapper)
        {
            _tradeanalysisRepository = tradeanalysisRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommonResponse> Handle(GetTradeAnalysisDetailRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseCommonResponse();
            var data = await _tradeanalysisRepository.GetById(request.Id);
            if (data != null)
            {
                response.Success = true;
                response.Data = _mapper.Map<TradeAnalysisDto>(data);
                response.Status = "200";
            }
            else
            {
                response.Success = false;
                response.Message = new NotFoundException(
                          nameof(TradeAnalysisDto), request.Id).Message.ToString();
                response.Status = "404";
            }
            return response;
        }
    }
}
