using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.PageCatagory_.Request.Command;
using ECX.Website.Application.DTOs.Common.Validators;
using ECX.Website.Application.DTOs.PageCatagory.Validators;
using ECX.Website.Application.DTOs.PageCatagory;
using ECX.Website.Application.Exceptions;
using ECX.Website.Application.Response;
using ECX.Website.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECX.Website.Application.CQRS.ParentLookup_.Request.Command;
using ECX.Website.Domain.Lookup;
using ECX.Website.Application.DTOs.ParentLookup.Validators;
using ECX.Website.Application.DTOs.ParentLookup;

namespace ECX.Website.Application.CQRS.ParentLookup_.Handler.Command
{
    public class UpdateParentLookupCommandHandler : IRequestHandler<UpdateParentLookupCommand, BaseCommonResponse>
    {
        private IParentLookupRepository _parentLookupRepository;
        private IMapper _mapper;
        public UpdateParentLookupCommandHandler(IParentLookupRepository parentlookupRepository, IMapper mapper)
        {
            _parentLookupRepository = parentlookupRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommonResponse> Handle(UpdateParentLookupCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommonResponse();
            var validator = new ParentLookupDtoUpdateValidator();
            var validationResult = await validator.ValidateAsync(request.ParentLookupFormDto);
            var ParentlookupDto = _mapper.Map<ParentLookupDto>(request.ParentLookupFormDto);
            var flag = await _parentLookupRepository.Exists(request.ParentLookupFormDto.Id);

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
                            nameof(Page), request.ParentLookupFormDto.Id).Message.ToString();
                response.Status = "404";
            }
            else
            {
                if (request.ParentLookupFormDto.ImgFile != null)
                {
                    try
                    {
                        var imageValidator = new ImageValidator();
                        var imgValidationResult = await imageValidator.ValidateAsync(request.ParentLookupFormDto.ImgFile);

                        if (imgValidationResult.IsValid == false)
                        {
                            response.Success = false;
                            response.Message = "Update Failed";
                            response.Errors = imgValidationResult.Errors.Select(x => x.ErrorMessage).ToList();
                            response.Status = "400";
                        }
                        else
                        {
                            var oldImage = (await _parentLookupRepository.GetById(
                                request.ParentLookupFormDto.Id)).ImgName;


                            string oldPath = Path.Combine(
                                Directory.GetCurrentDirectory(), @"wwwroot\image", oldImage);
                            File.Delete(oldPath);

                            string contentType = request.ParentLookupFormDto.ImgFile.ContentType.ToString();
                            string ext = contentType.Split('/')[1];
                            string fileName = Guid.NewGuid().ToString() + "." + ext;
                            string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\image", fileName);

                            using (Stream stream = new FileStream(path, FileMode.Create))
                            {
                                request.ParentLookupFormDto.ImgFile.CopyTo(stream);
                            }

                            ParentlookupDto.ImgName = fileName;
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
                    ParentlookupDto.ImgName = (await _parentLookupRepository.GetById(
                                request.ParentLookupFormDto.Id)).ImgName;
                }

                var updateData = await _parentLookupRepository.GetById(request.ParentLookupFormDto.Id);

                _mapper.Map(ParentlookupDto, updateData);

                var data = await _parentLookupRepository.Update(updateData);

                response.Data = _mapper.Map<ParentLookupDto>(data);
                response.Success = true;
                response.Message = "Updated Successfull";
                response.Status = "200";
            }
            return response;
        }

    }
}
