﻿using ECX.Website.Application.DTOs.Account;
using ECX.Website.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.CQRS.Account_.Request.Queries
{
    public class GetScrollingDataRequest :IRequest<BaseCommonResponse>
    {
        public Guid Id { get; set; }
    }
}
