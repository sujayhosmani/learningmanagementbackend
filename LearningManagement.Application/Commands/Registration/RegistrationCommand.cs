using LearningManagement.Common.ResponseInterceptor;
using LearningManagement.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.Application.Commands.Registration
{
    public class RegistrationCommand : IRequest<ValidatableResponse<UserDto>>
    {
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }

    }
}
