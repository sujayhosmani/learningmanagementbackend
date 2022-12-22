using Course.Domain.Entity;
using LearningManagement.Common.ResponseInterceptor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.CourseAdd
{
    public class CourseAddCommand : IRequest<ValidatableResponse<Courses>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string Technology { get; set; }
        public string LaunchURL { get; set; }
    }
}
