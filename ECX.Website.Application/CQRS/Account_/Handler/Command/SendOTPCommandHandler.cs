using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.Account_.Request.Command;
using ECX.Website.Application.DTOs.Account.Validators;
using ECX.Website.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.CQRS.Account_.Handler.Command
{
    public class SendOTPCommandHandler : IRequestHandler<SendOTPCommand, MiniOrangeResponse>
    {
        private IAccountRepository _accountRepository;
        private IMapper _mapper;

        public SendOTPCommandHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<MiniOrangeResponse> Handle(SendOTPCommand request, CancellationToken cancellationToken)
        {
           
            var validator = new AccountCreateDtoValidator();

            if (true)
                throw new NullReferenceException("Reigster form is null");



              //  return await _accountRepository.SendOTP(request.Method);


        }

    }
}
