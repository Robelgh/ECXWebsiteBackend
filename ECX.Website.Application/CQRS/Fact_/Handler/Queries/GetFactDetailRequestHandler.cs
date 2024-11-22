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
    public class GetFactDetailRequestHandler : IRequestHandler<GetFactDetailRequest, BaseCommonResponse>
    {
        private IFactRepository _FactRepository;
        private IMapper _mapper;
        
        public GetFactDetailRequestHandler(IFactRepository FactRepository, IMapper mapper)
        {
            _FactRepository = FactRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommonResponse> Handle(GetFactDetailRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseCommonResponse();
            var data = await _FactRepository.GetById(request.Id);
            if (data != null)
            {
                response.Success = true;
                response.Data = _mapper.Map<FactDto>(data);
                response.Status = "200";
            }
            else
            {
                response.Success = false;
                response.Message = new NotFoundException(
                          nameof(Facts), request.Id).Message.ToString();
                response.Status = "404";
            }
            return response;
        }
    }
}
