using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.DTOs.Account.Validators
{
    public class AccountCreateDtoValidator : AbstractValidator<RegisterDto>
    {
        public AccountCreateDtoValidator()
        {
            RuleFor(p => p.ConfirmPassword)
             .NotEmpty().WithMessage("{PropertyName} is requiered.")
             .NotNull();
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is requiered.")
                .NotNull();
            RuleFor(p => p.Password)
               .NotEmpty().WithMessage("{PropertyName} is requiered.")
               .NotNull();


        }
    }
}
