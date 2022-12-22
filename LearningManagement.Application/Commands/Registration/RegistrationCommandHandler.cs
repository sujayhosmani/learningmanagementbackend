using LearningManagement.Application.Commands.LoginAuth;
using LearningManagement.Common.interfaces;
using LearningManagement.Common.ResponseInterceptor;
using LearningManagement.Domain.DTOs;
using LearningManagement.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.Application.Commands.Registration
{
    public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, ValidatableResponse<UserDto>>
    {
        private readonly IUserRepository _userRepo;
        private readonly ILogger<RegistrationCommandHandler> _logger;
        public RegistrationCommandHandler(IUserRepository userRepository, ILogger<RegistrationCommandHandler> logger)
        {
            _userRepo = userRepository;
            _logger = logger;
        }

        public async Task<ValidatableResponse<UserDto>> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User user = new()
                {
                    Name = request.Name,
                    EmailId = request.EmailId,
                    Role = "user",
                    Password = request.Password,
                };
                UserDto userDto = await _userRepo.CreateUser(user, cancellationToken);
                _logger.LogInformation("RegistrationCommandHandler Registration completed for the user: {EmailId}", request.EmailId);
                return new ValidatableResponse<UserDto>("Added Successfully", null, userDto, StatusCodes.Status200OK);
            }
            catch(Exception ex)
            {
                _logger.LogError("RegistrationCommandHandler exception occured {EmailId}", request.EmailId);
                return new ValidatableResponse<UserDto>(ex.Message, ex.Message.ToString(), StatusCodes.Status500InternalServerError);
            }
            
        }
    }
}
