using AutoMapper;
using ECX.Website.Application.Contracts.Persistence;
using ECX.Website.Application.CQRS.Account_.Request.Command;
using ECX.Website.Application.DTOs.Account;
using ECX.Website.Application.DTOs.Account.Validators;
using ECX.Website.Application.Exceptions;

using ECX.Website.Application.Response;
using ECX.Website.Domain;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using ECX.Website.Application.DTOs.Common.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace ECX.Website.Application.CQRS.Account_.Handler.Command
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, ResponseAccount>
    {
        private IAccountRepository _accountRepository;
        private IMapper _mapper;

        public CreateAccountCommandHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }
        public async Task<ResponseAccount> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseAccount();
            var validator = new AccountCreateDtoValidator();

            if (request.RegisterDto == null)
                throw new NullReferenceException("Reigster form is null");

            else if (request.RegisterDto.Password != request.RegisterDto.ConfirmPassword)
                {
                response.Message = "Confirm password doesn't match the password";
                response.Success = false;
                response.Status = "403";
                
                };
            var validationResult = await validator.ValidateAsync(request.RegisterDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Faild";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                response.Status = "400";
            }
            else
            {
                try
                {
                    var Account = _mapper.Map<Account>(request.RegisterDto);
                    var result = await _accountRepository.RegisterUserAsync(Account , request.RegisterDto.Password);

                    if (result.Success)
                    {
                        response.Success = true;
                        response.Message = "Created Successfully";
                        response.Status = "200";
                        ;
                    }
                    else
                    {
                        response.Message = "User did not create";
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
            }
            return response;
        }
    }
}
