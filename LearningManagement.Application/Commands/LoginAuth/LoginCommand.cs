using LearningManagement.Common.ResponseInterceptor;
using LearningManagement.Domain.DTOs;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace LearningManagement.Application.Commands.LoginAuth
{
    public class LoginCommand : IRequest<ValidatableResponse<UserDto>>
    {
        [Required]
        public string EmailId { get; set; } 
        public string Password { get; set; } = string.Empty;

    }
}
