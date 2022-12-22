using Course.Domain.Entity;
using LearningManagement.Common.ResponseInterceptor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Queries.CourseByDuration
{
    public class CourseByDurationQuery : IRequest<ValidatableResponse<List<Courses>>>
    {
        public string? Technology { get; set; }
        public int From { get; set; }
        public int To { get; set; }
    }
}
