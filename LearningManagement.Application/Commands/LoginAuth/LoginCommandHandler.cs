using AutoMapper;
using LearningManagement.Common.Identity;
using LearningManagement.Common.interfaces;
using LearningManagement.Common.ResponseInterceptor;
using LearningManagement.Domain.DTOs;
using LearningManagement.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace LearningManagement.Application.Commands.LoginAuth
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ValidatableResponse<UserDto>>
    {
        
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _conf;
        private readonly IMapper _mapper;
        private const string TAG = "LoginCommandHandler";
        private readonly ILogger<LoginCommandHandler> _logger;

        public LoginCommandHandler(IUserRepository userRepository, IConfiguration conf, IMapper mapper, ILogger<LoginCommandHandler> logger)
        {
            _userRepo = userRepository;
            _conf = conf;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ValidatableResponse<UserDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User user = await _userRepo.GetUserByEmailId(request.EmailId, cancellationToken);

                if (user == null)
                {
                    _logger.LogWarning("{TAG} user Not found for user {EmailId}", TAG, request.EmailId);
                    return new ValidatableResponse<UserDto>("User not found", null, null, StatusCodes.Status404NotFound);
                }

                if (!user.Password.Equals(request.Password))
                {
                    _logger.LogWarning("{TAG} wrong credentials for user {EmailId}", TAG, request.EmailId);
                    return new ValidatableResponse<UserDto>("Wrong credentials", null, null, StatusCodes.Status400BadRequest);
                }
                GenerateTokenHandler generateToken = new GenerateTokenHandler(_conf);
                UserDto userDto = _mapper.Map<UserDto>(user);
                userDto.Token = generateToken.GenerateToken(user.Name, user.Role);
                _logger.LogInformation("{TAG} user logged in {EmailId}", TAG, userDto.EmailId);
                return new ValidatableResponse<UserDto>("login successful", null, userDto, StatusCodes.Status200OK);
            }
            catch(Exception ex)
            {
                _logger.LogError("{TAG} exception occured {message}", TAG, ex.Message);
                return new ValidatableResponse<UserDto>(ex.Message, ex.Message.ToString(), StatusCodes.Status500InternalServerError);
            }
            

        }
    }
}
