using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.DTOs.FeedBack.Validators
{
    public class FeedBackCreateDtoValidator : AbstractValidator<FeedBackDto>
    {
        public FeedBackCreateDtoValidator()
        {

            RuleFor(p => p.Topic)
                .NotEmpty().WithMessage("{PropertyName} is requiered.")
                .NotNull();
            RuleFor(p => p.Description)
               .NotEmpty().WithMessage("{PropertyName} is requiered.")
               .NotNull();
            
        }
    }
}
