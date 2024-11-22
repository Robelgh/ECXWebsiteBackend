using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.PageCatagory_.Request.Command;
using ECX.Website.Application.CQRS.ParentLookup_.Request.Command;
using ECX.Website.Application.DTOs.Common.Validators;
using ECX.Website.Application.DTOs.PageCatagory.Validators;
using ECX.Website.Application.DTOs.PageCatagory;
using ECX.Website.Application.Response;
using ECX.Website.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ECX.Website.Domain.Lookup;
using ECX.Website.Application.DTOs.ParentLookup.Validators;
using ECX.Website.Application.DTOs.ParentLookup;

namespace ECX.Website.Application.CQRS.ParentLookup_.Handler.Command
{
    public class CreateParentLookupCommandHandler : IRequestHandler<CreateParentLookupCommand, BaseCommonResponse>
    {

        private IParentLookupRepository _parentlookupRepository;
        private IMapper _mapper;

        public CreateParentLookupCommandHandler(IParentLookupRepository parentlookupRepository, IMapper mapper)
        {
            _parentlookupRepository = parentlookupRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommonResponse> Handle(CreateParentLookupCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommonResponse();
            var validator = new ParentLookupDtoCreateValidator();
            var validationResult = await validator.ValidateAsync(request.ParentLookupFormDto);

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
                    var imageValidator = new ImageValidator();
                    var imgValidationResult = await imageValidator.ValidateAsync(request.ParentLookupFormDto.ImgFile);

                    if (imgValidationResult.IsValid == false)
                    {
                        response.Success = false;
                        response.Message = "Creation Faild";
                        response.Errors = imgValidationResult.Errors.Select(x => x.ErrorMessage).ToList();
                        response.Status = "400";
                    }
                    else
                    {
                        string contentType = request.ParentLookupFormDto.ImgFile.ContentType.ToString();
                        string ext = contentType.Split('/')[1];
                        string fileName = Guid.NewGuid().ToString() + "." + ext;
                        string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\image", fileName);

                        using (Stream stream = new FileStream(path, FileMode.Create))
                        {
                            request.ParentLookupFormDto.ImgFile.CopyTo(stream);
                        }
                        var ParentlookupDto = _mapper.Map<ParentLookupDto>(request.ParentLookupFormDto);
                        ParentlookupDto.ImgName = fileName;

                        Guid pageId;
                        bool flag = true;

                        while (true)
                        {
                            pageId = (Guid.NewGuid());
                            flag = await _parentlookupRepository.Exists(pageId);
                            if (flag == false)
                            {
                                ParentlookupDto.Id = pageId;
                                break;
                            }
                        }

                        var data = _mapper.Map<ParentLookup>(ParentlookupDto);

                        var saveData = await _parentlookupRepository.Add(data);

                        response.Data = _mapper.Map<ParentLookupDto>(saveData);
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
