using AutoMapper;
using ECX.Website.Application.CQRS.Account_.Request.Command;
using ECX.Website.Application.CQRS.Account_.Request.Queries;
using ECX.Website.Application.DTOs.Account;
using ECX.Website.Application.Response;
using ECX.Website.Domain;
using ECX.Website.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECX.Website.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        //private readonly UserManager<Account> _userManager;
        //private readonly IMapper _mapper;
        //private readonly JWTHandler _jwtHandler;
        private readonly IMediator _mediator;

        //public AccountController(UserManager<Account> userManager, IMapper mapper, JWTHandler jwtHandler)
        //{
        //    _userManager = userManager;
        //    _mapper = mapper;
        //    _jwtHandler = jwtHandler;
        //}

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]

        public async Task<ActionResult<ResponseAccount>> Post([FromForm] RegisterDto data)
        {
            var command = new CreateAccountCommand { RegisterDto = data };
            ResponseAccount response = await _mediator.Send(command);
            switch (response.Status)
            {
                case "200": return Ok(response);
                case "400": return BadRequest(response);
                case "404": return NotFound(response);
                default: return response;

            }

            //if (data == null || !ModelState.IsValid)
            //    return BadRequest();

            //var Account = _mapper.Map<Account>(data);
            //var result = await _userManager.CreateAsync(Account, data.Password);
            //if (!result.Succeeded)
            //{
            //    var errors = result.Errors.Select(e => e.Description);

            //    return BadRequest();
            //}



            //return StatusCode(201);
        }

        [HttpPost]

        [Route("login")]

        public async Task<ActionResult<ResponseAccount>> Login([FromForm] loginDto data)
        {
            var command = new LoginAccountCommand { loginDto = data };
            ResponseAccount response = await _mediator.Send(command);
            switch (response.Status)
            {
                case "200": return Ok(response);
                case "400": return BadRequest(response);
                case "404": return NotFound(response);
                default: return response;

            }
        }



        //public AccountController(IMediator mediator)
        //{
        //    _mediator = mediator;
        //}

        //// GET: api/<AccountController>
        //[HttpGet]
        //public async Task<ActionResult<BaseCommonResponse>> Get()
        //{
        //    var query = new GetAccountListRequest();
        //    BaseCommonResponse response = await _mediator.Send(query);
        //    switch(response.Status){
        //        case "200" : return Ok(response);
        //        case "400" : return BadRequest(response);
        //        case "404" : return NotFound(response);
        //        default : return response;  
        //    }
        //}

        //// GET api/<AccountController>/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<BaseCommonResponse>> Get(Guid id)
        //{
        //    var query = new GetAccountDetailRequest { Id = id };
        //    BaseCommonResponse response = await _mediator.Send(query);
        //    switch(response.Status){
        //        case "200" : return Ok(response);
        //        case "400" : return BadRequest(response);
        //        case "404" : return NotFound(response);
        //        default : return response;  
        //    }
        //}

        //// POST api/<AccountController>
        //[HttpPost]
        //public async Task<ActionResult<BaseCommonResponse>> Post([FromForm] AccountFormDto data)
        //{
        //    var command = new CreateAccountCommand { AccountFormDto = data };
        //    BaseCommonResponse response = await _mediator.Send(command);
        //    switch(response.Status){
        //        case "200" : return Ok(response);
        //        case "400" : return BadRequest(response);
        //        case "404" : return NotFound(response);
        //        default : return response;

        //    }

        //}

        //// PUT api/<AccountController>/5
        //[HttpPut]
        //public async Task<ActionResult<BaseCommonResponse>> Put([FromForm] AccountFormDto data)
        //{
        //    var command = new UpdateAccountCommand { AccountFormDto = data};
        //    BaseCommonResponse response = await _mediator.Send(command);
        //    switch(response.Status){
        //        case "200" : return Ok(response);
        //        case "400" : return BadRequest(response);
        //        case "404" : return NotFound(response);
        //        default : return response;

        //    }
        //}

        //// DELETE api/<AccountController>/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<BaseCommonResponse>> Delete(Guid id)
        //{
        //    var command = new DeleteAccountCommand { Id = id };
        //    BaseCommonResponse response = await _mediator.Send(command);
        //    switch(response.Status){
        //        case "200" : return Ok(response);
        //        case "400" : return BadRequest(response);
        //        case "404" : return NotFound(response);
        //        default : return response;

        //    }
        //}
    }
}