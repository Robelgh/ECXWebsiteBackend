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
    public class LoginAccountCommandHandler : IRequestHandler<LoginAccountCommand, ResponseAccount>
    {
        private IAccountRepository _accountRepository;
        private IMapper _mapper;

        public LoginAccountCommandHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<ResponseAccount> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseAccount();
            var validator = new AccountCreateDtoValidator();

            if (request.LoginADDto == null)
                throw new NullReferenceException("Reigster form is null");

         
                try
                {
                  
                    var result = await _accountRepository.AutenticateUser(request.LoginADDto.UserName , request.LoginADDto.Password);

                    if (result.Success)
                    {
                        response.Success = true;
                        response.Message = result.Message;
                        response.UserName= result.UserName;
                        response.Token = result.Token; 
                        response.Status = "200";
                        ;
                    }
                    else
                    {
                        response.Message = result.Message;
                        response.Success = false;
                        response.Errors = result.Errors;
                    }



                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = "Creation Failed";
                    response.Errors = new List<string> { ex.Message };
                    response.Status = "400";
                }
            
            return response;
        }

    }
}
