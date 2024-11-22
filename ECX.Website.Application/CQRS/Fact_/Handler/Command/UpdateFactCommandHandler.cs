using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.Fact_.Request.Command;
using ECX.Website.Application.DTOs.Fact;
using ECX.Website.Application.DTOs.Fact.Validators;
using ECX.Website.Application.DTOs.Common.Validators;
using ECX.Website.Application.Exceptions;
using ECX.Website.Application.Response;
using ECX.Website.Domain;
using MediatR;

namespace ECX.Website.Application.CQRS.Fact_.Handler.Command
{
    public class UpdateFactCommandHandler : IRequestHandler<UpdateFactCommand, BaseCommonResponse>
    {
        private IFactRepository _FactRepository;
        private IMapper _mapper;
        public UpdateFactCommandHandler(IFactRepository FactRepository, IMapper mapper)
        {
            _FactRepository = FactRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommonResponse> Handle(UpdateFactCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommonResponse();
            var validator = new FactUpdateDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FactFormDto);
            var FactDto = _mapper.Map<FactDto>(request.FactFormDto);
            var flag = await _FactRepository.Exists(request.FactFormDto.Id);

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
                            nameof(Facts), request.FactFormDto.Id).Message.ToString();
                response.Status = "404";
            }
            else 
            {
             

                var updateData = await _FactRepository.GetById(request.FactFormDto.Id);
                
                _mapper.Map(FactDto, updateData);

                var data = await _FactRepository.Update(updateData);

                response.Data = _mapper.Map<FactDto>(data);
                response.Success = true;
                response.Message = "Updated Successfull";
                response.Status = "200";
            }
            return response;
        }
    }
 }

