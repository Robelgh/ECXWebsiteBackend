using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.ContractFile_.Request.Queries;
using ECX.Website.Application.DTOs.ContractFile;
using ECX.Website.Application.DTOs.PageCatagory;
using ECX.Website.Application.Exceptions;
using ECX.Website.Application.Response;
using ECX.Website.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.CQRS.ContractFile_.Handler.Queries
{
    public class GetContractFileDetailByCommodityRequestHandler : IRequestHandler<GetContractFileDetailByCommodityRequest, BaseCommonResponse>
    {

        private IContractFileRepository _contractFileRepository;
        private IMapper _mapper;

        public GetContractFileDetailByCommodityRequestHandler(IContractFileRepository contractFileRepository, IMapper mapper)
        {
            _contractFileRepository = contractFileRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommonResponse> Handle(GetContractFileDetailByCommodityRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseCommonResponse();
            var data = await _contractFileRepository.GetContractByCommodityId(request.Id);

            if (data != null)
            {
                response.Success = true;
                response.Data = _mapper.Map<List<ContractFileDto>>(data);
                response.Status = "200";
            }
            else
            {
                response.Success = false;
                response.Message = new NotFoundException(
                    nameof(PageCatagory), request.Id).Message.ToString();
                response.Status = "404";
            }
            return response;
        }
    }
}
