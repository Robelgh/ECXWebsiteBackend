using AutoMapper;
using ECX.Website.Application.CQRS.Fact_.Request.Command;
using ECX.Website.Application.Exceptions;
using ECX.Website.Application.Contracts.Persistence;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECX.Website.Domain;
using ECX.Website.Application.Response;

namespace ECX.Website.Application.CQRS.Fact_.Handler.Command
{
    public class DeleteFactCommandHandler : IRequestHandler<DeleteFactCommand, BaseCommonResponse>
    {
        
        private IFactRepository _FactRepository;
        private IMapper _mapper;
        public DeleteFactCommandHandler(IFactRepository FactRepository, IMapper mapper)
        {
            _FactRepository = FactRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommonResponse> Handle(DeleteFactCommand request, CancellationToken cancellationToken)
        {
            var data = await _FactRepository.GetById(request.Id);
            var response = new BaseCommonResponse();

            if (data == null)
            {
                response.Success = false;
                response.Message = new NotFoundException(
                            nameof(Facts), request.Id).Message.ToString();
                response.Status = "404";
            }
            else
            {
                await _FactRepository.Delete(data);

       

                response.Success = true;
                response.Message = "Successfully Deleted";
                response.Status = "200";

            }
                
            return response;
        }
    }
}
