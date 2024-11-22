using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.Page_.Request.Queries;
using ECX.Website.Application.CQRS.PageCatagory_.Request.Queries;
using ECX.Website.Application.DTOs.Page;
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

namespace ECX.Website.Application.CQRS.Page_.Handler.Queries
{
    public class GetPageListByPageCatagoryRequestHandler : IRequestHandler<GetPageListByPageCatagoryRequest, BaseCommonResponse>
    {
        private IPageRepository _pageRepository;
        private IMapper _mapper;
        public GetPageListByPageCatagoryRequestHandler(IPageRepository pageRepository, IMapper mapper)
        {
            _pageRepository = pageRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommonResponse> Handle(GetPageListByPageCatagoryRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseCommonResponse();
            var data = await _pageRepository.GetPageByPageCatagoryId(request.Id);

            if (data != null)
            {
                response.Success = true;
                response.Data = _mapper.Map<List<PageDto>>(data);
                response.Status = "200";
            }
            else
            {
                response.Success = false;
                response.Message = new NotFoundException(
                    nameof(Page), request.Id).Message.ToString();
                response.Status = "404";
            }
            return response;

        }
    }
}
