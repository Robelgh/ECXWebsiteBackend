using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.SessionSchedule_.Request.Command;
using ECX.Website.Application.CQRS.SessionSchedule_.Request.Queries;
using ECX.Website.Application.DTOs.Common.Validators;
using ECX.Website.Application.DTOs.Research.Validators;
using ECX.Website.Application.DTOs.SessionSchedule;
using ECX.Website.Application.DTOs.SessionSchedule.Validators;
using ECX.Website.Application.Exceptions;
using ECX.Website.Application.Response;
using ECX.Website.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.CQRS.SessionSchedule_.Handler.Command
{
    public class UpdateSessionScheduleCommandHandler : IRequestHandler<updateSessionScheduleCommand, BaseCommonResponse>
    {
        private ISessionScheduleRepository _sessionScheduleRepository;
        private IMapper _mapper;

        public UpdateSessionScheduleCommandHandler(ISessionScheduleRepository sessionScheduleRepository, IMapper mapper)
        {
            _sessionScheduleRepository = sessionScheduleRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommonResponse> Handle(updateSessionScheduleCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommonResponse();
            var validator = new SessionScheduleValidator();
            var validationResult = await validator.ValidateAsync(request.SessionScheduleDto);
            var SessionScheduleDto = _mapper.Map<SessionScheduleDto>(request.SessionScheduleDto);
            var flag = await _sessionScheduleRepository.Exists(request.SessionScheduleDto.Id);

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
                            nameof(SessionSchedule), request.SessionScheduleDto.Id).Message.ToString();
                response.Status = "404";
            }
            else
            {
             
                var updateData = await _sessionScheduleRepository.GetById(request.SessionScheduleDto.Id);

                _mapper.Map(SessionScheduleDto, updateData);

                var data = await _sessionScheduleRepository.Update(updateData);

                response.Data = _mapper.Map<SessionScheduleDto>(data);
                response.Success = true;
                response.Message = "Updated Successfull";
                response.Status = "200";
            }
            return response;
        }

    }
}
