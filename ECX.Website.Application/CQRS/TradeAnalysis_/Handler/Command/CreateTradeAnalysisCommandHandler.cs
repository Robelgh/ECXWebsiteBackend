using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.Subscription_.Request.Command;
using ECX.Website.Application.CQRS.TradeAnalysis_.Request.Command;
using ECX.Website.Application.DTOs.Common.Validators;
using ECX.Website.Application.DTOs.Subscription;
using ECX.Website.Application.DTOs.Subscription.Validators;
using ECX.Website.Application.DTOs.TradeAnalysis;
using ECX.Website.Application.DTOs.TradeAnalysis.Validators;
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
    public class CreateTradeAnalysisCommandHandler : IRequestHandler<CreateTradeAnalysisCommand, BaseCommonResponse>
    {
        private ITradeAnalysisRepository _tradeanalysisRepository;
        private IMapper _mapper;

        public CreateTradeAnalysisCommandHandler(ITradeAnalysisRepository tradeanalysisRepository, IMapper mapper)
        {
            _tradeanalysisRepository = tradeanalysisRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommonResponse> Handle(CreateTradeAnalysisCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommonResponse();
            var validator = new CreateTradeAnalysisDtoValidators();
            var validationResult = await validator.ValidateAsync(request.TradeAnalysisFormDto);
            var TradeAnalysisDto = _mapper.Map<TradeAnalysisFormDto>(request.TradeAnalysisFormDto);
            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Faild";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                response.Status = "400";
            }
            else
            {
                try
                {
                    var pdfValidator = new PdfValidator();
                    var pdfValidationResult = await pdfValidator.ValidateAsync(request.TradeAnalysisFormDto.FileName);

                    if (pdfValidationResult.IsValid == false)
                    {
                        response.Success = false;
                        response.Message = "Creation Faild";
                        response.Errors = pdfValidationResult.Errors.Select(x => x.ErrorMessage).ToList();
                        response.Status = "400";
                    }
                    else
                    {
                        string contentType = request.TradeAnalysisFormDto.FileName.ContentType.ToString();
                        string ext = contentType.Split('/')[1];
                        string fileName = Guid.NewGuid().ToString() + "." + ext;
                        string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\pdf", fileName);

                        using (Stream stream = new FileStream(path, FileMode.Create))
                        {
                            request.TradeAnalysisFormDto.FileName.CopyTo(stream);
                        }
                        var TradeAnalysisesDto = _mapper.Map<TradeAnalysisDto>(request.TradeAnalysisFormDto);
                        TradeAnalysisesDto.FileName = fileName;

                        Guid publicationId;
                        bool flag = true;

                        while (true)
                        {
                            publicationId = (Guid.NewGuid());
                            flag = await _tradeanalysisRepository.Exists(publicationId);
                            if (flag == false)
                            {
                                TradeAnalysisesDto.Id = publicationId;
                                break;
                            }
                        }

                        var data = _mapper.Map<TradeAnalysis>(TradeAnalysisesDto);

                        var saveData = await _tradeanalysisRepository.Add(data);

                        response.Data = _mapper.Map<TradeAnalysisDto>(saveData);
                        response.Success = true;
                        response.Message = "Created Successfully";
                        response.Status = "200";
                    }
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = "Creation Failed";
                    response.Errors = new List<string> { ex.Message };
                    response.Status = "400";
                }

            }
            return response;
        }
    }
}
