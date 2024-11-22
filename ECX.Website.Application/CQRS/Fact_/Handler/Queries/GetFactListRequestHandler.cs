using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.Fact_.Request.Command;
using ECX.Website.Application.DTOs.Fact;
using ECX.Website.Application.DTOs.Fact.Validators;
using ECX.Website.Application.Exceptions;

using ECX.Website.Application.Response;
using ECX.Website.Domain;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ECX.Website.Application.CQRS.Fact_.Request.Queries;

namespace ECX.Website.Application.CQRS.Fact_.Handler.Queries
{
    public class GetFactListRequestHandler : IRequestHandler<GetFactListRequest, BaseCommonResponse>
    {
        private IFactRepository _FactRepository;
        private IMapper _mapper;
        public GetFactListRequestHandler(IFactRepository FactRepository, IMapper mapper)
        {
            _FactRepository = FactRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommonResponse> Handle(GetFactListRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseCommonResponse();
            var data = await _FactRepository.GetAll();

            response.Success = true;
            response.Data = _mapper.Map<List<FactDto>>(data);
            response.Status = "200";

            return response;
        }
    }
}
