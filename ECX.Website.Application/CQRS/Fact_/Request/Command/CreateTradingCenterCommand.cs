﻿using ECX.Website.Application.DTOs.Fact;
using ECX.Website.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.CQRS.Fact_.Request.Command
{
    public class CreateFactCommand : IRequest<BaseCommonResponse>
    {
        public FactFormDto FactFormDto { get; set; }
    }
}
