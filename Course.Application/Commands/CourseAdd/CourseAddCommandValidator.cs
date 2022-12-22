using Course.Application.CourseAdd;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Commands.CourseAdd
{
    public class CourseAddCommandValidator : AbstractValidator<CourseAddCommand>
    {
        public CourseAddCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Description).NotEmpty().NotNull();
            RuleFor(x => x.Technology).NotEmpty().NotNull();
            RuleFor(x => x.Duration).NotEmpty().NotNull();
            RuleFor(x => x.LaunchURL).NotEmpty().NotNull();
        }
    }
}
