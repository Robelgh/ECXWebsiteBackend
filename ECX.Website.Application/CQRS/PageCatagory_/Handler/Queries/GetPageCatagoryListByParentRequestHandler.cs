using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.PageCatagory_.Request.Queries;
using ECX.Website.Application.DTOs.PageCatagory;
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

namespace ECX.Website.Application.CQRS.PageCatagory_.Handler.Queries
{
    public class GetPageCatagoryListByParentRequestHandler : IRequestHandler<GetPageCatagoryListByParentRequest, BaseCommonResponse>
    {
        private IPageCatagoryRepository _pageCatagoryRepository;
        private IMapper _mapper;

        public GetPageCatagoryListByParentRequestHandler(IPageCatagoryRepository pageCatagoryRepository, IMapper mapper)
        {
            _pageCatagoryRepository = pageCatagoryRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommonResponse> Handle(GetPageCatagoryListByParentRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseCommonResponse();
            var data = await _pageCatagoryRepository.GetCatagoryByParentId(request.Id);

            if (data != null)
            {
                response.Success = true;
                response.Data = _mapper.Map<List<PageCatagoryDto>>(data);
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
