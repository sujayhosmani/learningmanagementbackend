using Course.Domain.Entity;
using LearningManagement.Common.ResponseInterceptor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Queries.CourseAll
{
    public class CourseAllQuery : IRequest<ValidatableResponse<List<Courses>>>
    {
    }
}
