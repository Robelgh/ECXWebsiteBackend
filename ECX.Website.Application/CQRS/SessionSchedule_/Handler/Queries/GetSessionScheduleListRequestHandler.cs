using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.Research_.Request.Queries;
using ECX.Website.Application.CQRS.SessionSchedule_.Request.Queries;
using ECX.Website.Application.DTOs.Research;
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
    public class GetSessionScheduleListRequestHandler : IRequestHandler<GetSessionScheduleListRequest, BaseCommonResponse>
    {
        private ISessionScheduleRepository _sessionScheduleRepository;
        private IMapper _mapper;

        public GetSessionScheduleListRequestHandler(ISessionScheduleRepository sessionScheduleRepository, IMapper mapper)
        {
            _sessionScheduleRepository = sessionScheduleRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommonResponse> Handle(GetSessionScheduleListRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseCommonResponse();
            var data = await _sessionScheduleRepository.GetAll();

            response.Success = true;
            response.Data = _mapper.Map<List<SessionSchedule>>(data);
            response.Status = "200";

            return response;
        }

    }
}