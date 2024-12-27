using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.Account_.Request.Command;
using ECX.Website.Application.Response;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.CQRS.Account_.Handler.Command
{
    public class ADLoginAccountCommandHandler : IRequestHandler<ADLoginAccountCommand, AuthenticationCommandResponse>
    {

        private IAccountRepository _accRepository;
        private IMapper _mapper;
        private readonly IConfiguration _configuration;


        public ADLoginAccountCommandHandler(IAccountRepository accRepository, IConfiguration configration, IMapper mapper)
        {
            _accRepository = accRepository;
            _configuration = configration;
            _mapper = mapper;
        }

        public async Task<AuthenticationCommandResponse> Handle(ADLoginAccountCommand request, CancellationToken cancellationToken)
        {

            var response = await _accRepository.AuthenticateUser(request.ADloginDto.UserName, request.ADloginDto.Password);
            return response;

        }
    }

}
