using ECX.Website.Application.Contracts.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECX.Website.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MCRController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMCRRepository _mcrRepository;
        private readonly IRealTimeDataRepository _realTimeDataRepository;


        public MCRController(IMediator mediator, IMCRRepository mcrRepository, IRealTimeDataRepository realTimeDataRepository)
        {
            _mediator = mediator;
            _mcrRepository = mcrRepository;
            _realTimeDataRepository = realTimeDataRepository;
        }

        [HttpGet("grn/{id}/{grn}")]
        public IActionResult GetGRNStatus(string id, string grn)
        {
            return Ok(_mcrRepository.GetGRNStatus(id, grn));
        }

        [HttpGet("member/{id}")]
        public IActionResult GetMemberClientList(string id)
        {
            return Ok(_mcrRepository.GetMemberClientList(id));
        }

        [HttpGet("whrbygrn/{grn}")]
        public IActionResult GetWHRStatusbyGRN(string grn)
        {
            return Ok(_mcrRepository.GetWHRStatusbyGRN(grn));
        }

        [HttpGet("tradebywhr/{whr}")]
        public IActionResult GetTradeStatusbyWHR(string whr)
        {
            return Ok(_mcrRepository.GetTradeStatusbyWHR(whr));
        }




    }
}
