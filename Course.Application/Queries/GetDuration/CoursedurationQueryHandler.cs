using Course.Domain.Entity;
using Course.Infrastructure.Interfaces;
using LearningManagement.Common.ResponseInterceptor;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Queries.GetDuration
{
    public class CoursedurationQueryHandler : IRequestHandler<CourseDurationQuery, ValidatableResponse<Duration>>
    {
        private readonly ICourseRepository _repo;

        public CoursedurationQueryHandler(ICourseRepository repo)
        {
            _repo = repo;
        }
        public async Task<ValidatableResponse<Duration>> Handle(CourseDurationQuery request, CancellationToken cancellationToken)
        {
            Duration duration = await _repo.CourseDuration();
            return new ValidatableResponse<Duration>("successful", null, duration, StatusCodes.Status200OK);
        }
    }
}
