using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.Application.Commands.LoginAuth
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(c => c.EmailId).NotEmpty().NotNull();
            RuleFor(c => c.Password).NotEmpty().NotNull();
        }
    }
}
