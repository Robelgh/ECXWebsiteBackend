using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.SessionSchedule_.Request.Command;
using ECX.Website.Application.CQRS.SessionSchedule_.Request.Queries;
using ECX.Website.Application.DTOs.Common.Validators;
using ECX.Website.Application.DTOs.Research.Validators;
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
    public class CreateSessionScheduleCommandHandler : IRequestHandler<CreateSessionScheduleCommand, BaseCommonResponse>
    {
        private ISessionScheduleRepository _sessionScheduleRepository;
        private IMapper _mapper;

        public CreateSessionScheduleCommandHandler(ISessionScheduleRepository sessionScheduleRepository, IMapper mapper)
        {
            _sessionScheduleRepository = sessionScheduleRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommonResponse> Handle(CreateSessionScheduleCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommonResponse();
            var validator = new SessionScheduleValidator();
            var validationResult = await validator.ValidateAsync(request.SessionScheduleDto);

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
                     var data = _mapper.Map<SessionSchedule>(request.SessionScheduleDto);

                     var saveData = await _sessionScheduleRepository.Add(data);

                      response.Data = _mapper.Map<SessionSchedule>(saveData);
                      response.Success = true;
                      response.Message = "Created Successfully";
                      response.Status = "200";
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
