using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.DTOs.Fact.Validators
{
    public class FactCreateDtoValidator : AbstractValidator<FactFormDto>
    {
        public FactCreateDtoValidator()
        {

            RuleFor(p => p.LangId)
                .NotEmpty().WithMessage("{PropertyName} is requiered.")
                .NotNull();
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is requiered.")
                .NotNull();
            RuleFor(p => p.Amount)
               .NotEmpty().WithMessage("{PropertyName} is requiered.")
               .NotNull();
       
        }
    }
}
