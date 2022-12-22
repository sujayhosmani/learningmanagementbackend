using LearningManagement.Common.ResponseInterceptor;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Commands.CourseDelete
{
    public class CourseDeleteCommand : IRequest<ValidatableResponse<string>>
    {
        public string Id { get; set; }
    }
}
