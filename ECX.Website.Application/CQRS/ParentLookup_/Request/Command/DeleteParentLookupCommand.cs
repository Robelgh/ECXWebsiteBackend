using ECX.Website.Application.DTOs.PageCatagory;
using ECX.Website.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.Website.Application.CQRS.ParentLookup_.Request.Command
{
    public class DeleteParentLookupCommand : IRequest<BaseCommonResponse>
    {
        public Guid Id { get; set; }
    }
}


