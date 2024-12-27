using ECX.Website.Application.DTOs.Research;
using ECX.Website.Application.DTOs.SessionSchedule;
using ECX.Website.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.CQRS.SessionSchedule_.Request.Queries
{
    public class GetSessionScheduleDetailRequest :IRequest<BaseCommonResponse>
    {
        public Guid Id { get; set; }

    }
}
