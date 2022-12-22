using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Queries.CourseByTech
{
    public class CourseByTechQueryValidator : AbstractValidator<CourseByTechQuery>
    {
        public CourseByTechQueryValidator()
        {
            RuleFor(x => x.Technology).NotNull().NotEmpty();
        }
    }
}
