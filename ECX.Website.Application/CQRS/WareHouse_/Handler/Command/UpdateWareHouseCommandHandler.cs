using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.WareHouse_.Request.Command;
using ECX.Website.Application.DTOs.WareHouse;
using ECX.Website.Application.DTOs.WareHouse.Validators;
using ECX.Website.Application.DTOs.Common.Validators;
using ECX.Website.Application.Exceptions;
using ECX.Website.Application.Response;
using ECX.Website.Domain;
using MediatR;

namespace ECX.Website.Application.CQRS.WareHouse_.Handler.Command
{
    public class UpdateWareHouseCommandHandler : IRequestHandler<UpdateWareHouseCommand, BaseCommonResponse>
    {
        private IWareHouseRepository _wareHouseRepository;
        private IMapper _mapper;
        public UpdateWareHouseCommandHandler(IWareHouseRepository wareHouseRepository, IMapper mapper)
        {
            _wareHouseRepository = wareHouseRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommonResponse> Handle(UpdateWareHouseCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommonResponse();
            var validator = new WareHouseUpdateDtoValidator();
            var validationResult = await validator.ValidateAsync(request.WareHouseFormDto);
            var WareHouseDto = _mapper.Map<WareHouseDto>(request.WareHouseFormDto);
            var flag = await _wareHouseRepository.Exists(request.WareHouseFormDto.Id);

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
                            nameof(WareHouse), request.WareHouseFormDto.Id).Message.ToString();
                response.Status = "404";
            }
            else 
            {
                if (request.WareHouseFormDto.ImgFile != null)
                {
                    try
                    {
                    
                            var oldImage = (await _wareHouseRepository.GetById(
                                request.WareHouseFormDto.Id)).ImgName;
                            

                            string oldPath = Path.Combine(
                                Directory.GetCurrentDirectory(), @"wwwroot\image",oldImage);
                            File.Delete(oldPath);

                     

                           
                            WareHouseDto.ImgName = "fileName";
                        
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
                    WareHouseDto.ImgName = (await _wareHouseRepository.GetById(
                                request.WareHouseFormDto.Id)).ImgName;
                } 

                var updateData = await _wareHouseRepository.GetById(request.WareHouseFormDto.Id);
                
                _mapper.Map(WareHouseDto, updateData);

                var data = await _wareHouseRepository.Update(updateData);

                response.Data = _mapper.Map<WareHouseDto>(data);
                response.Success = true;
                response.Message = "Updated Successfull";
                response.Status = "200";
            }
            return response;
        }
    }
 }

