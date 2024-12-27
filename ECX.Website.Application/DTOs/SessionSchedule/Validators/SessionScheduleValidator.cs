using ECX.Website.Application.DTOs.Research;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.DTOs.SessionSchedule.Validators
{
    public class SessionScheduleValidator : AbstractValidator<SessionScheduleDto>
    {
        public SessionScheduleValidator()
        {

            RuleFor(p => p.LangId)
                .NotEmpty().WithMessage("{PropertyName} is requiered.")
                .NotNull();
       
        }
    }
}
