using LearningManagement.Application.Commands.LoginAuth;
using LearningManagement.Application.Commands.Registration;
using LearningManagement.Common.Constants;
using LearningManagement.Domain.DTOs;
using LearningManagement.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LearningManagement.Login.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}")]
    [Produces("application/json")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Constants.Login)]
        public async Task<ActionResult> Login([FromBody] LoginCommand user)
        {
            var response = await _mediator.Send(user);
            return response.ResponseData;
        }

        [HttpPost(Constants.Register)]
        public async Task<ActionResult> Register([FromBody] RegistrationCommand user)
        {
            var response = await _mediator.Send(user);
            return response.ResponseData;
        }

    }
}
