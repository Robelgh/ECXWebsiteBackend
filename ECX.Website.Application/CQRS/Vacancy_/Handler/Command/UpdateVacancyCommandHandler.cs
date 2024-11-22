using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.Vacancy_.Request.Command;
using ECX.Website.Application.DTOs.Vacancy;
using ECX.Website.Application.DTOs.Vacancy.Validators;
using ECX.Website.Application.DTOs.Common.Validators;
using ECX.Website.Application.Exceptions;
using ECX.Website.Application.Response;
using ECX.Website.Domain;
using MediatR;

namespace ECX.Website.Application.CQRS.Vacancy_.Handler.Command
{
    public class UpdateVacancyCommandHandler : IRequestHandler<UpdateVacancyCommand, BaseCommonResponse>
    {
        private IVacancyRepository _vacancyRepository;
        private IMapper _mapper;
        public UpdateVacancyCommandHandler(IVacancyRepository vacancyRepository, IMapper mapper)
        {
            _vacancyRepository = vacancyRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommonResponse> Handle(UpdateVacancyCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommonResponse();
            var validator = new VacancyUpdateDtoValidator();
            var validationResult = await validator.ValidateAsync(request.VacancyFormDto);
            var VacancyDto = _mapper.Map<VacancyDto>(request.VacancyFormDto);
            var flag = await _vacancyRepository.Exists(request.VacancyFormDto.Id);

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
                            nameof(Vacancy), request.VacancyFormDto.Id).Message.ToString();
                response.Status = "404";
            }
            else 
            {

                var updateData = await _vacancyRepository.GetById(request.VacancyFormDto.Id);
                
                _mapper.Map(VacancyDto, updateData);

                var data = await _vacancyRepository.Update(updateData);

                response.Data = _mapper.Map<VacancyDto>(data);
                response.Success = true;
                response.Message = "Updated Successfull";
                response.Status = "200";
            }
            return response;
        }
    }
 }

