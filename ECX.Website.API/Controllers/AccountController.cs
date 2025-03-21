using AutoMapper;
using ECX.Website.Application.CQRS.Account_.Request.Command;
using ECX.Website.Application.CQRS.Account_.Request.Queries;
using ECX.Website.Application.DTOs.Account;
using ECX.Website.Application.Response;
using ECX.Website.Domain;
using ECX.Website.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ECX.Website.Application.Contracts.Persistence;
using System.Text.Json;

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
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountRepository _accountRepository;

        private readonly ExternalApiService _externalApiService;




        //public AccountController(ILogger<AccountController> logger)
        //{
        //    _logger = logger;
        //}

        public AccountController(IMediator mediator, ILogger<AccountController> logger , IAccountRepository accountRepository, ExternalApiService externalApiService)
        {
            _mediator = mediator;
            _logger = logger;
            _accountRepository = accountRepository;
            _externalApiService = externalApiService;
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

        //[HttpPost]

        //[Route("login")]

        //public async Task<ActionResult<ResponseAccount>> Login([FromForm] LoginADDto data)
        //{
        //    var command = new LoginAccountCommand { LoginADDto = data };
        //    ResponseAccount response = await _mediator.Send(command);


        //    switch (response.Status)
        //    {
        //        case "200": return Ok(response);
        //        case "400": return BadRequest(response);
        //        case "404": return NotFound(response);
        //        default: return response;

        //    }
        //}

        [HttpPost]

        [Route("login")]

        public async Task<ActionResult<ResponseAccount>> Login([FromForm] LoginADDto data)
        {
            // Assuming SSO URL is generated as part of the login request.
            string SSOUrl = $"{HttpContext.Request.Path}?ssoaction=login"; // Build SSO URL dynamically

            // Simulate or perform the SSO authentication here.
            var response = await AuthenticateWithSSO(SSOUrl, data);

            // Switch based on the response status
            switch (response.Status)
            {
                case "200":
                    return Ok(response); // Success response
                case "400":
                    return BadRequest(response); // Invalid request
                case "404":
                    return NotFound(response); // Not found, e.g., invalid credentials
                default:
                    return response; // Return response directly for other cases
            }
        }

        [HttpPost]

        [Route("AD/login")]

        public async Task<ActionResult<ResponseAccount>> LoginAD([FromForm] LoginADDto data)
        {
            var command = new LoginAccountCommand { LoginADDto = data };
            ResponseAccount response = await _mediator.Send(command);
            switch (response.Status)
            {
                case "200": return Ok(response);
                case "400": return BadRequest(response);
                case "404": return NotFound(response);
                default: return response;

            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { Message = "Logged out" });
        }


        [HttpPost]

        [Route("MCR/AD/login")]

        public async Task<ActionResult<ResponseAccount>> LoginMCRAD([FromForm] LoginADDto data)
        {
           // await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var command = new MCRLoginAccountCommand { LoginADDto = data };
            ResponseAccount response = await _mediator.Send(command);
            return await Task.FromResult(response);
        }
        private async Task<ResponseAccount> AuthenticateWithSSO(string ssoUrl, LoginADDto data)
        {
            ResponseAccount response = new ResponseAccount
            {
                Status = "200", // Assume success for demo
                Message = "Login successful"
            };

            return await Task.FromResult(response);
        }

        [Authorize(AuthenticationSchemes = "Identity.Application")]
        [HttpGet("check-session")]
        public IActionResult CheckSession()
        {
            return Ok(new { valid = true });
        }

        [Authorize(AuthenticationSchemes = "Identity.Application")]
        [HttpPost("SendOTP")]
        public async Task<ActionResult<ResponseAccount>> SendOTP([FromForm] MiniorangeDto data)
        {
            //var command = new SendOTPCommand { miniorangeDto = data };
            //MiniOrangeResponse response = await _accountRepository.SendOTP();

            try
            {
                // Call the external API
                var response = await _externalApiService.CallExternalApiAsync("https://mfa.ecx.com.et/api/auth/challenge", data);

                // Return the response from the external API
                return Ok(response);
            }
            catch (HttpRequestException ex)
            {
                // Handle errors from the external API
                return StatusCode(500, $"Error calling external API: {ex.Message}");
            }

            
        }

        [Authorize(AuthenticationSchemes = "Identity.Application")]
        [HttpPost("VerifyOTP")]
        public async Task<ActionResult<ResponseAccount>> VerifyOTP([FromBody] MiniorangeValidationDto data)
        {
            try
            {
                // Call the external API
                var response = await _externalApiService.CallExternalApiAsync("https://mfa.ecx.com.et/api/auth/validate", data);

                JsonDocument jsonDocument = JsonDocument.Parse(response.Value);
                JsonElement root=jsonDocument.RootElement;
                string status = root.GetProperty("status").GetString();
                

                if (status == "SUCCESS")
                {

                    string cookieValue = this.HttpContext.Request.Cookies[".AspNet.RaindropSharedTraderInterface.Cookie"];
                    return Ok(new ResponseAccount
                    {
                        Token = cookieValue,
                        Status = "success",
                        Message="Success",
                        Success=true
                    }) ;

                }

                // Return the response from the external API
                return Ok(new ResponseAccount
                {
                    Status = "Failed",
                    Message = "Failed",
                    Success = false
                });
            }
            catch (HttpRequestException ex)
            {
                // Handle errors from the external API
                return StatusCode(500, $"Error calling external API: {ex.Message}");
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