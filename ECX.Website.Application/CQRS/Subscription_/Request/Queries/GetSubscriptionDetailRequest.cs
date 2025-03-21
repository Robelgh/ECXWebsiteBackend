﻿using ECX.Website.Application.DTOs.Subscription;
using ECX.Website.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.CQRS.Subscription_.Request.Queries
{
    public class GetSubscriptionDetailRequest :IRequest<BaseCommonResponse>
    {
        public Guid Id { get; set; }
    }
}
