using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.DTOs.ParentLookup.Validators
{
    public class ParentLookupDtoValidator : AbstractValidator<ParentLookupFormDto>
    {
        public ParentLookupDtoValidator()
        {
            RuleFor(p=>p.Title)
                .NotEmpty().WithMessage("{PropertyName} is requiered.")
                .NotNull();
            RuleFor(p => p.Description)
               .NotEmpty().WithMessage("{PropertyName} is requiered.")
               .NotNull();
        }
    }
}
