using ECX.Website.Application.DTOs.Account;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.DTOs.Email.Validators
{
    public class SendEmailDtoValidator : AbstractValidator<SendEmailDto>
    {

        public SendEmailDtoValidator()
        {
            RuleFor(p => p.To)
             .NotEmpty().WithMessage("{PropertyName} is requiered.")
             .NotNull();
            RuleFor(p => p.Subject)
                .NotEmpty().WithMessage("{PropertyName} is requiered.")
                .NotNull();
            RuleFor(p => p.Body)
               .NotEmpty().WithMessage("{PropertyName} is requiered.")
               .NotNull();


        }
    }
}
