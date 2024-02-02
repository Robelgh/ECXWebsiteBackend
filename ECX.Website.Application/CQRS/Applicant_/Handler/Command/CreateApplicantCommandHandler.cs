﻿using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.Applicant_.Request.Command;
using ECX.Website.Application.DTOs.Applicant;
using ECX.Website.Application.DTOs.Applicant.Validators;
using ECX.Website.Application.Exceptions;

using ECX.Website.Application.Response;
using ECX.Website.Domain;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using ECX.Website.Application.DTOs.Common.Validators;

namespace ECX.Website.Application.CQRS.Applicant_.Handler.Command
{
    public class CreateApplicantCommandHandler : IRequestHandler<CreateApplicantCommand, BaseCommonResponse>
    {
        private IApplicantRepository _applicantRepository;
        private IMapper _mapper;
        
        public CreateApplicantCommandHandler(IApplicantRepository applicantRepository, IMapper mapper)
        {
            _applicantRepository = applicantRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommonResponse> Handle(CreateApplicantCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommonResponse();
            var validator = new ApplicantCreateDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ApplicantFormDto);

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
                    var pdfValidationResult = await pdfValidator.ValidateAsync(request.ApplicantFormDto.File);

                    if (pdfValidationResult.IsValid == false)
                    {
                        response.Success = false;
                        response.Message = "Creation Faild";
                        response.Errors = pdfValidationResult.Errors.Select(x => x.ErrorMessage).ToList();
                        response.Status = "400";
                    }
                    else
                    {
                        string contentType = request.ApplicantFormDto.File.ContentType.ToString();
                        string ext = contentType.Split('/')[1];
                        string fileName = Guid.NewGuid().ToString() +"."+ext;
                        string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\pdf", fileName);

                        using (Stream stream = new FileStream(path, FileMode.Create))
                        {
                            request.ApplicantFormDto.File.CopyTo(stream);
                        }
                        var ApplicantDto = _mapper.Map<ApplicantDto>(request.ApplicantFormDto);
                        ApplicantDto.FileName = fileName;

                        Guid applicantId ;
                        bool flag = true;

                        while (true)
                        {
                            applicantId = (Guid.NewGuid());
                            flag = await _applicantRepository.Exists(applicantId);
                            if (flag == false)
                            {
                                ApplicantDto.Id = applicantId;
                                break;
                            }
                        }

                        var data =_mapper.Map<Applicant>(ApplicantDto);
                        
                        var saveData = await _applicantRepository.Add(data);

                        response.Data = _mapper.Map<ApplicantDto>(saveData);
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
