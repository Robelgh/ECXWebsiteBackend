using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.SessionSchedule_.Request.Queries;
using ECX.Website.Application.DTOs.Research;
using ECX.Website.Application.Exceptions;
using ECX.Website.Application.Response;
using ECX.Website.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.CQRS.SessionSchedule_.Handler.Queries
{
    public class GetSessionScheduleDetailRequestHandler : IRequestHandler<GetSessionScheduleDetailRequest, BaseCommonResponse>
    {
        private ISessionScheduleRepository _sessionScheduleRepository;
        private IMapper _mapper;

        public GetSessionScheduleDetailRequestHandler(ISessionScheduleRepository sessionScheduleRepository, IMapper mapper)
        {
            _sessionScheduleRepository = sessionScheduleRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommonResponse> Handle(GetSessionScheduleDetailRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseCommonResponse();
            var data = await _sessionScheduleRepository.GetById(request.Id);
            if (data != null)
            {
                response.Success = true;
                response.Data = _mapper.Map<SessionSchedule>(data);
                response.Status = "200";
            }
            else
            {
                response.Success = false;
                response.Message = new NotFoundException(
                          nameof(SessionSchedule), request.Id).Message.ToString();
                response.Status = "404";
            }
            return response;
        }

    }
}
