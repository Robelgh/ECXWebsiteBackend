using ECX.Website.Application.DTOs.SocialMedia;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.DTOs.TradeAnalysis.Validators
{
    public class CreateTradeAnalysisDtoValidators : AbstractValidator<TradeAnalysisFormDto>
    {
        public CreateTradeAnalysisDtoValidators()
        {

            RuleFor(p => p.Name)
             .NotEmpty().WithMessage("{PropertyName} is requiered.")
             .NotNull();
            RuleFor(p => p.Summary)
                .NotEmpty().WithMessage("{PropertyName} is requiered.")
                .NotNull();
            RuleFor(p => p.Type)
             .NotEmpty().WithMessage("{PropertyName} is requiered.")
             .NotNull();
            RuleFor(p => p.Date)
                .NotEmpty().WithMessage("{PropertyName} is requiered.")
                .NotNull();
        
        }
    }
}
