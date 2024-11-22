using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.Commodity_.Request.Queries;
using ECX.Website.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.CQRS.Commodity_.Handler.Queries
{
    public class GetCommodityLookupDetailRequestHandler : IRequestHandler<GetCommodityDetailRequest, BaseCommonResponse>
    {
        private ICommodityRepository _commodityRepository;
        private IMapper _mapper;

        public GetCommodityLookupDetailRequestHandler(ICommodityRepository commodityRepository, IMapper mapper)
        {
            _commodityRepository = commodityRepository;
            _mapper = mapper;
        }

        public Task<BaseCommonResponse> Handle(GetCommodityDetailRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
