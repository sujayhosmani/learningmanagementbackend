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

namespace Course.Application.Queries.GetTechnology
{
    public class CourseTechnologiesQueryHandler : IRequestHandler<CourseTechnologiesQuery, ValidatableResponse<List<string>>>
    {
        private readonly ICourseRepository _repo;

        public CourseTechnologiesQueryHandler(ICourseRepository repo)
        {
            _repo = repo;
        }
        public async Task<ValidatableResponse<List<string>>> Handle(CourseTechnologiesQuery request, CancellationToken cancellationToken)
        {
            List<string> courses = await _repo.CourseTechnologies();
            return new ValidatableResponse<List<string>>("successful", null, courses, StatusCodes.Status200OK);
        }
    }
}
