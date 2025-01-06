using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.SessionSchedule_.Request.Command;
using ECX.Website.Application.CQRS.SessionSchedule_.Request.Queries;
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
    public class DeleteSessionScheduleCommandHandler : IRequestHandler<DeleteSessionScheduleCommand, BaseCommonResponse>
    {
        private ISessionScheduleRepository _sessionScheduleRepository;
        private IMapper _mapper;

        public DeleteSessionScheduleCommandHandler(ISessionScheduleRepository sessionScheduleRepository, IMapper mapper)
        {
            _sessionScheduleRepository = sessionScheduleRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommonResponse> Handle(DeleteSessionScheduleCommand request, CancellationToken cancellationToken)
        {
            var data = await _sessionScheduleRepository.GetById(request.Id);
            var response = new BaseCommonResponse();

            if (data == null)
            {
                response.Success = false;
                response.Message = new NotFoundException(
                            nameof(Research), request.Id).Message.ToString();
                response.Status = "404";
            }
            else
            {
                await _sessionScheduleRepository.Delete(data);

                response.Success = true;
                response.Message = "Successfully Deleted";
                response.Status = "200";

            }

            return response;
        }

    }
}
