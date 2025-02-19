using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.TradeAnalysis_.Request.Command;
using ECX.Website.Application.CQRS.TrainingDoc_.Request.Command;
using ECX.Website.Application.Exceptions;
using ECX.Website.Application.Response;
using ECX.Website.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.CQRS.TradeAnalysis_.Handler.Command
{
    public class DeleteTradeAnalysisCommandHandler : IRequestHandler<DeleteTradeAnalysisCommand, BaseCommonResponse>
    {

        private ITradeAnalysisRepository _tradeanalysisRepository;
        private IMapper _mapper;
        public DeleteTradeAnalysisCommandHandler(ITradeAnalysisRepository tradeanalysisRepository, IMapper mapper)
        {
            _tradeanalysisRepository = tradeanalysisRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommonResponse> Handle(DeleteTradeAnalysisCommand request, CancellationToken cancellationToken)
        {
            var data = await _tradeanalysisRepository.GetById(request.Id);
            var response = new BaseCommonResponse();

            if (data == null)
            {
                response.Success = false;
                response.Message = new NotFoundException(
                            nameof(TrainingDoc), request.Id).Message.ToString();
                response.Status = "404";
            }
            else
            {
                await _tradeanalysisRepository.Delete(data);

                string path = Path.Combine(
                    Directory.GetCurrentDirectory(), @"wwwroot\pdf", data.FileName);

                File.Delete(path);

                response.Success = true;
                response.Message = "Successfully Deleted";
                response.Status = "200";

            }

            return response;
        }
    }
}
