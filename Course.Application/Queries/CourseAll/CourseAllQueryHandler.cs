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

namespace Course.Application.Queries.CourseAll
{
    public class CourseAllQueryHandler : IRequestHandler<CourseAllQuery, ValidatableResponse<List<Courses>>>
    {
        private readonly ICourseRepository _repo;
        private readonly ILogger<CourseAllQueryHandler> _logger;

        public CourseAllQueryHandler(ICourseRepository courseRepository, ILogger<CourseAllQueryHandler> logger)
        {
            _repo = courseRepository;
            _logger = logger;
        }
        public async Task<ValidatableResponse<List<Courses>>> Handle(CourseAllQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<Courses> courses = await _repo.GetAllCourses();
                _logger.LogInformation("CourseAllQueryHandler getAll requested");
                return new ValidatableResponse<List<Courses>>("successful", null, courses, StatusCodes.Status200OK);
            }
            catch(Exception ex)
            {
                _logger.LogError("CourseAllQueryHandler exception {message} ", ex.Message);
                return new ValidatableResponse<List<Courses>>(ex.Message, ex.Message, StatusCodes.Status500InternalServerError);
            }
            
        }
    }
}
