﻿using ECX.Website.Application.DTOs.Applicant;
using ECX.Website.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.CQRS.Applicant_.Request.Queries
{
    public class GetApplicantDetailRequest :IRequest<BaseCommonResponse>
    {
        public Guid Id { get; set; }
    }
}
