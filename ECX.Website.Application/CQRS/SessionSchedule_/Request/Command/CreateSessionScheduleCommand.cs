using ECX.Website.Application.DTOs.Research;
using ECX.Website.Application.DTOs.SessionSchedule;
using ECX.Website.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.CQRS.SessionSchedule_.Request.Command
{
    public class CreateSessionScheduleCommand : IRequest<BaseCommonResponse>
    {
        public SessionScheduleDto SessionScheduleDto { get; set; }
    }
}
