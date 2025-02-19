using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.TradeAnalysis_.Request.Command;
using ECX.Website.Application.CQRS.TrainingDoc_.Request.Command;
using ECX.Website.Application.DTOs.Common.Validators;
using ECX.Website.Application.DTOs.Subscription;
using ECX.Website.Application.DTOs.TradeAnalysis;
using ECX.Website.Application.DTOs.TradeAnalysis.Validators;
using ECX.Website.Application.DTOs.TrainingDoc.Validators;
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
    public class UpdateTradeAnalysisCommandHandler : IRequestHandler<UpdateTradeAnalysisCommand, BaseCommonResponse>
    {
        private ITradeAnalysisRepository _tradeanalysisRepository;
        private IMapper _mapper;
        public UpdateTradeAnalysisCommandHandler(ITradeAnalysisRepository tradeanalysisRepository, IMapper mapper)
        {
            _tradeanalysisRepository = tradeanalysisRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommonResponse> Handle(UpdateTradeAnalysisCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommonResponse();
            var validator = new CreateTradeAnalysisDtoValidators();
            var validationResult = await validator.ValidateAsync(request.TradeAnalysisFormDto);
            var TradeAnalysisDto = _mapper.Map<TradeAnalysisDto>(request.TradeAnalysisFormDto);
            var flag = await _tradeanalysisRepository.Exists(request.TradeAnalysisFormDto.Id);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Update Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                response.Status = "400";
            }
            else if (flag == false)
            {

                response.Success = false;
                response.Message = new NotFoundException(
                            nameof(TrainingDoc), request.TradeAnalysisFormDto.Id).Message.ToString();
                response.Status = "404";
            }
            else
            {
                if (request.TradeAnalysisFormDto.FileName != null)
                {
                    try
                    {
                        var pdfValidator = new PdfValidator();
                        var pdfValidationResult = await pdfValidator.ValidateAsync(request.TradeAnalysisFormDto.FileName);

                        if (pdfValidationResult.IsValid == false)
                        {
                            response.Success = false;
                            response.Message = "Update Failed";
                            response.Errors = pdfValidationResult.Errors.Select(x => x.ErrorMessage).ToList();
                            response.Status = "400";
                        }
                        else
                        {
                            var oldPdf = (await _tradeanalysisRepository.GetById(
                                request.TradeAnalysisFormDto.Id)).FileName;


                            string oldPath = Path.Combine(
                                Directory.GetCurrentDirectory(), @"wwwroot\pdf", oldPdf);
                            File.Delete(oldPath);

                            string contentType = request.TradeAnalysisFormDto.FileName.ContentType.ToString();
                            string ext = contentType.Split('/')[1];
                            string fileName = Guid.NewGuid().ToString() + "." + ext;
                            string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\pdf", fileName);

                            using (Stream stream = new FileStream(path, FileMode.Create))
                            {
                                request.TradeAnalysisFormDto.FileName.CopyTo(stream);
                            }

                            TradeAnalysisDto.FileName = fileName;
                        }
                    }
                    catch (Exception ex)
                    {
                        response.Success = false;
                        response.Message = "Update Failed";
                        response.Errors = new List<string> { ex.Message };
                        response.Status = "400";
                    }
                }
                else
                {
                    TradeAnalysisDto.FileName = (await _tradeanalysisRepository.GetById(
                                request.TradeAnalysisFormDto.Id)).FileName;
                }

                var updateData = await _tradeanalysisRepository.GetById(request.TradeAnalysisFormDto.Id);

                _mapper.Map(TradeAnalysisDto, updateData);

                var data = await _tradeanalysisRepository.Update(updateData);

                response.Data = _mapper.Map<TradeAnalysisDto>(data);
                response.Success = true;
                response.Message = "Updated Successfull";
                response.Status = "200";
            }
            return response;
        }
    }
}