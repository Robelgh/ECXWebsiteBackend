﻿using ECX.Website.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.CQRS.Faq_.Request.Command
{
    public class DeleteFaqCommand : IRequest<BaseCommonResponse>
    {
        public Guid Id { get; set; }
    }
}
