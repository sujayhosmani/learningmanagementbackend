using Course.Domain.Entity;
using Course.Infrastructure.Interfaces;
using DnsClient.Internal;
using LearningManagement.Common.ResponseInterceptor;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Application.Queries.CourseByTech
{
    public class CourseByTechQueryHandler : IRequestHandler<CourseByTechQuery, ValidatableResponse<List<Courses>>>
    {

        private readonly ICourseRepository _repo;
        private readonly ILogger<CourseByTechQueryHandler> _logger;

        public CourseByTechQueryHandler(ICourseRepository repo, ILogger<CourseByTechQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        public async Task<ValidatableResponse<List<Courses>>> Handle(CourseByTechQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<Courses> courses = await _repo.CourserByTechnology(request.Technology);
                _logger.LogInformation("CourseByTechQueryHandler get tech requested for {tech}", request.Technology);
                return new ValidatableResponse<List<Courses>>("successful", null, courses, StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError("CourseByTechQueryHandler exception {message} ", ex.Message);
                return new ValidatableResponse<List<Courses>>(ex.Message, ex.Message, StatusCodes.Status500InternalServerError);
            }
           
        }
    }
}
