using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.PageCatagory_.Request.Queries;
using ECX.Website.Application.CQRS.ParentLookup_.Request.Queries;
using ECX.Website.Application.DTOs.PageCatagory;
using ECX.Website.Application.DTOs.ParentLookup;
using ECX.Website.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.CQRS.ParentLookup_.Handler.Queries
{
    public class GetListParentLookupRequestHandler : IRequestHandler<GetParentLookupListRequest, BaseCommonResponse>
    {
        private IParentLookupRepository _parentLookupRepository;
        private IMapper _mapper;
        public GetListParentLookupRequestHandler(IParentLookupRepository parentlookupRepository, IMapper mapper)
        {
            _parentLookupRepository = parentlookupRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommonResponse> Handle(GetParentLookupListRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseCommonResponse();
            var data = await _parentLookupRepository.GetAll();

            response.Success = true;
            response.Data = _mapper.Map<List<ParentLookupDto>>(data);
            response.Status = "200";

            return response;
        }
    }
}
