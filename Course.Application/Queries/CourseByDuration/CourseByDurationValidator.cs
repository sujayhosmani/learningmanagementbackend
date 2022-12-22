using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Queries.CourseByDuration
{
    public class CourseByDurationValidator : AbstractValidator<CourseByDurationQuery>
    {
        public CourseByDurationValidator()
        {
            RuleFor(x => x.From).NotEmpty().NotNull();
            RuleFor(x => x.To).NotEmpty().NotNull();
        }
    }
}
