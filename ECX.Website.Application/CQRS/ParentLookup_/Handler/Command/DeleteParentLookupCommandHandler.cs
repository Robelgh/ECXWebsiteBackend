using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.PageCatagory_.Request.Command;
using ECX.Website.Application.CQRS.ParentLookup_.Request.Command;
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

namespace ECX.Website.Application.CQRS.ParentLookup_.Handler.Command
{
    public class DeleteParentLookupCommandHandler : IRequestHandler<DeleteParentLookupCommand, BaseCommonResponse>
    {

        private IParentLookupRepository _parentlookupRepository;
        private IMapper _mapper;
        public DeleteParentLookupCommandHandler(IParentLookupRepository parentlookupRepository, IMapper mapper)
        {
            _parentlookupRepository = parentlookupRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommonResponse> Handle(DeleteParentLookupCommand request, CancellationToken cancellationToken)
        {
            var data = await _parentlookupRepository.GetById(request.Id);
            var response = new BaseCommonResponse();

            if (data == null)
            {
                response.Success = false;
                response.Message = new NotFoundException(
                            nameof(ParentLookup), request.Id).Message.ToString();
                response.Status = "404";
            }
            else
            {
                await _parentlookupRepository.Delete(data);

                string path = Path.Combine(
                    Directory.GetCurrentDirectory(), @"wwwroot\image", data.ImgName);

                File.Delete(path);

                response.Success = true;
                response.Message = "Successfully Deleted";
                response.Status = "200";

            }

            return response;
        }
    }
}
