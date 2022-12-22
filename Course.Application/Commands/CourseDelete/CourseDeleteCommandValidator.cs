using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Commands.CourseDelete
{
    public class CourseDeleteCommandValidator : AbstractValidator<CourseDeleteCommand>
    {
        public CourseDeleteCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
        }
    }
}
