using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.PageCatagory_.Request.Queries;
using ECX.Website.Application.CQRS.ParentLookup_.Request.Queries;
using ECX.Website.Application.DTOs.PageCatagory;
using ECX.Website.Application.DTOs.ParentLookup;
using ECX.Website.Application.Exceptions;
using ECX.Website.Application.Response;
using ECX.Website.Domain;
using ECX.Website.Domain.Lookup;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.CQRS.ParentLookup_.Handler.Queries
{
    public class GetDetailParentLookupRequestHandler : IRequestHandler<GetParentLookupDetailRequest, BaseCommonResponse>
    {
        private IParentLookupRepository _parentLookupRepository;
        private IMapper _mapper;

        public GetDetailParentLookupRequestHandler(IParentLookupRepository parentLookupRepository, IMapper mapper)
        {
            _parentLookupRepository = parentLookupRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommonResponse> Handle(GetParentLookupDetailRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseCommonResponse();
            var data = await _parentLookupRepository.GetById(request.Id);
            if (data != null)
            {
                response.Success = true;
                response.Data = _mapper.Map<ParentLookupDto>(data);
                response.Status = "200";
            }
            else
            {
                response.Success = false;
                response.Message = new NotFoundException(
                    nameof(ParentLookup), request.Id).Message.ToString();
                response.Status = "404";
            }
            return response;
        }
    }
}
