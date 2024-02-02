﻿using ECX.Website.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.CQRS.TradingCenter_.Request.Command
{
    public class DeleteTradingCenterCommand : IRequest<BaseCommonResponse>
    {
        public Guid Id { get; set; }
    }
}
