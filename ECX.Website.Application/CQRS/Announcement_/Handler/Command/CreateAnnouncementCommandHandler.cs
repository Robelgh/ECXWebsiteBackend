﻿using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.Announcement_.Request.Command;
using ECX.Website.Application.DTOs.Announcement;
using ECX.Website.Application.DTOs.Announcement.Validators;
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

namespace ECX.Website.Application.CQRS.Announcement_.Handler.Command
{
    public class CreateAnnouncementCommandHandler : IRequestHandler<CreateAnnouncementCommand, BaseCommonResponse>
    {
        private IAnnouncementRepository _announcementRepository;
        private IMapper _mapper;
        
        public CreateAnnouncementCommandHandler(IAnnouncementRepository announcementRepository, IMapper mapper)
        {
            _announcementRepository = announcementRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommonResponse> Handle(CreateAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommonResponse();
            var validator = new AnnouncementCreateDtoValidator();
            var validationResult = await validator.ValidateAsync(request.AnnouncementFormDto);

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
                    var pdfValidationResult = await pdfValidator.ValidateAsync(request.AnnouncementFormDto.File);

                    if (pdfValidationResult.IsValid == false)
                    {
                        response.Success = false;
                        response.Message = "Creation Faild";
                        response.Errors = pdfValidationResult.Errors.Select(x => x.ErrorMessage).ToList();
                        response.Status = "400";
                    }
                    else
                    {
                        string contentType = request.AnnouncementFormDto.File.ContentType.ToString();
                        string ext = contentType.Split('/')[1];
                        string fileName = Guid.NewGuid().ToString() +"."+ext;
                        string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\pdf", fileName);

                        using (Stream stream = new FileStream(path, FileMode.Create))
                        {
                            request.AnnouncementFormDto.File.CopyTo(stream);
                        }
                        var AnnouncementDto = _mapper.Map<AnnouncementDto>(request.AnnouncementFormDto);
                        AnnouncementDto.FileName = fileName;

                        Guid announcementId ;
                        bool flag = true;

                        while (true)
                        {
                            announcementId = Guid.NewGuid();
                            flag = await _announcementRepository.Exists(announcementId);
                            if (flag == false)
                            {
                                AnnouncementDto.Id = announcementId;
                                break;
                            }
                        }

                        var data =_mapper.Map<Announcement>(AnnouncementDto);
                        
                        var saveData = await _announcementRepository.Add(data);

                        response.Data = _mapper.Map<AnnouncementDto>(saveData);
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
